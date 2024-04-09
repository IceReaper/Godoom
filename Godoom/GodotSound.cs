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
using ManagedDoom.Doom.Wad;
using ManagedDoom.Doom.World;
using ManagedDoom.Platform;
using System.Globalization;
using Node = Godot.Node;

namespace Godoom;

public sealed class GodotSound : ISound, IDisposable
{
	private sealed class AudioInfo
	{
		public Mobj? Source;
		public Sfx Sound;
		public int Volume;
	}

	private const float ClipDist = 1200;

	private readonly Config _config;
	private readonly Node _node;

	private readonly AudioStreamWav?[] _buffers = new AudioStreamWav?[DoomInfo.SfxNames.Length];
	private readonly float[] _amplitudes = new float[DoomInfo.SfxNames.Length];

	private readonly DoomRandom? _random;

	private readonly Dictionary<Node, AudioInfo> _soundPlayers = [];

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
		_node = node;

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

	public void Update()
	{
		var now = DateTime.Now;

		if ((now - _lastUpdate).TotalSeconds < 0.01)
			return;

		foreach (var (player, info) in _soundPlayers)
			SetParam(player, info);

		_lastUpdate = now;
	}

	public void StartSound(Sfx sfx)
	{
		var player = new AudioStreamPlayer
		{
			Autoplay = true, VolumeDb = Linear2Db(_masterVolumeDecay), Stream = _buffers[(int)sfx], PitchScale = GetPitch(SfxType.Diffuse, sfx)
		};

		player.Finished += () =>
		{
			player.Dispose();
		};

		_node.AddChild(player);
	}

	public void StartSound(Mobj mobj, Sfx sfx, SfxType type)
	{
		StartSound(mobj, sfx, type, 100);
	}

	public void StartSound(Mobj mobj, Sfx sfx, SfxType type, int volume)
	{
		foreach (var (node, info) in _soundPlayers)
		{
			if (info.Source != mobj || info.Sound != sfx)
				continue;

			(node as AudioStreamPlayer)?.Play();
			(node as AudioStreamPlayer3D)?.Play();

			return;
		}

		if (type == SfxType.Diffuse || mobj == mobj.World.DisplayPlayer.Mobj)
		{
			var player = new AudioStreamPlayer
			{
				Autoplay = true, VolumeDb = Linear2Db(_masterVolumeDecay), Stream = _buffers[(int)sfx], PitchScale = GetPitch(SfxType.Diffuse, sfx)
			};

			player.Finished += () =>
			{
				_soundPlayers.Remove(player);
				player.Dispose();
			};

			var info = new AudioInfo { Source = mobj, Sound = sfx, Volume = volume };

			_node.AddChild(player);
			_soundPlayers.Add(player, info);

			SetParam(player, info);
		}
		else
		{
			var player = new AudioStreamPlayer3D
			{
				Autoplay = true,
				VolumeDb = Linear2Db(_masterVolumeDecay),
				Stream = _buffers[(int)sfx],
				PitchScale = GetPitch(SfxType.Diffuse, sfx),
				AttenuationFilterCutoffHz = 41000,
				UnitSize = 100,
				MaxDistance = ClipDist
			};

			player.Finished += () =>
			{
				_soundPlayers.Remove(player);
				player.Dispose();
			};

			var info = new AudioInfo { Source = mobj, Sound = sfx, Volume = volume };

			_node.AddChild(player);
			_soundPlayers.Add(player, info);

			SetParam(player, info);
		}
	}

	public void StopSound(Mobj mobj)
	{
		foreach (var info in _soundPlayers.Values.Where(info => info.Source == mobj))
		{
			info.Source = null;
			info.Volume /= 5;
		}
	}

	public void Reset()
	{
		_random?.Clear();

		foreach (var player in _soundPlayers.Keys.ToArray())
			player.Dispose();

		_soundPlayers.Clear();
	}

	public void Pause()
	{
		foreach (var node in _soundPlayers.Keys)
		{
			switch (node)
			{
				case AudioStreamPlayer player:
				{
					player.StreamPaused = true;

					break;
				}

				case AudioStreamPlayer3D player:
				{
					player.StreamPaused = true;

					break;
				}
			}
		}
	}

	public void Resume()
	{
		foreach (var node in _soundPlayers.Keys)
		{
			switch (node)
			{
				case AudioStreamPlayer player:
				{
					player.StreamPaused = false;

					break;
				}

				case AudioStreamPlayer3D player:
				{
					player.StreamPaused = false;

					break;
				}
			}
		}
	}

	private void SetParam(Node node, AudioInfo info)
	{
		switch (node)
		{
			case AudioStreamPlayer player:
			{
				player.VolumeDb = Linear2Db(0.01F * _masterVolumeDecay * info.Volume);

				break;
			}

			case AudioStreamPlayer3D player:
			{
				player.VolumeDb = Linear2Db(0.01F * _masterVolumeDecay * info.Volume);

				if (info.Source != null)
					player.Position = new Vector3(info.Source.X.ToFloat(), info.Source.Z.ToFloat(), info.Source.Y.ToFloat());

				break;
			}
		}
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
		foreach (var buffer in _buffers)
			buffer?.Dispose();
	}
}
