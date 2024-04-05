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
