/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace ManagedDoom
{
	public sealed class DummyFlatLookup : IFlatLookup
	{
		private Flat[] flats;

		private Dictionary<string, Flat> nameToFlat;
		private Dictionary<string, int> nameToNumber;

		private int skyFlatNumber;
		private Flat skyFlat;

		public DummyFlatLookup(Wad wad)
		{
			var firstFlat = wad.GetLumpNumber("F_START") + 1;
			var lastFlat = wad.GetLumpNumber("F_END") - 1;
			var count = lastFlat - firstFlat + 1;

			flats = new Flat[count];

			nameToFlat = new Dictionary<string, Flat>();
			nameToNumber = new Dictionary<string, int>();

			for (var lump = firstFlat; lump <= lastFlat; lump++)
			{
				if (wad.GetLumpSize(lump) != 4096)
				{
					continue;
				}

				var number = lump - firstFlat;
				var name = wad.LumpInfos[lump].Name;
				var flat = name != "F_SKY1" ? DummyData.GetFlat() : DummyData.GetSkyFlat();

				flats[number] = flat;
				nameToFlat[name] = flat;
				nameToNumber[name] = number;
			}

			skyFlatNumber = nameToNumber["F_SKY1"];
			skyFlat = nameToFlat["F_SKY1"];
		}

		public int GetNumber(string name)
		{
			if (nameToNumber.ContainsKey(name))
			{
				return nameToNumber[name];
			}
			else
			{
				return -1;
			}
		}

		public IEnumerator<Flat> GetEnumerator()
		{
			return ((IEnumerable<Flat>)flats).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return flats.GetEnumerator();
		}

		public int Count => flats.Length;
		public Flat this[int num] => flats[num];
		public Flat this[string name] => nameToFlat[name];
		public int SkyFlatNumber => skyFlatNumber;
		public Flat SkyFlat => skyFlat;
	}
}
