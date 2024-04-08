/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Common;
using ManagedDoom.Doom.Graphics;
using ManagedDoom.Doom.Math;

namespace ManagedDoom.Doom.Map;

public sealed class SideDef
{
	private static readonly int dataSize = 30;

	private Fixed textureOffset;
	private Fixed rowOffset;
	private int topTexture;
	private int bottomTexture;
	private int middleTexture;
	private Sector sector;

	public SideDef(
		Fixed textureOffset,
		Fixed rowOffset,
		int topTexture,
		int bottomTexture,
		int middleTexture,
		Sector sector)
	{
		this.textureOffset = textureOffset;
		this.rowOffset = rowOffset;
		this.topTexture = topTexture;
		this.bottomTexture = bottomTexture;
		this.middleTexture = middleTexture;
		this.sector = sector;
	}

	public static SideDef FromData(byte[] data, int offset, ITextureLookup textures, Sector[] sectors)
	{
		var textureOffset = BitConverter.ToInt16(data, offset);
		var rowOffset = BitConverter.ToInt16(data, offset + 2);
		var topTextureName = DoomInterop.ToString(data, offset + 4, 8);
		var bottomTextureName = DoomInterop.ToString(data, offset + 12, 8);
		var middleTextureName = DoomInterop.ToString(data, offset + 20, 8);
		var sectorNum = BitConverter.ToInt16(data, offset + 28);

		return new SideDef(
			Fixed.FromInt(textureOffset),
			Fixed.FromInt(rowOffset),
			textures.GetNumber(topTextureName),
			textures.GetNumber(bottomTextureName),
			textures.GetNumber(middleTextureName),
			sectorNum != -1 ? sectors[sectorNum] : null);
	}

	public static SideDef[] FromWad(Wad.Wad wad, int lump, ITextureLookup textures, Sector[] sectors)
	{
		var length = wad.GetLumpSize(lump);
		if (length % dataSize != 0)
		{
			throw new Exception();
		}

		var data = wad.ReadLump(lump);
		var count = length / dataSize;
		var sides = new SideDef[count]; ;

		for (var i = 0; i < count; i++)
		{
			var offset = dataSize * i;
			sides[i] = FromData(data, offset, textures, sectors);
		}

		return sides;
	}

	public Fixed TextureOffset
	{
		get => textureOffset;
		set => textureOffset = value;
	}

	public Fixed RowOffset
	{
		get => rowOffset;
		set => rowOffset = value;
	}

	public int TopTexture
	{
		get => topTexture;
		set => topTexture = value;
	}

	public int BottomTexture
	{
		get => bottomTexture;
		set => bottomTexture = value;
	}

	public int MiddleTexture
	{
		get => middleTexture;
		set => middleTexture = value;
	}

	public Sector Sector => sector;
}
