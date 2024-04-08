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
	public class OpeningSequenceRenderer
	{
		private DrawScreen screen;
		private Renderer parent;

		private PatchCache cache;

		public OpeningSequenceRenderer(Wad wad, DrawScreen screen, Renderer parent)
		{
			this.screen = screen;
			this.parent = parent;

			cache = new PatchCache(wad);
		}

		public void Render(OpeningSequence sequence, Fixed frameFrac)
		{
			var scale = screen.Width / 320;

			switch (sequence.State)
			{
				case OpeningSequenceState.Title:
					screen.DrawPatch(cache["TITLEPIC"], 0, 0, scale);
					break;

				case OpeningSequenceState.Demo:
					parent.RenderGame(sequence.DemoGame, frameFrac);
					break;

				case OpeningSequenceState.Credit:
					screen.DrawPatch(cache["CREDIT"], 0, 0, scale);
					break;
			}
		}
	}
}
