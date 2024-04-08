﻿/*
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
	public sealed class LoadMenu : MenuDef
	{
		private string[] name;
		private int[] titleX;
		private int[] titleY;
		private TextBoxMenuItem[] items;

		private int index;
		private TextBoxMenuItem choice;

		public LoadMenu(
			DoomMenu menu,
			string name, int titleX, int titleY,
			int firstChoice,
			params TextBoxMenuItem[] items) : base(menu)
		{
			this.name = new[] { name };
			this.titleX = new[] { titleX };
			this.titleY = new[] { titleY };
			this.items = items;

			index = firstChoice;
			choice = items[index];
		}

		public override void Open()
		{
			for (var i = 0; i < items.Length; i++)
			{
				items[i].SetText(Menu.SaveSlots[i]);
			}
		}

		private void Up()
		{
			index--;
			if (index < 0)
			{
				index = items.Length - 1;
			}

			choice = items[index];
		}

		private void Down()
		{
			index++;
			if (index >= items.Length)
			{
				index = 0;
			}

			choice = items[index];
		}

		public override bool DoEvent(DoomEvent e)
		{
			if (e.Type != EventType.KeyDown)
			{
				return true;
			}

			if (e.Key == DoomKey.Up)
			{
				Up();
				Menu.StartSound(Sfx.PSTOP);
			}

			if (e.Key == DoomKey.Down)
			{
				Down();
				Menu.StartSound(Sfx.PSTOP);
			}

			if (e.Key == DoomKey.Enter)
			{
				if (DoLoad(index))
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

		public bool DoLoad(int slotNumber)
		{
			if (Menu.SaveSlots[slotNumber] != null)
			{
				Menu.Doom.LoadGame(slotNumber);
				return true;
			}
			else
			{
				return false;
			}
		}

		public IReadOnlyList<string> Name => name;
		public IReadOnlyList<int> TitleX => titleX;
		public IReadOnlyList<int> TitleY => titleY;
		public IReadOnlyList<MenuItem> Items => items;
		public MenuItem Choice => choice;
	}
}
