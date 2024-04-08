/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Game;

public enum GameMode
{
	Shareware,  // DOOM 1 shareware, E1, M9
	Registered, // DOOM 1 registered, E3, M27
	Commercial, // DOOM 2 retail, E1 M34
	// DOOM 2 german edition not handled
	Retail, // DOOM 1 retail, E4, M36
	Indetermined	// Well, no IWAD found.
}
