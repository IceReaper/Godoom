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

public interface IUserInput
{
	void BuildTicCmd(TicCmd cmd);
	void Reset();
	void GrabMouse();
	void ReleaseMouse();

	public int MaxMouseSensitivity { get; }
	public int MouseSensitivity { get; set; }
}
