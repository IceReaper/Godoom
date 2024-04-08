/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.UserInput;

public static class DoomMouseButtonExtensions
{
	public static string ToString(DoomMouseButton button)
	{
		return button switch
		{
			DoomMouseButton.Mouse1 => "mouse1",
			DoomMouseButton.Mouse2 => "mouse2",
			DoomMouseButton.Mouse3 => "mouse3",
			DoomMouseButton.Mouse4 => "mouse4",
			DoomMouseButton.Mouse5 => "mouse5",
			_ => "unknown"
		};
	}

	public static DoomMouseButton Parse(string value)
	{
		return value switch
		{
			"mouse1" => DoomMouseButton.Mouse1,
			"mouse2" => DoomMouseButton.Mouse2,
			"mouse3" => DoomMouseButton.Mouse3,
			"mouse4" => DoomMouseButton.Mouse4,
			"mouse5" => DoomMouseButton.Mouse5,
			_ => DoomMouseButton.Unknown
		};
	}
}
