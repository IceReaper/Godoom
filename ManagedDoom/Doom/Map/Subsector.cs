/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Map;

public sealed class Subsector
{
	private static readonly int dataSize = 4;

	private Sector sector;
	private int segCount;
	private int firstSeg;

	public Subsector(Sector sector, int segCount, int firstSeg)
	{
		this.sector = sector;
		this.segCount = segCount;
		this.firstSeg = firstSeg;
	}

	public static Subsector FromData(byte[] data, int offset, Seg[] segs)
	{
		var segCount = BitConverter.ToInt16(data, offset);
		var firstSegNumber = BitConverter.ToInt16(data, offset + 2);

		return new Subsector(
			segs[firstSegNumber].SideDef.Sector,
			segCount,
			firstSegNumber);
	}

	public static Subsector[] FromWad(Wad.Wad wad, int lump, Seg[] segs)
	{
		var length = wad.GetLumpSize(lump);
		if (length % dataSize != 0)
		{
			throw new Exception();
		}

		var data = wad.ReadLump(lump);
		var count = length / dataSize;
		var subsectors = new Subsector[count];

		for (var i = 0; i < count; i++)
		{
			var offset = dataSize * i;
			subsectors[i] = FromData(data, offset, segs);
		}

		return subsectors;
	}

	public Sector Sector => sector;
	public int SegCount => segCount;
	public int FirstSeg => firstSeg;
}
