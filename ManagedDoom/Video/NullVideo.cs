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
	public class NullVideo : IVideo
	{
		private static NullVideo instance;

		public static NullVideo GetInstance()
		{
			if (instance == null)
			{
				instance = new NullVideo();
			}

			return instance;
		}

		public void Render(Doom doom, Fixed frameFrac)
		{
		}

		public void InitializeWipe()
		{
		}

		public bool HasFocus()
		{
			return true;
		}

		public int MaxWindowSize => ThreeDRenderer.MaxScreenSize;

		public int WindowSize
		{
			get
			{
				return 7;
			}

			set
			{
			}
		}

		public bool DisplayMessage
		{
			get
			{
				return true;
			}

			set
			{
			}
		}

		public int MaxGammaCorrectionLevel => 10;

		public int GammaCorrectionLevel
		{
			get
			{
				return 2;
			}

			set
			{
			}
		}

		public int WipeBandCount => 321;
		public int WipeHeight => 200;
	}
}
