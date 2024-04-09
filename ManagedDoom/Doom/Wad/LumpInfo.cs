/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Wad;

public sealed class LumpInfo
{
	public const int DataSize = 16;

	public string Name { get; }
	public Stream Stream { get; }
	public int Position { get; }
	public int Size { get; }

	public LumpInfo(string name, Stream stream, int position, int size)
	{
		Name = name;
		Stream = stream;
		Position = position;
		Size = size;
	}
}
