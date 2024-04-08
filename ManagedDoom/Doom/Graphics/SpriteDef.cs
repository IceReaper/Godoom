﻿/*
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
	public sealed class SpriteDef
	{
		private SpriteFrame[] frames;

		public SpriteDef(SpriteFrame[] frames)
		{
			this.frames = frames;
		}

		public SpriteFrame[] Frames => frames;
	}
}
