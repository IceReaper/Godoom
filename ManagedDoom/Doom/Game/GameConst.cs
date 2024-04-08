/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Math;

namespace ManagedDoom.Doom.Game;

public static class GameConst
{
	public static readonly int TicRate = 35;

	public static readonly Fixed MaxThingRadius = Fixed.FromInt(32);

	public static readonly int TurboThreshold = 0x32;
}
