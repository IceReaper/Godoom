/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;

namespace ManagedDoom
{
	public abstract class MenuItem
	{
		private int skullX;
		private int skullY;
		private MenuDef next;

		private MenuItem()
		{
		}

		public MenuItem(int skullX, int skullY, MenuDef next)
		{
			this.skullX = skullX;
			this.skullY = skullY;
			this.next = next;
		}

		public int SkullX => skullX;
		public int SkullY => skullY;
		public MenuDef Next => next;
	}
}
