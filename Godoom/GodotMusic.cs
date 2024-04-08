/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using Godoom.Music;
using ManagedDoom;
using ManagedDoom.Audio;
using ManagedDoom.Doom.Game;
using ManagedDoom.Doom.Info;
using ManagedDoom.Doom.Wad;
using ManagedDoom.Platform;
using System.Globalization;
using Node = Godot.Node;
using StreamAudioStreamPlayer = Godoom.Nodes.StreamAudioStreamPlayer;

namespace Godoom;

public sealed class GodotMusic : IMusic, IDisposable
{
	private readonly Config _config;
	private readonly Wad _wad;

	private readonly MusReader _reader;
	private readonly StreamAudioStreamPlayer _audioPlayer;

	private Bgm _current;

	public int MaxVolume => 15;

	public int Volume
	{
		get => _config.AudioMusicVolume;
		set => _config.AudioMusicVolume = value;
	}

	public GodotMusic(Config config, GameContent content, Node node, string sfPath)
	{
		_config = config;
		_wad = content.Wad;

		_reader = new MusReader(this, config, sfPath);

		_audioPlayer = new StreamAudioStreamPlayer { MixRate = MusDecoder.SampleRate, FillBuffer = _reader.GetData };
		node.AddChild(_audioPlayer);

		_current = Bgm.NONE;
	}

	public void StartMusic(Bgm bgm, bool loop)
	{
		if (bgm == _current)
			return;

		var data = _wad.ReadLump($"D_{DoomInfo.BgmNames[(int)bgm].ToString().ToUpper(CultureInfo.InvariantCulture)}");
		var decoder = ReadData(data, loop);

		_reader.SetDecoder(decoder);

		_current = bgm;
	}

	private static IDecoder? ReadData(byte[] data, bool loop)
	{
		return MusDecoder.IsMus(data) ? new MusDecoder(data, loop) :
			MidiDecoder.IsMidi(data) ? new MidiDecoder(data, loop) : null;
	}

	public void Dispose()
	{
		_audioPlayer.Dispose();
	}
}
