﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Map;

[Flags]
public enum LineFlags
{
	Blocking = 1,
	BlockMonsters = 2,
	TwoSided = 4,
	DontPegTop = 8,
	DontPegBottom = 16,
	Secret = 32,
	SoundBlock = 64,
	DontDraw = 128,
	Mapped = 256
}
