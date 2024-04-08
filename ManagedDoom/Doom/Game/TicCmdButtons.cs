/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;

namespace ManagedDoom
{
	public static class TicCmdButtons
	{
		public static readonly byte Attack = 1;

		// Use button, to open doors, activate switches.
		public static readonly byte Use = 2;

		// Flag: game events, not really buttons.
		public static readonly byte Special = 128;
		public static readonly byte SpecialMask = 3;

		// Flag, weapon change pending.
		// If true, the next 3 bits hold weapon num.
		public static readonly byte Change = 4;

		// The 3bit weapon mask and shift, convenience.
		public static readonly byte WeaponMask = 8 + 16 + 32;
		public static readonly byte WeaponShift = 3;

		// Pause the game.
		public static readonly byte Pause = 1;
	}
}
