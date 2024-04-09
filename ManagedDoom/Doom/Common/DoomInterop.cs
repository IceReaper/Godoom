/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Common;

public static class DoomInterop
{
	public static string ToString(byte[] data, int offset, int maxLength)
	{
		var length = 0;

		for (var i = 0; i < maxLength; i++)
		{
			if (data[offset + i] == 0)
				break;

			length++;
		}

		var chars = new char[length];

		for (var i = 0; i < chars.Length; i++)
		{
			var c = data[offset + i];

			if (c >= 'a' && c <= 'z')
				c -= 0x20;

			chars[i] = (char)c;
		}

		return new string(chars);
	}
}
