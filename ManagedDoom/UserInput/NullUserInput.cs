/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Game;

namespace ManagedDoom.UserInput;

public sealed class NullUserInput : IUserInput
{
	private static NullUserInput instance;

	public static NullUserInput GetInstance()
	{
		if (instance == null)
		{
			instance = new NullUserInput();
		}

		return instance;
	}

	public void BuildTicCmd(TicCmd cmd)
	{
		cmd.Clear();
	}

	public void Reset()
	{
	}

	public void GrabMouse()
	{
	}

	public void ReleaseMouse()
	{
	}

	public int MaxMouseSensitivity
	{
		get
		{
			return 9;
		}
	}

	public int MouseSensitivity
	{
		get
		{
			return 3;
		}

		set
		{
		}
	}
}
