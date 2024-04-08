/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.World;

public enum FloorMoveType
{
	// Lower floor to highest surrounding floor.
	LowerFloor,

	// Lower floor to lowest surrounding floor.
	LowerFloorToLowest,

	// Lower floor to highest surrounding floor very fast.
	TurboLower,

	// Raise floor to lowest surrounding ceiling.
	RaiseFloor,

	// Raise floor to next highest surrounding floor.
	RaiseFloorToNearest,

	// Raise floor to shortest height texture around it.
	RaiseToTexture,

	// Lower floor to lowest surrounding floor and
	// change floor texture.
	LowerAndChange,

	RaiseFloor24,
	RaiseFloor24AndChange,
	RaiseFloorCrush,

	// Raise to next highest floor, turbo-speed.
	RaiseFloorTurbo,
	DonutRaise,
	RaiseFloor512
}
