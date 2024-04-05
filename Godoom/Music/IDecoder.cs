using MeltySynth;

namespace Godoom.Music;

public interface IDecoder
{
	public void RenderWaveform(Synthesizer synthesizer, Span<float> left, Span<float> right);
}
