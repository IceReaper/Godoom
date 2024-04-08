/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Graphics.Dummy;

public static class DummyData
{
	private static Patch dummyPatch;

	public static Patch GetPatch()
	{
		if (dummyPatch != null)
		{
			return dummyPatch;
		}
		else
		{
			var width = 64;
			var height = 128;

			var data = new byte[height + 32];
			for (var y = 0; y < data.Length; y++)
			{
				data[y] = y / 32 % 2 == 0 ? (byte)80 : (byte)96;
			}

			var columns = new Column[width][];
			var c1 = new Column[] { new Column(0, data, 0, height) };
			var c2 = new Column[] { new Column(0, data, 32, height) };
			for (var x = 0; x < width; x++)
			{
				columns[x] = x / 32 % 2 == 0 ? c1 : c2;
			}

			dummyPatch = new Patch("DUMMY", width, height, 32, 128, columns);

			return dummyPatch;
		}
	}



	private static Dictionary<int, Texture> dummyTextures = new Dictionary<int, Texture>();

	public static Texture GetTexture(int height)
	{
		if (dummyTextures.ContainsKey(height))
		{
			return dummyTextures[height];
		}
		else
		{
			var patch = new TexturePatch[] { new TexturePatch(0, 0, GetPatch()) };

			dummyTextures.Add(height, new Texture("DUMMY", false, 64, height, patch));

			return dummyTextures[height];
		}
	}



	private static Flat dummyFlat;

	public static Flat GetFlat()
	{
		if (dummyFlat != null)
		{
			return dummyFlat;
		}
		else
		{
			var data = new byte[64 * 64];
			var spot = 0;
			for (var y = 0; y < 64; y++)
			{
				for (var x = 0; x < 64; x++)
				{
					data[spot] = ((x / 32) ^ (y / 32)) == 0 ? (byte)80 : (byte)96;
					spot++;
				}
			}

			dummyFlat = new Flat("DUMMY", data);

			return dummyFlat;
		}
	}



	private static Flat dummySkyFlat;

	public static Flat GetSkyFlat()
	{
		if (dummySkyFlat != null)
		{
			return dummySkyFlat;
		}
		else
		{
			dummySkyFlat = new Flat("DUMMY", GetFlat().Data);

			return dummySkyFlat;
		}
	}
}
