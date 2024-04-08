/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.UserInput;

public sealed class KeyBinding
{
	public static readonly KeyBinding Empty = new KeyBinding();

	private DoomKey[] keys;
	private DoomMouseButton[] mouseButtons;

	private KeyBinding()
	{
		keys = Array.Empty<DoomKey>();
		mouseButtons = Array.Empty<DoomMouseButton>();
	}

	public KeyBinding(IReadOnlyList<DoomKey> keys)
	{
		this.keys = keys.ToArray();
		this.mouseButtons = Array.Empty<DoomMouseButton>();
	}

	public KeyBinding(IReadOnlyList<DoomKey> keys, IReadOnlyList<DoomMouseButton> mouseButtons)
	{
		this.keys = keys.ToArray();
		this.mouseButtons = mouseButtons.ToArray();
	}

	public override string ToString()
	{
		var keyValues = keys.Select(key => DoomKeyEx.ToString(key));
		var mouseValues = mouseButtons.Select(button => DoomMouseButtonEx.ToString(button));
		var values = keyValues.Concat(mouseValues).ToArray();
		if (values.Length > 0)
		{
			return string.Join(", ", values);
		}
		else
		{
			return "none";
		}
	}

	public static KeyBinding Parse(string value)
	{
		if (value == "none")
		{
			return Empty;
		}

		var keys = new List<DoomKey>();
		var mouseButtons = new List<DoomMouseButton>();

		var split = value.Split(',').Select(x => x.Trim());
		foreach (var s in split)
		{
			var key = DoomKeyEx.Parse(s);
			if (key != DoomKey.Unknown)
			{
				keys.Add(key);
				continue;
			}

			var mouseButton = DoomMouseButtonEx.Parse(s);
			if (mouseButton != DoomMouseButton.Unknown)
			{
				mouseButtons.Add(mouseButton);
			}
		}

		return new KeyBinding(keys, mouseButtons);
	}

	public IReadOnlyList<DoomKey> Keys => keys;
	public IReadOnlyList<DoomMouseButton> MouseButtons => mouseButtons;
}
