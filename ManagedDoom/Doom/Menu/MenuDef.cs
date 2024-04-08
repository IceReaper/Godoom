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
	public abstract class MenuDef
	{
		private DoomMenu menu;

		public MenuDef(DoomMenu menu)
		{
			this.menu = menu;
		}

		public virtual void Open()
		{
		}

		public virtual void Update()
		{
		}

		public abstract bool DoEvent(DoomEvent e);

		public DoomMenu Menu => menu;
	}
}
