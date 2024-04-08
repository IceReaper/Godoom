﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Common;

public sealed class DoomString
{
	private static Dictionary<string, DoomString> valueTable = new Dictionary<string, DoomString>();
	private static Dictionary<string, DoomString> nameTable = new Dictionary<string, DoomString>();

	private string original;
	private string replaced;

	public DoomString(string original)
	{
		this.original = original;
		replaced = original;

		if (!valueTable.ContainsKey(original))
		{
			valueTable.Add(original, this);
		}
	}

	public DoomString(string name, string original) : this(original)
	{
		nameTable.Add(name, this);
	}

	public override string ToString()
	{
		return replaced;
	}

	public char this[int index]
	{
		get
		{
			return replaced[index];
		}
	}

	public static implicit operator string(DoomString ds)
	{
		return ds.replaced;
	}

	public static void ReplaceByValue(string original, string replaced)
	{
		DoomString ds;
		if (valueTable.TryGetValue(original, out ds))
		{
			ds.replaced = replaced;
		}
	}

	public static void ReplaceByName(string name, string value)
	{
		DoomString ds;
		if (nameTable.TryGetValue(name, out ds))
		{
			ds.replaced = value;
		}
	}
}
