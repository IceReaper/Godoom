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
	public sealed class LightFlash : Thinker
	{
		private World world;

		private Sector sector;
		private int count;
		private int maxLight;
		private int minLight;
		private int maxTime;
		private int minTime;

		public LightFlash(World world)
		{
			this.world = world;
		}

		public override void Run()
		{
			if (--count > 0)
			{
				return;
			}

			if (sector.LightLevel == maxLight)
			{
				sector.LightLevel = minLight;
				count = (world.Random.Next() & minTime) + 1;
			}
			else
			{
				sector.LightLevel = maxLight;
				count = (world.Random.Next() & maxTime) + 1;
			}
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

		public int MaxTime
		{
			get => maxTime;
			set => maxTime = value;
		}

		public int MinTime
		{
			get => minTime;
			set => minTime = value;
		}
	}
}
