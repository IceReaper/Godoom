/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Collections.Generic;

namespace ManagedDoom
{
	public sealed class PatchCache
	{
		private Wad wad;
		private Dictionary<string, Patch> cache;

		public PatchCache(Wad wad)
		{
			this.wad = wad;

			cache = new Dictionary<string, Patch>();
		}

		public Patch this[string name]
		{
			get
			{
				Patch patch;
				if (!cache.TryGetValue(name, out patch))
				{
					patch = Patch.FromWad(wad, name);
					cache.Add(name, patch);
				}
				return patch;
			}
		}

		public int GetWidth(string name)
		{
			return this[name].Width;
		}

		public int GetHeight(string name)
		{
			return this[name].Height;
		}
	}
}
