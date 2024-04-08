/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Game;
using ManagedDoom.Doom.Graphics;

namespace ManagedDoom.Doom.World;

public class MobjStateDef
{
	private int number;
	private Sprite sprite;
	private int frame;
	private int tics;
	private Action<World, Player, PlayerSpriteDef> playerAction;
	private Action<World, Mobj> mobjAction;
	private MobjState next;
	private int misc1;
	private int misc2;

	public MobjStateDef(
		int number,
		Sprite sprite,
		int frame,
		int tics,
		Action<World, Player, PlayerSpriteDef> playerAction,
		Action<World, Mobj> mobjAction,
		MobjState next,
		int misc1,
		int misc2)
	{
		this.number = number;
		this.sprite = sprite;
		this.frame = frame;
		this.tics = tics;
		this.playerAction = playerAction;
		this.mobjAction = mobjAction;
		this.next = next;
		this.misc1 = misc1;
		this.misc2 = misc2;
	}

	public int Number
	{
		get => number;
		set => number = value;
	}

	public Sprite Sprite
	{
		get => sprite;
		set => sprite = value;
	}

	public int Frame
	{
		get => frame;
		set => frame = value;
	}

	public int Tics
	{
		get => tics;
		set => tics = value;
	}

	public Action<World, Player, PlayerSpriteDef> PlayerAction
	{
		get => playerAction;
		set => playerAction = value;
	}

	public Action<World, Mobj> MobjAction
	{
		get => mobjAction;
		set => mobjAction = value;
	}

	public MobjState Next
	{
		get => next;
		set => next = value;
	}

	public int Misc1
	{
		get => misc1;
		set => misc1 = value;
	}

	public int Misc2
	{
		get => misc2;
		set => misc2 = value;
	}
}
