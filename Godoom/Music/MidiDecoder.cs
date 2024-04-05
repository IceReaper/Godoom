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
