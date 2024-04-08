/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Collections.Generic;

namespace ManagedDoom
{
	public sealed class PressAnyKey : MenuDef
	{
		private string[] text;
		private Action action;

		public PressAnyKey(DoomMenu menu, string text, Action action) : base(menu)
		{
			this.text = text.Split('\n');
			this.action = action;
		}

		public override bool DoEvent(DoomEvent e)
		{
			if (e.Type == EventType.KeyDown)
			{
				if (action != null)
				{
					action();
				}

				Menu.Close();
				Menu.StartSound(Sfx.SWTCHX);

				return true;
			}

			return true;
		}

		public IReadOnlyList<string> Text => text;
	}
}
