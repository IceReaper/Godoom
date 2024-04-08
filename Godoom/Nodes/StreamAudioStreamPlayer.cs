/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using Godot;

namespace Godoom.Nodes;

public partial class StreamAudioStreamPlayer : AudioStreamPlayer
{
	private readonly AudioStreamGenerator _stream;

	private Vector2[] _buffer = [];
	private int _pointer;

	public Func<Vector2[]>? FillBuffer { get; set; }

	public float MixRate
	{
		get => _stream.MixRate;
		set => _stream.MixRate = value;
	}

	public StreamAudioStreamPlayer()
	{
		Stream = _stream = new AudioStreamGenerator();
	}

	public override void _Process(double delta)
	{
		if (FillBuffer == null)
			return;

		if (!Playing)
			Play();

		var audioPlayback = (AudioStreamGeneratorPlayback)GetStreamPlayback();

		while (true)
		{
			var remaining = audioPlayback.GetFramesAvailable();

			if (remaining == 0)
				break;

			if (_pointer == _buffer.Length)
			{
				_buffer = FillBuffer();
				_pointer = 0;
			}

			var frames = Math.Min(remaining, _buffer.Length - _pointer);

			audioPlayback.PushBuffer(_buffer.Skip(_pointer).Take(frames).ToArray());

			_pointer += frames;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
			_stream.Dispose();

		base.Dispose(disposing);
	}
}
