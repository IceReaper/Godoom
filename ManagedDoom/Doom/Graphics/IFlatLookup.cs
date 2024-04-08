/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Graphics;

public interface IFlatLookup : IReadOnlyList<Flat>
{
	int GetNumber(string name);
	public Flat this[string name] { get; }
	public int SkyFlatNumber { get; }
	public Flat SkyFlat { get; }
}
