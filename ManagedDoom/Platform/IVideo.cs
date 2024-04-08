/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Math;

namespace ManagedDoom.Platform;

public interface IVideo
{
	public void Render(Doom.Doom doom, Fixed frameFrac);
	public void InitializeWipe();
	public bool HasFocus();

	public int MaxWindowSize { get; }
	public int WindowSize { get; set; }

	public bool DisplayMessage { get; set; }

	public int MaxGammaCorrectionLevel { get; }
	public int GammaCorrectionLevel { get; set; }

	public int WipeBandCount { get; }
	public int WipeHeight { get; }
}
