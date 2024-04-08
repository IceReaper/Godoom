/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Math;

namespace ManagedDoom.Doom.World;

public sealed class PlayerSpriteDef
{
	private MobjStateDef state;
	private int tics;
	private Fixed sx;
	private Fixed sy;

	public void Clear()
	{
		state = null;
		tics = 0;
		sx = Fixed.Zero;
		sy = Fixed.Zero;
	}

	public MobjStateDef State
	{
		get => state;
		set => state = value;
	}

	public int Tics
	{
		get => tics;
		set => tics = value;
	}

	public Fixed Sx
	{
		get => sx;
		set => sx = value;
	}

	public Fixed Sy
	{
		get => sy;
		set => sy = value;
	}
}
