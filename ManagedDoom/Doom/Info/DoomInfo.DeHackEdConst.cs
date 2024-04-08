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
	public static partial class DoomInfo
	{
		public static class DeHackEdConst
		{
			public static int InitialHealth { get; set; } = 100;
			public static int InitialBullets { get; set; } = 50;
			public static int MaxHealth { get; set; } = 200;
			public static int MaxArmor { get; set; } = 200;
			public static int GreenArmorClass { get; set; } = 1;
			public static int BlueArmorClass { get; set; } = 2;
			public static int MaxSoulsphere { get; set; } = 200;
			public static int SoulsphereHealth { get; set; } = 100;
			public static int MegasphereHealth { get; set; } = 200;
			public static int GodModeHealth { get; set; } = 100;
			public static int IdfaArmor { get; set; } = 200;
			public static int IdfaArmorClass { get; set; } = 2;
			public static int IdkfaArmor { get; set; } = 200;
			public static int IdkfaArmorClass { get; set; } = 2;
			public static int BfgCellsPerShot { get; set; } = 40;
			public static bool MonstersInfight { get; set; } = false;
		}
	}
}
