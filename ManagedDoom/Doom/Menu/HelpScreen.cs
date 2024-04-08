/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Audio;
using ManagedDoom.Doom.Event;
using ManagedDoom.Doom.Game;
using ManagedDoom.UserInput;

namespace ManagedDoom.Doom.Menu;

public sealed class HelpScreen : MenuDef
{
	private int pageCount;

	private int page;

	public HelpScreen(DoomMenu menu) : base(menu)
	{
		if (menu.Options.GameMode == GameMode.Shareware)
		{
			pageCount = 2;
		}
		else
		{
			pageCount = 1;
		}
	}

	public override void Open()
	{
		page = pageCount - 1;
	}

	public override bool DoEvent(DoomEvent e)
	{
		if (e.Type != EventType.KeyDown)
		{
			return true;
		}

		if (e.Key == DoomKey.Enter ||
			e.Key == DoomKey.Space ||
			e.Key == DoomKey.LControl ||
			e.Key == DoomKey.RControl)
		{
			page--;
			if (page == -1)
			{
				Menu.Close();
			}
			Menu.StartSound(Sfx.PISTOL);
		}

		if (e.Key == DoomKey.Escape)
		{
			Menu.Close();
			Menu.StartSound(Sfx.SWTCHX);
		}

		return true;
	}

	public int Page => page;
}
