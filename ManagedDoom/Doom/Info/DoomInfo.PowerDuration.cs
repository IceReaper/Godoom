/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Game;

namespace ManagedDoom.Doom.Info;

public static partial class DoomInfo
{
	public static class PowerDuration
	{
		public static readonly int Invulnerability = 30 * GameConst.TicRate;
		public static readonly int Invisibility = 60 * GameConst.TicRate;
		public static readonly int Infrared = 120 * GameConst.TicRate;
		public static readonly int IronFeet = 60 * GameConst.TicRate;
	}
}
