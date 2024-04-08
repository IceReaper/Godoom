/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using MeltySynth;

namespace Godoom.Music;

public class MidiDecoder : IDecoder
{
	private static readonly byte[] Header = [(byte)'M', (byte)'T', (byte)'h', (byte)'d'];

	private readonly MidiFile _midi;
	private readonly bool _loop;

	private MidiFileSequencer? _sequencer;

	public MidiDecoder(byte[] data, bool loop)
	{
		_midi = new MidiFile(new MemoryStream(data));
		_loop = loop;
	}

	public static bool IsMidi(IEnumerable<byte> data)
	{
		return data.Take(4).SequenceEqual(Header);
	}

	public void RenderWaveform(Synthesizer synthesizer, Span<float> left, Span<float> right)
	{
		if (_sequencer == null)
		{
			_sequencer = new MidiFileSequencer(synthesizer);
			_sequencer.Play(_midi, _loop);
		}

		_sequencer.Render(left, right);
	}
}
