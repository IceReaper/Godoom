/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Collections.Generic;

namespace ManagedDoom
{
	public interface ITextureLookup : IReadOnlyList<Texture>
	{
		int GetNumber(string name);
		Texture this[string name] { get; }
		public int[] SwitchList { get; }
	}
}
