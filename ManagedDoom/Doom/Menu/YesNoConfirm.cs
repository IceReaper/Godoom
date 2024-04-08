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
	public sealed class YesNoConfirm : MenuDef
	{
		private string[] text;
		private Action action;

		public YesNoConfirm(DoomMenu menu, string text, Action action) : base(menu)
		{
			this.text = text.Split('\n');
			this.action = action;
		}

		public override bool DoEvent(DoomEvent e)
		{
			if (e.Type != EventType.KeyDown)
			{
				return true;
			}

			if (e.Key == DoomKey.Y ||
				e.Key == DoomKey.Enter ||
				e.Key == DoomKey.Space)
			{
				action();
				Menu.Close();
				Menu.StartSound(Sfx.PISTOL);
			}

			if (e.Key == DoomKey.N ||
				e.Key == DoomKey.Escape)
			{
				Menu.Close();
				Menu.StartSound(Sfx.SWTCHX);
			}

			return true;
		}

		public IReadOnlyList<string> Text => text;
	}
}
