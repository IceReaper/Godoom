/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Menu;

public class SimpleMenuItem : MenuItem
{
	private string name;
	private int itemX;
	private int itemY;
	private Action action;
	private Func<bool> selectable;

	public SimpleMenuItem(
		string name,
		int skullX, int skullY,
		int itemX, int itemY,
		Action action, MenuDef next)
		: base(skullX, skullY, next)
	{
		this.name = name;
		this.itemX = itemX;
		this.itemY = itemY;
		this.action = action;
		this.selectable = null;
	}

	public SimpleMenuItem(
		string name,
		int skullX, int skullY,
		int itemX, int itemY,
		Action action, MenuDef next, Func<bool> selectable)
		: base(skullX, skullY, next)
	{
		this.name = name;
		this.itemX = itemX;
		this.itemY = itemY;
		this.action = action;
		this.selectable = selectable;
	}

	public string Name => name;
	public int ItemX => itemX;
	public int ItemY => itemY;
	public Action Action => action;

	public bool Selectable
	{
		get
		{
			if (selectable == null)
			{
				return true;
			}
			else
			{
				return selectable();
			}
		}
	}
}
