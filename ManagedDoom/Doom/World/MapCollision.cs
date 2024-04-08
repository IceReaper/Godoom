/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Map;
using ManagedDoom.Doom.Math;

namespace ManagedDoom.Doom.World;

public sealed class MapCollision
{
	private World world;

	private Fixed openTop;
	private Fixed openBottom;
	private Fixed openRange;
	private Fixed lowFloor;

	public MapCollision(World world)
	{
		this.world = world;
	}

	/// <summary>
	/// Sets opentop and openbottom to the window through a two sided line.
	/// </summary>
	public void LineOpening(LineDef line)
	{
		if (line.BackSide == null)
		{
			// If the line is single sided, nothing can pass through.
			openRange = Fixed.Zero;
			return;
		}

		var front = line.FrontSector;
		var back = line.BackSector;

		if (front.CeilingHeight < back.CeilingHeight)
		{
			openTop = front.CeilingHeight;
		}
		else
		{
			openTop = back.CeilingHeight;
		}

		if (front.FloorHeight > back.FloorHeight)
		{
			openBottom = front.FloorHeight;
			lowFloor = back.FloorHeight;
		}
		else
		{
			openBottom = back.FloorHeight;
			lowFloor = front.FloorHeight;
		}

		openRange = openTop - openBottom;
	}

	public Fixed OpenTop => openTop;
	public Fixed OpenBottom => openBottom;
	public Fixed OpenRange => openRange;
	public Fixed LowFloor => lowFloor;
}
