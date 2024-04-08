/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using Godot;
using ManagedDoom;
using ManagedDoom.Audio;
using ManagedDoom.Doom.Common;
using ManagedDoom.Doom.Game;
using ManagedDoom.Doom.Info;
using ManagedDoom.Doom.Math;
using ManagedDoom.Doom.Wad;
using ManagedDoom.Doom.World;
using ManagedDoom.Platform;
using System.Globalization;
using Node = Godot.Node;

namespace Godoom;

public sealed class GodotSound : ISound, IDisposable
{
	private sealed class ChannelInfo
	{
		public Sfx Reserved;
		public Sfx Playing;
		public float Priority;
		public Mobj? Source;
		public SfxType Type;
		public int Volume;
		public Fixed LastX;
		public Fixed LastY;
	}

	private const int ChannelCount = 8;

	private const float ClipDist = 1200;
	private const float CloseDist = 160;
	private const float Attenuator = ClipDist - CloseDist;

	private static readonly float FastDecay = (float)Math.Pow(0.5, 1.0 / 7);
	private static readonly float SlowDecay = (float)Math.Pow(0.5, 1.0 / 35);

	private readonly Config _config;

	private readonly AudioStreamWav?[] _buffers = new AudioStreamWav?[DoomInfo.SfxNames.Length];
	private readonly float[] _amplitudes = new float[DoomInfo.SfxNames.Length];

	private readonly DoomRandom? _random;

	private readonly AudioStreamPlayer3D[] _channels;
	private readonly ChannelInfo[] _infos;

	private readonly AudioStreamPlayer _uiChannel;
	private Sfx _uiReserved;

	private Mobj? _listener;

	private float _masterVolumeDecay;

	private DateTime _lastUpdate;

	public int MaxVolume => 15;

	public int Volume
	{
		get => _config.AudioSoundVolume;

		set
		{
			_config.AudioSoundVolume = value;
			_masterVolumeDecay = (float)_config.AudioSoundVolume / MaxVolume;
		}
	}

	public GodotSound(Config config, GameContent content, Node node)
	{
		_config = config;

		config.AudioSoundVolume = Math.Clamp(config.AudioSoundVolume, 0, MaxVolume);

		if (config.AudioRandomPitch)
			_random = new DoomRandom();

		for (var i = 0; i < DoomInfo.SfxNames.Length; i++)
		{
			var name = $"DS{DoomInfo.SfxNames[i].ToString().ToUpper(CultureInfo.InvariantCulture)}";

			if (content.Wad.GetLumpNumber(name) == -1)
				continue;

			var samples = GetSamples(content.Wad, name, out var sampleRate, out var sampleCount);

			if (samples.Length == 0)
				continue;

			_buffers[i] = new AudioStreamWav { Format = AudioStreamWav.FormatEnum.Format8Bits, Stereo = false, MixRate = sampleRate, Data = samples };
			_amplitudes[i] = GetAmplitude(samples, sampleRate, sampleCount);
		}

		_channels = new AudioStreamPlayer3D[ChannelCount];
		_infos = new ChannelInfo[ChannelCount];

		for (var i = 0; i < _channels.Length; i++)
		{
			_channels[i] = new AudioStreamPlayer3D { AttenuationModel = AudioStreamPlayer3D.AttenuationModelEnum.Disabled };
			_infos[i] = new ChannelInfo();

			node.AddChild(_channels[i]);
		}

		_uiChannel = new AudioStreamPlayer();
		node.AddChild(_uiChannel);

		_masterVolumeDecay = (float)config.AudioSoundVolume / MaxVolume;
	}

	private static byte[] GetSamples(Wad wad, string name, out int sampleRate, out int sampleCount)
	{
		var data = wad.ReadLump(name);

		if (data.Length < 8)
		{
			sampleRate = -1;
			sampleCount = -1;

			return [];
		}

		sampleRate = BitConverter.ToUInt16(data, 2);
		sampleCount = BitConverter.ToInt32(data, 4);

		var offset = 8;

		if (ContainsDmxPadding(data))
		{
			offset += 16;
			sampleCount -= 32;
		}

		data = data.Skip(offset).Take(sampleCount).ToArray();

		return data.Select(static b => (byte)(b - 128)).ToArray();
	}

	private static bool ContainsDmxPadding(byte[] data)
	{
		var sampleCount = BitConverter.ToInt32(data, 4);

		if (sampleCount < 32)
			return false;

		var first = data[8];

		for (var i = 1; i < 16; i++)
		{
			if (data[8 + i] != first)
				return false;
		}

		var last = data[8 + sampleCount - 1];

		for (var i = 1; i < 16; i++)
		{
			if (data[8 + sampleCount - i - 1] != last)
				return false;
		}

		return true;
	}

	private static float GetAmplitude(Span<byte> samples, int sampleRate, int sampleCount)
	{
		var max = 0;
		var count = Math.Min(sampleRate / 5, sampleCount);

		for (var t = 0; t < count; t++)
		{
			var a = samples[t] - 128;

			if (a < 0)
				a = -a;

			if (a > max)
				max = a;
		}

		return (float)max / 128;
	}

	public void SetListener(Mobj listener)
	{
		_listener = listener;
	}

