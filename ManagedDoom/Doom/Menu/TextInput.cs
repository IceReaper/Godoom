/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Event;
using ManagedDoom.UserInput;

namespace ManagedDoom.Doom.Menu;

public sealed class TextInput
{
	private List<char> text;
	private Action<IReadOnlyList<char>> typed;
	private Action<IReadOnlyList<char>> finished;
	private Action canceled;

	private TextInputState state;

	public TextInput(
		IReadOnlyList<char> initialText,
		Action<IReadOnlyList<char>> typed,
		Action<IReadOnlyList<char>> finished,
		Action canceled)
	{
		this.text = initialText.ToList();
		this.typed = typed;
		this.finished = finished;
		this.canceled = canceled;

		state = TextInputState.Typing;
	}

	public bool DoEvent(DoomEvent e)
	{
		var ch = e.Key.GetChar();
		if (ch != 0)
		{
			text.Add(ch);
			typed(text);
			return true;
		}

		if (e.Key == DoomKey.Backspace && e.Type == EventType.KeyDown)
		{
			if (text.Count > 0)
			{
				text.RemoveAt(text.Count - 1);
			}
			typed(text);
			return true;
		}

		if (e.Key == DoomKey.Enter && e.Type == EventType.KeyDown)
		{
			finished(text);
			state = TextInputState.Finished;
			return true;
		}

		if (e.Key == DoomKey.Escape && e.Type == EventType.KeyDown)
		{
			canceled();
			state = TextInputState.Canceled;
			return true;
		}

		return true;
	}

	public IReadOnlyList<char> Text => text;
	public TextInputState State => state;
}
