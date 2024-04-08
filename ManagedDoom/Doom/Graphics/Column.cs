/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Graphics;

public sealed class Column
{
	public const int Last = 0xFF;

	private int topDelta;
	private byte[] data;
	private int offset;
	private int length;

	public Column(int topDelta, byte[] data, int offset, int length)
	{
		this.topDelta = topDelta;
		this.data = data;
		this.offset = offset;
		this.length = length;
	}

	public int TopDelta => topDelta;
	public byte[] Data => data;
	public int Offset => offset;
	public int Length => length;
}
