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

public sealed class Intercept
{
	private Fixed frac;
	private Mobj thing;
	private LineDef line;

	public void Make(Fixed frac, Mobj thing)
	{
		this.frac = frac;
		this.thing = thing;
		this.line = null;
	}

	public void Make(Fixed frac, LineDef line)
	{
		this.frac = frac;
		this.thing = null;
		this.line = line;
	}

	public Fixed Frac
	{
		get => frac;
		set => frac = value;
	}

	public Mobj Thing => thing;
	public LineDef Line => line;
}
