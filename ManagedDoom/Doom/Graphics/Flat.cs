/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Graphics;

public sealed class Flat
{
	private string name;
	private byte[] data;

	public Flat(string name, byte[] data)
	{
		this.name = name;
		this.data = data;
	}

	public static Flat FromData(string name, byte[] data)
	{
		return new Flat(name, data);
	}

	public override string ToString()
	{
		return name;
	}

	public string Name => name;
	public byte[] Data => data;
}
