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
	public sealed class FireFlicker : Thinker
	{
		private World world;

		private Sector sector;
		private int count;
		private int maxLight;
		private int minLight;

		public FireFlicker(World world)
		{
			this.world = world;
		}

		public override void Run()
		{
			if (--count > 0)
			{
				return;
			}

			var amount = (world.Random.Next() & 3) * 16;

			if (sector.LightLevel - amount < minLight)
			{
				sector.LightLevel = minLight;
			}
			else
			{
				sector.LightLevel = maxLight - amount;
			}

			count = 4;
		}

		public Sector Sector
		{
			get => sector;
			set => sector = value;
		}

		public int Count
		{
			get => count;
			set => count = value;
		}

		public int MaxLight
		{
			get => maxLight;
			set => maxLight = value;
		}

		public int MinLight
		{
			get => minLight;
			set => minLight = value;
		}
	}
}
