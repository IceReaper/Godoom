/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.IO;

namespace ManagedDoom
{
	public sealed class LumpInfo
	{
		public const int DataSize = 16;

		private string name;
		private Stream stream;
		private int position;
		private int size;

		public LumpInfo(string name, Stream stream, int position, int size)
		{
			this.name = name;
			this.stream = stream;
			this.position = position;
			this.size = size;
		}

		public string Name => name;
		public Stream Stream => stream;
		public int Position => position;
		public int Size => size;
	}
}
