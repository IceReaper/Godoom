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
	public sealed class TextureAnimationInfo
	{
		private bool isTexture;
		private int picNum;
		private int basePic;
		private int numPics;
		private int speed;

		public TextureAnimationInfo(bool isTexture, int picNum, int basePic, int numPics, int speed)
		{
			this.isTexture = isTexture;
			this.picNum = picNum;
			this.basePic = basePic;
			this.numPics = numPics;
			this.speed = speed;
		}

		public bool IsTexture => isTexture;
		public int PicNum => picNum;
		public int BasePic => basePic;
		public int NumPics => numPics;
		public int Speed => speed;
	}
}
