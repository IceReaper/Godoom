/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Collections.Generic;

namespace ManagedDoom
{
	public static partial class DoomInfo
	{
		public static class ParTimes
		{
			// These lists are not readonly to allow change via BEX.

			public static readonly IReadOnlyList<IList<int>> Doom1 = new int[][]
			{
				new int[] {  30,  75, 120,  90, 165, 180, 180,  30, 165 },
				new int[] {  90,  90,  90, 120,  90, 360, 240,  30, 170 },
				new int[] {  90,  45,  90, 150,  90,  90, 165,  30, 135 },
				new int[] { 165, 255, 135, 150, 180, 390, 135, 360, 180 }
			};

			public static readonly IList<int> Doom2 = new int[]
			{
				30,   90, 120, 120,  90, 150, 120, 120, 270,  90,
				210, 150, 150, 150, 210, 150, 420, 150, 210, 150,
				240, 150, 180, 150, 150, 300, 330, 420, 300, 180,
				120,  30
			};
		}
	}
}
