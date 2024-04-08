/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.IO;

namespace ManagedDoom
{
	public sealed class SaveSlots
	{
		private static readonly int slotCount = 6;
		private static readonly int descriptionSize = 24;

		private string[] slots;

		private void ReadSlots()
		{
			slots = new string[slotCount];

			var directory = ConfigUtilities.GetExeDirectory();
			var buffer = new byte[descriptionSize];
			for (var i = 0; i < slots.Length; i++)
			{
				var path = Path.Combine(directory, "doomsav" + i + ".dsg");
				if (File.Exists(path))
				{
					using (var reader = new FileStream(path, FileMode.Open, FileAccess.Read))
					{
						reader.Read(buffer, 0, buffer.Length);
						slots[i] = DoomInterop.ToString(buffer, 0, buffer.Length);
					}
				}
			}
		}

		public string this[int number]
		{
			get
			{
				if (slots == null)
				{
					ReadSlots();
				}

				return slots[number];
			}

			set
			{
				slots[number] = value;
			}
		}

		public int Count => slots.Length;
	}
}
