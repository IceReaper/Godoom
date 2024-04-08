/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Map;

namespace ManagedDoom.Doom.World;

public sealed class Button
{
	private LineDef line;
	private ButtonPosition position;
	private int texture;
	private int timer;
	private Mobj soundOrigin;

	public void Clear()
	{
		line = null;
		position = 0;
		texture = 0;
		timer = 0;
		soundOrigin = null;
	}

	public LineDef Line
	{
		get => line;
		set => line = value;
	}

	public ButtonPosition Position
	{
		get => position;
		set => position = value;
	}

	public int Texture
	{
		get => texture;
		set => texture = value;
	}

	public int Timer
	{
		get => timer;
		set => timer = value;
	}

	public Mobj SoundOrigin
	{
		get => soundOrigin;
		set => soundOrigin = value;
	}
}
