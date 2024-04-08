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
	private static readonly KeyBinding Empty = new();

	private readonly DoomKey[] _keys;
	private readonly DoomMouseButton[] _mouseButtons;

	private KeyBinding()
	{
		_keys = [];
		_mouseButtons = [];
	}

	public KeyBinding(IEnumerable<DoomKey> keys)
	{
		_keys = keys.ToArray();
		_mouseButtons = [];
	}

	public KeyBinding(IEnumerable<DoomKey> keys, IEnumerable<DoomMouseButton> mouseButtons)
	{
		_keys = keys.ToArray();
		_mouseButtons = mouseButtons.ToArray();
	}

	public override string ToString()
	{
		var keyValues = _keys.Select(DoomKeyEx.ToString);
		var mouseValues = _mouseButtons.Select(DoomMouseButtonEx.ToString);
		var values = keyValues.Concat(mouseValues).ToArray();

		return values.Length > 0 ? string.Join(", ", values) : "none";
	}

	public static KeyBinding Parse(string value)
	{
		if (value == "none")
			return Empty;

		var keys = new List<DoomKey>();
		var mouseButtons = new List<DoomMouseButton>();

		var split = value.Split(',').Select(static x => x.Trim());

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
				mouseButtons.Add(mouseButton);
		}

		return new KeyBinding(keys, mouseButtons);
	}

	public IEnumerable<DoomKey> Keys => _keys;
	public IEnumerable<DoomMouseButton> MouseButtons => _mouseButtons;
}
