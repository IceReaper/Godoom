using Godot;
using ManagedDoom;
using MeltySynth;

namespace Godoom.Music;

public class MusReader
{
	private const int BlockLength = 2048;

	private readonly GodotMusic _parent;
	private readonly Config _config;

	private readonly Synthesizer _synthesizer;

	private readonly float[] _left;
	private readonly float[] _right;

	private volatile IDecoder? _current;
	private volatile IDecoder? _reserved;

	public MusReader(GodotMusic parent, Config config, string sfPath)
	{
		_parent = parent;
		_config = config;

		config.audio_musicvolume = Math.Clamp(config.audio_musicvolume, 0, parent.MaxVolume);

		_synthesizer = new Synthesizer(
			sfPath,
			new SynthesizerSettings(MusDecoder.SampleRate) { BlockSize = MusDecoder.BlockLength, EnableReverbAndChorus = config.audio_musiceffect }
		);

		_left = new float[BlockLength];
		_right = new float[BlockLength];
	}

	public void SetDecoder(IDecoder? decoder)
	{
		_reserved = decoder;
	}

	public Vector2[] GetData()
	{
		var data = new Vector2[BlockLength];

		if (_reserved != _current)
		{
			_synthesizer.Reset();
			_current = _reserved;
		}

		var a = 32768 * (2.0F * _config.audio_musicvolume / _parent.MaxVolume);

		_current?.RenderWaveform(_synthesizer, _left, _right);

		for (var i = 0; i < BlockLength; i++)
		{
			data[i] = new Vector2(
				Math.Clamp((int)(a * _left[i]), short.MinValue, short.MaxValue) / (float)short.MaxValue,
				Math.Clamp((int)(a * _right[i]), short.MinValue, short.MaxValue) / (float)short.MaxValue
			);
		}

		return data;
	}
}
