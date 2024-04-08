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
	public sealed class GlowingLight : Thinker
	{
		private static readonly int glowSpeed = 8;

		private World world;

		private Sector sector;
		private int minLight;
		private int maxLight;
		private int direction;

		public GlowingLight(World world)
		{
			this.world = world;
		}

		public override void Run()
		{
			switch (direction)
			{
				case -1:
					// Down.
					sector.LightLevel -= glowSpeed;
					if (sector.LightLevel <= minLight)
					{
						sector.LightLevel += glowSpeed;
						direction = 1;
					}
					break;

				case 1:
					// Up.
					sector.LightLevel += glowSpeed;
					if (sector.LightLevel >= maxLight)
					{
						sector.LightLevel -= glowSpeed;
						direction = -1;
					}
					break;
			}
		}

		public Sector Sector
		{
			get => sector;
			set => sector = value;
		}

		public int MinLight
		{
			get => minLight;
			set => minLight = value;
		}

		public int MaxLight
		{
			get => maxLight;
			set => maxLight = value;
		}

		public int Direction
		{
			get => direction;
			set => direction = value;
		}
	}
}
