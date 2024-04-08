/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.UserInput;

public static class DoomMouseButtonEx
{
	public static string ToString(DoomMouseButton button)
	{
		switch (button)
		{
			case DoomMouseButton.Mouse1:
				return "mouse1";
			case DoomMouseButton.Mouse2:
				return "mouse2";
			case DoomMouseButton.Mouse3:
				return "mouse3";
			case DoomMouseButton.Mouse4:
				return "mouse4";
			case DoomMouseButton.Mouse5:
				return "mouse5";
			default:
				return "unknown";
		}
	}

	public static DoomMouseButton Parse(string value)
	{
		switch (value)
		{
			case "mouse1":
				return DoomMouseButton.Mouse1;
			case "mouse2":
				return DoomMouseButton.Mouse2;
			case "mouse3":
				return DoomMouseButton.Mouse3;
			case "mouse4":
				return DoomMouseButton.Mouse4;
			case "mouse5":
				return DoomMouseButton.Mouse5;
			default:
				return DoomMouseButton.Unknown;
		}
	}
}
