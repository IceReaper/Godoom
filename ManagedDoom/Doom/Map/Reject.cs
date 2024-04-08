/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Map;

public sealed class Reject
{
	private byte[] data;
	private int sectorCount;

	private Reject(byte[] data, int sectorCount)
	{
		// If the reject table is too small, expand it to avoid crash.
		// https://doomwiki.org/wiki/Reject#Reject_Overflow
		var expectedLength = (sectorCount * sectorCount + 7) / 8;
		if (data.Length < expectedLength)
		{
			Array.Resize(ref data, expectedLength);
		}

		this.data = data;
		this.sectorCount = sectorCount;
	}

	public static Reject FromWad(Wad.Wad wad, int lump, Sector[] sectors)
	{
		return new Reject(wad.ReadLump(lump), sectors.Length);
	}

	public bool Check(Sector sector1, Sector sector2)
	{
		var s1 = sector1.Number;
		var s2 = sector2.Number;

		var p = s1 * sectorCount + s2;
		var byteIndex = p >> 3;
		var bitIndex = 1 << (p & 7);

		return (data[byteIndex] & bitIndex) != 0;
	}
}
