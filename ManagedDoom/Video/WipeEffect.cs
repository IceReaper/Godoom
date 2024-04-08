/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;

namespace ManagedDoom.Video
{
	public sealed class WipeEffect
	{
		private short[] y;
		private int height;
		private DoomRandom random;

		public WipeEffect(int width, int height)
		{
			y = new short[width];
			this.height = height;
			random = new DoomRandom(DateTime.Now.Millisecond);
		}

		public void Start()
		{
			y[0] = (short)(-(random.Next() % 16));
			for (var i = 1; i < y.Length; i++)
			{
				var r = (random.Next() % 3) - 1;
				y[i] = (short)(y[i - 1] + r);
				if (y[i] > 0)
				{
					y[i] = 0;
				}
				else if (y[i] == -16)
				{
					y[i] = -15;
				}
			}
		}

		public UpdateResult Update()
		{
			var done = true;

			for (var i = 0; i < y.Length; i++)
			{
				if (y[i] < 0)
				{
					y[i]++;
					done = false;
				}
				else if (y[i] < height)
				{
					var dy = (y[i] < 16) ? y[i] + 1 : 8;
					if (y[i] + dy >= height)
					{
						dy = height - y[i];
					}
					y[i] += (short)dy;
					done = false;
				}
			}

			if (done)
			{
				return UpdateResult.Completed;
			}
			else
			{
				return UpdateResult.None;
			}
		}

		public short[] Y => y;
	}
}
