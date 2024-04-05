using MeltySynth;

namespace Godoom.Music;

public class MusDecoder : IDecoder
{
	private sealed class MusEvent
	{
		public int Type;
		public int Channel;
		public int Data1;
		public int Data2;
	}

	private enum ReadResult
	{
		Ongoing,
		EndOfGroup,
		EndOfFile
	}

	public const int SampleRate = 44100;
	public const int BlockLength = SampleRate / 140;

	private static readonly byte[] Header = [(byte)'M', (byte)'U', (byte)'S', 0x1A];

	private readonly byte[] _data;
	private readonly bool _loop;

	private readonly int[] _lastVolume = new int[16];
	private readonly MusEvent[] _events = new MusEvent[128];

	private readonly int _scoreStart;

	private int _eventCount;

	private int _p;
	private int _delay;

	private int _blockWrote = BlockLength;

	public MusDecoder(byte[] data, bool loop)
	{
		_data = data;
		_loop = loop;

		for (var i = 0; i < _events.Length; i++)
			_events[i] = new MusEvent();

		_scoreStart = BitConverter.ToUInt16(data, 6);

		Reset();
	}

	public static bool IsMus(IEnumerable<byte> data)
	{
		return data.Take(4).SequenceEqual(Header);
	}

	public void RenderWaveform(Synthesizer synthesizer, Span<float> left, Span<float> right)
	{
		var wrote = 0;

		while (wrote < left.Length)
		{
			if (_blockWrote == synthesizer.BlockSize)
			{
				ProcessMidiEvents(synthesizer);
				_blockWrote = 0;
			}

			var srcRem = synthesizer.BlockSize - _blockWrote;
			var dstRem = left.Length - wrote;
			var rem = Math.Min(srcRem, dstRem);

			synthesizer.Render(left.Slice(wrote, rem), right.Slice(wrote, rem));

			_blockWrote += rem;
			wrote += rem;
		}
	}

	private void ProcessMidiEvents(Synthesizer synthesizer)
	{
		if (_delay > 0)
			_delay--;

		if (_delay != 0)
			return;

		_delay = ReadSingleEventGroup();

		SendEvents(synthesizer);

		if (_delay != -1)
			return;

		synthesizer.NoteOffAll(false);

		if (_loop)
			Reset();
	}

	private void Reset()
	{
		for (var i = 0; i < _lastVolume.Length; i++)
			_lastVolume[i] = 0;

		_p = _scoreStart;
		_delay = 0;
	}

	private int ReadSingleEventGroup()
	{
		_eventCount = 0;

		while (true)
		{
			var result = ReadSingleEvent();

			if (result == ReadResult.EndOfGroup)
				break;

			if (result == ReadResult.EndOfFile)
				return -1;
		}

		var time = 0;

		while (true)
		{
			var value = _data[_p++];
			time = time * 128 + (value & 127);

			if ((value & 128) == 0)
				break;
		}

		return time;
	}

	private ReadResult ReadSingleEvent()
	{
		var channelNumber = _data[_p] & 0xF;

		channelNumber = channelNumber switch
		{
			15 => 9,
			>= 9 => channelNumber + 1,
			_ => channelNumber
		};

		var eventType = (_data[_p] & 0x70) >> 4;
		var last = _data[_p] >> 7 != 0;

		_p++;

		var @event = _events[_eventCount];
		_eventCount++;

		switch (eventType)
		{
			case 0:
				@event.Type = 0;
				@event.Channel = channelNumber;

				var releaseNote = _data[_p++];

				@event.Data1 = releaseNote;
				@event.Data2 = 0;

				break;

			case 1:
				@event.Type = 1;
				@event.Channel = channelNumber;

				var playNote = _data[_p++];
				var noteNumber = playNote & 127;
				var noteVolume = (playNote & 128) != 0 ? _data[_p++] : -1;

				@event.Data1 = noteNumber;

				if (noteVolume == -1)
					@event.Data2 = _lastVolume[channelNumber];
				else
				{
					@event.Data2 = noteVolume;
					_lastVolume[channelNumber] = noteVolume;
				}

				break;

			case 2:
				@event.Type = 2;
				@event.Channel = channelNumber;

				var pitchWheel = _data[_p++];

				var pitchWheel2 = (pitchWheel << 7) / 2;
				var pitchWheel1 = pitchWheel2 & 127;

				pitchWheel2 >>= 7;

				@event.Data1 = pitchWheel1;
				@event.Data2 = pitchWheel2;

				break;

			case 3:
				@event.Type = 3;
				@event.Channel = channelNumber;

				var systemEvent = _data[_p++];

				@event.Data1 = systemEvent;
				@event.Data2 = 0;

				break;

			case 4:
				@event.Type = 4;
				@event.Channel = channelNumber;

				var controllerNumber = _data[_p++];
				var controllerValue = _data[_p++];

				@event.Data1 = controllerNumber;
				@event.Data2 = controllerValue;

				break;

			case 6:
				return ReadResult.EndOfFile;
		}

		return last ? ReadResult.EndOfGroup : ReadResult.Ongoing;
	}

	private void SendEvents(Synthesizer synthesizer)
	{
		for (var i = 0; i < _eventCount; i++)
		{
			var @event = _events[i];

			switch (@event.Type)
			{
				case 0:
					synthesizer.NoteOff(@event.Channel, @event.Data1);

					break;

				case 1:
					synthesizer.NoteOn(@event.Channel, @event.Data1, @event.Data2);

					break;

				case 2:
					synthesizer.ProcessMidiMessage(@event.Channel, 0xE0, @event.Data1, @event.Data2);

					break;

				case 3:
					switch (@event.Data1)
					{
						case 11:
							synthesizer.NoteOffAll(@event.Channel, false);

							break;

						case 14:
							synthesizer.ResetAllControllers(@event.Channel);

							break;
					}

					break;

				case 4:
					switch (@event.Data1)
					{
						case 0:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xC0, @event.Data2, 0);

							break;

						case 1:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x00, @event.Data2);

							break;

						case 2:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x01, @event.Data2);

							break;

						case 3:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x07, @event.Data2);

							break;

						case 4:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x0A, @event.Data2);

							break;

						case 5:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x0B, @event.Data2);

							break;

						case 6:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x5B, @event.Data2);

							break;

						case 7:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x5D, @event.Data2);

							break;

						case 8:
							synthesizer.ProcessMidiMessage(@event.Channel, 0xB0, 0x40, @event.Data2);

							break;
					}

					break;
			}
		}
	}
}
