/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;

namespace ManagedDoom
{
	public sealed class TexturePatch
	{
		public const int DataSize = 10;

		private int originX;
		private int originY;
		private Patch patch;

		public TexturePatch(
			int originX,
			int originY,
			Patch patch)
		{
			this.originX = originX;
			this.originY = originY;
			this.patch = patch;
		}

		public static TexturePatch FromData(byte[] data, int offset, Patch[] patches)
		{
			var originX = BitConverter.ToInt16(data, offset);
			var originY = BitConverter.ToInt16(data, offset + 2);
			var patchNum = BitConverter.ToInt16(data, offset + 4);

			return new TexturePatch(
				originX,
				originY,
				patches[patchNum]);
		}

		public string Name => patch.Name;
		public int OriginX => originX;
		public int OriginY => originY;
		public int Width => patch.Width;
		public int Height => patch.Height;
		public Column[][] Columns => patch.Columns;
	}
}
