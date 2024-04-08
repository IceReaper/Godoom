/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Menu;

public class ToggleMenuItem : MenuItem
{
	private string name;
	private int itemX;
	private int itemY;

	private string[] states;
	private int stateX;

	private int stateNumber;

	private Func<int> reset;
	private Action<int> action;

	public ToggleMenuItem(
		string name,
		int skullX, int skullY,
		int itemX, int itemY,
		string state1, string state2,
		int stateX,
		Func<int> reset,
		Action<int> action)
		: base(skullX, skullY, null)
	{
		this.name = name;
		this.itemX = itemX;
		this.itemY = itemY;

		this.states = new[] { state1, state2 };
		this.stateX = stateX;

		stateNumber = 0;

		this.action = action;
		this.reset = reset;
	}

	public void Reset()
	{
		if (reset != null)
		{
			stateNumber = reset();
		}
	}

	public void Up()
	{
		stateNumber++;
		if (stateNumber == states.Length)
		{
			stateNumber = 0;
		}

		if (action != null)
		{
			action(stateNumber);
		}
	}

	public void Down()
	{
		stateNumber--;
		if (stateNumber == -1)
		{
			stateNumber = states.Length - 1;
		}

		if (action != null)
		{
			action(stateNumber);
		}
	}

	public string Name => name;
	public int ItemX => itemX;
	public int ItemY => itemY;

	public string State => states[stateNumber];
	public int StateX => stateX;
}
