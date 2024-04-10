﻿/*
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

public sealed class DivLine
{
	private Fixed x;
	private Fixed y;
	private Fixed dx;
	private Fixed dy;

	public void MakeFrom(LineDef line)
	{
		x = line.Vertex1.X;
		y = line.Vertex1.Y;
		dx = line.Dx;
		dy = line.Dy;
	}

	public Fixed X
	{
		get => x;
		set => x = value;
	}

	public Fixed Y
	{
		get => y;
		set => y = value;
	}

	public Fixed Dx
	{
		get => dx;
		set => dx = value;
	}

	public Fixed Dy
	{
		get => dy;
		set => dy = value;
	}
}
