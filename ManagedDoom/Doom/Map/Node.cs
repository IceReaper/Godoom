﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Math;

namespace ManagedDoom.Doom.Map;

public sealed class Node
{
	private static readonly int dataSize = 28;

	private Fixed x;
	private Fixed y;
	private Fixed dx;
	private Fixed dy;

	private Fixed[][] boundingBox;

	private int[] children;

	public Node(
		Fixed x,
		Fixed y,
		Fixed dx,
		Fixed dy,
		Fixed frontBoundingBoxTop,
		Fixed frontBoundingBoxBottom,
		Fixed frontBoundingBoxLeft,
		Fixed frontBoundingBoxRight,
		Fixed backBoundingBoxTop,
		Fixed backBoundingBoxBottom,
		Fixed backBoundingBoxLeft,
		Fixed backBoundingBoxRight,
		int frontChild,
		int backChild)
	{
		this.x = x;
		this.y = y;
		this.dx = dx;
		this.dy = dy;

		var frontBoundingBox = new Fixed[4]
		{
			frontBoundingBoxTop,
			frontBoundingBoxBottom,
			frontBoundingBoxLeft,
			frontBoundingBoxRight
		};

		var backBoundingBox = new Fixed[4]
		{
			backBoundingBoxTop,
			backBoundingBoxBottom,
			backBoundingBoxLeft,
			backBoundingBoxRight
		};

		boundingBox = new Fixed[][]
		{
			frontBoundingBox,
			backBoundingBox
		};

		children = new int[]
		{
			frontChild,
			backChild
		};
	}

	public static Node FromData(byte[] data, int offset)
	{
		var x = BitConverter.ToInt16(data, offset);
		var y = BitConverter.ToInt16(data, offset + 2);
		var dx = BitConverter.ToInt16(data, offset + 4);
		var dy = BitConverter.ToInt16(data, offset + 6);
		var frontBoundingBoxTop = BitConverter.ToInt16(data, offset + 8);
		var frontBoundingBoxBottom = BitConverter.ToInt16(data, offset + 10);
		var frontBoundingBoxLeft = BitConverter.ToInt16(data, offset + 12);
		var frontBoundingBoxRight = BitConverter.ToInt16(data, offset + 14);
		var backBoundingBoxTop = BitConverter.ToInt16(data, offset + 16);
		var backBoundingBoxBottom = BitConverter.ToInt16(data, offset + 18);
		var backBoundingBoxLeft = BitConverter.ToInt16(data, offset + 20);
		var backBoundingBoxRight = BitConverter.ToInt16(data, offset + 22);
		var frontChild = BitConverter.ToInt16(data, offset + 24);
		var backChild = BitConverter.ToInt16(data, offset + 26);

		return new Node(
			Fixed.FromInt(x),
			Fixed.FromInt(y),
			Fixed.FromInt(dx),
			Fixed.FromInt(dy),
			Fixed.FromInt(frontBoundingBoxTop),
			Fixed.FromInt(frontBoundingBoxBottom),
			Fixed.FromInt(frontBoundingBoxLeft),
			Fixed.FromInt(frontBoundingBoxRight),
			Fixed.FromInt(backBoundingBoxTop),
			Fixed.FromInt(backBoundingBoxBottom),
			Fixed.FromInt(backBoundingBoxLeft),
			Fixed.FromInt(backBoundingBoxRight),
			frontChild,
			backChild);
	}

	public static Node[] FromWad(Wad.Wad wad, int lump, Subsector[] subsectors)
	{
		var length = wad.GetLumpSize(lump);
		if (length % dataSize != 0)
		{
			throw new Exception();
		}

		var data = wad.ReadLump(lump);
		var count = length / dataSize;
		var nodes = new Node[count];

		for (var i = 0; i < count; i++)
		{
			var offset = dataSize * i;
			nodes[i] = FromData(data, offset);
		}

		return nodes;
	}

	public static bool IsSubsector(int node)
	{
		return (node & unchecked((int)0xFFFF8000)) != 0;
	}

	public static int GetSubsector(int node)
	{
		return node ^ unchecked((int)0xFFFF8000);
	}

	public Fixed X => x;
	public Fixed Y => y;
	public Fixed Dx => dx;
	public Fixed Dy => dy;
	public Fixed[][] BoundingBox => boundingBox;
	public int[] Children => children;
}