	public void Update()
	{
		var now = DateTime.Now;

		if ((now - _lastUpdate).TotalSeconds < 0.01)
			return;

		for (var i = 0; i < _infos.Length; i++)
		{
			var info = _infos[i];
			var channel = _channels[i];

			if (info.Playing != Sfx.NONE)
			{
				if (channel.Playing)
				{
					info.Priority *= info.Type == SfxType.Diffuse ? SlowDecay : FastDecay;

					SetParam(channel, info);
				}
				else
				{
					info.Playing = Sfx.NONE;

					if (info.Reserved == Sfx.NONE)
						info.Source = null;
				}
			}

			if (info.Reserved == Sfx.NONE)
				continue;

			if (info.Playing != Sfx.NONE)
				channel.Stop();

			channel.Stream = _buffers[(int)info.Reserved];

			SetParam(channel, info);

			channel.PitchScale = GetPitch(info.Type, info.Reserved);
			channel.Play();

			info.Playing = info.Reserved;
			info.Reserved = Sfx.NONE;
		}

		if (_uiReserved != Sfx.NONE)
		{
			_uiChannel.Stop();

			_uiChannel.VolumeDb = Linear2Db(_masterVolumeDecay);
			_uiChannel.Stream = _buffers[(int)_uiReserved];
			_uiChannel.Play();

			_uiReserved = Sfx.NONE;
		}

		_lastUpdate = now;
	}

	public void StartSound(Sfx sfx)
	{
		_uiReserved = sfx;
	}

	public void StartSound(Mobj mobj, Sfx sfx, SfxType type)
	{
		StartSound(mobj, sfx, type, 100);
	}

	public void StartSound(Mobj mobj, Sfx sfx, SfxType type, int volume)
	{
		var x = (mobj.X - (_listener?.X ?? Fixed.Zero)).ToFloat();
		var y = (mobj.Y - (_listener?.Y ?? Fixed.Zero)).ToFloat();
		var dist = MathF.Sqrt(x * x + y * y);

		var priority = type == SfxType.Diffuse ? volume : _amplitudes[(int)sfx] * GetDistanceDecay(dist) * volume;

		var info = _infos.FirstOrDefault(info => info.Source == mobj && info.Type == type)
			?? _infos.FirstOrDefault(static info => info is { Reserved: Sfx.NONE, Playing: Sfx.NONE }) ?? _infos.OrderBy(static info => info.Priority).First();

		info.Reserved = sfx;
		info.Priority = priority;
		info.Source = mobj;
		info.Type = type;
		info.Volume = volume;
	}

	public void StopSound(Mobj mobj)
	{
		foreach (var info in _infos.Where(info => info.Source == mobj))
		{
			if (info.Source == null)
				continue;

			info.LastX = info.Source.X;
			info.LastY = info.Source.Y;
			info.Source = null;
			info.Volume /= 5;
		}
	}

	public void Reset()
	{
		_random?.Clear();

		for (var i = 0; i < _infos.Length; i++)
		{
			_channels[i].Stop();
			_infos[i] = new ChannelInfo();
		}

		_listener = null;
	}

	public void Pause()
	{
		foreach (var channel in _channels)
			channel.StreamPaused = true;
	}

	public void Resume()
	{
		foreach (var channel in _channels)
			channel.StreamPaused = false;
	}

	private void SetParam(AudioStreamPlayer3D sound, ChannelInfo info)
	{
		if (info.Type == SfxType.Diffuse)
		{
			sound.Position = new Vector3(0, 0, -1);
			sound.VolumeDb = Linear2Db(0.01F * _masterVolumeDecay * info.Volume);
		}
		else
		{
			Fixed sourceX;
			Fixed sourceY;

			if (info.Source == null)
			{
				sourceX = info.LastX;
				sourceY = info.LastY;
			}
			else
			{
				sourceX = info.Source.X;
				sourceY = info.Source.Y;
			}

			var x = (sourceX - (_listener?.X ?? Fixed.Zero)).ToFloat();
			var y = (sourceY - (_listener?.Y ?? Fixed.Zero)).ToFloat();

			if (Math.Abs(x) < 16 && Math.Abs(y) < 16)
			{
				sound.Position = new Vector3(0, 0, -1);
				sound.VolumeDb = Linear2Db(0.01F * _masterVolumeDecay * info.Volume);
			}
			else
			{
				var dist = MathF.Sqrt(x * x + y * y);
				var angle = MathF.Atan2(y, x) - (float)(_listener?.Angle.ToRadian() ?? 0);

				sound.Position = new Vector3(-MathF.Sin(angle), 0, -MathF.Cos(angle));
				sound.VolumeDb = Linear2Db(0.01F * _masterVolumeDecay * GetDistanceDecay(dist) * info.Volume);
			}
		}
	}

	private static float GetDistanceDecay(float dist)
	{
		return dist < CloseDist ? 1F : Math.Max((ClipDist - dist) / Attenuator, 0F);
	}

	private float GetPitch(SfxType type, Sfx sfx)
	{
		if (_random == null)
			return 1.0F;

		if (sfx is Sfx.ITEMUP or Sfx.TINK or Sfx.RADIO)
			return 1.0F;

		if (type == SfxType.Voice)
			return 1.0F + 0.075F * (_random.Next() - 128) / 128;

		return 1.0F + 0.025F * (_random.Next() - 128) / 128;
	}

	private static float Linear2Db(float linear)
	{
		return linear == 0 ? float.NegativeInfinity : 10 * MathF.Log10(linear);
	}

	public void Dispose()
	{
		_uiChannel.Dispose();

		foreach (var channel in _channels)
			channel.Dispose();

		foreach (var buffer in _buffers)
			buffer?.Dispose();
	}
}
