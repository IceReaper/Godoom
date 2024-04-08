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
	public sealed class AnimationDef
	{
		private bool isTexture;
		private string endName;
		private string startName;
		private int speed;

		public AnimationDef(bool isTexture, string endName, string startName, int speed)
		{
			this.isTexture = isTexture;
			this.endName = endName;
			this.startName = startName;
			this.speed = speed;
		}

		public bool IsTexture => isTexture;
		public string EndName => endName;
		public string StartName => startName;
		public int Speed => speed;
	}
}
