/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Audio;
using ManagedDoom.Doom.World;

namespace ManagedDoom.Platform;

public interface ISound
{
	public void SetListener(Mobj listener);
	public void Update();
	public void StartSound(Sfx sfx);
	public void StartSound(Mobj mobj, Sfx sfx, SfxType type);
	public void StartSound(Mobj mobj, Sfx sfx, SfxType type, int volume);
	public void StopSound(Mobj mobj);
	public void Reset();
	public void Pause();
	public void Resume();

	public int MaxVolume { get; }
	public int Volume { get; set; }
}
