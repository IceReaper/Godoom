/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Runtime.ExceptionServices;

namespace ManagedDoom
{
	public sealed class ColorMap
	{
		public static readonly int Inverse = 32;

		private byte[][] data;

		public ColorMap(Wad wad)
		{
			try
			{
				Console.Write("Load color map: ");

				var raw = wad.ReadLump("COLORMAP");
				var num = raw.Length / 256;
				data = new byte[num][];
				for (var i = 0; i < num; i++)
				{
					data[i] = new byte[256];
					var offset = 256 * i;
					for (var c = 0; c < 256; c++)
					{
						data[i][c] = raw[offset + c];
					}
				}

				Console.WriteLine("OK");
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed");
				ExceptionDispatchInfo.Throw(e);
			}
		}

		public byte[] this[int index]
		{
			get
			{
				return data[index];
			}
		}

		public byte[] FullBright
		{
			get
			{
				return data[0];
			}
		}
	}
}
