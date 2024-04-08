/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.UserInput;

namespace ManagedDoom.Doom.Event;

public sealed class DoomEvent
{
	private EventType type;
	private DoomKey key;

	public DoomEvent(EventType type, DoomKey key)
	{
		this.type = type;
		this.key = key;
	}

	public EventType Type => type;
	public DoomKey Key => key;
}
