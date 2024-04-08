/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.World;

public class Thinker
{
	private Thinker prev;
	private Thinker next;
	private ThinkerState thinkerState;

	public Thinker()
	{
	}

	public virtual void Run()
	{
	}

	public virtual void UpdateFrameInterpolationInfo()
	{
	}

	public Thinker Prev
	{
		get => prev;
		set => prev = value;
	}

	public Thinker Next
	{
		get => next;
		set => next = value;
	}

	public ThinkerState ThinkerState
	{
		get => thinkerState;
		set => thinkerState = value;
	}
}
