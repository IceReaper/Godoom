/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;

namespace ManagedDoom.Audio
{
	public sealed class NullMusic : IMusic
	{
		private static NullMusic instance;

		public static NullMusic GetInstance()
		{
			if (instance == null)
			{
				instance = new NullMusic();
			}

			return instance;
		}

		public void StartMusic(Bgm bgm, bool loop)
		{
		}

		public int MaxVolume
		{
			get
			{
				return 15;
			}
		}

		public int Volume
		{
			get
			{
				return 0;
			}

			set
			{
			}
		}
	}
}
