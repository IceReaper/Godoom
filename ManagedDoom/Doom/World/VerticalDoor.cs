﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Audio;
using ManagedDoom.Doom.Map;
using ManagedDoom.Doom.Math;

namespace ManagedDoom.Doom.World;

public class VerticalDoor : Thinker
{
	private World world;

	private VerticalDoorType type;
	private Sector sector;
	private Fixed topHeight;
	private Fixed speed;

	// 1 = up, 0 = waiting at top, -1 = down.
	private int direction;

	// Tics to wait at the top.
	private int topWait;

	// When it reaches 0, start going down
	// (keep in case a door going down is reset).
	private int topCountDown;

	public VerticalDoor(World world)
	{
		this.world = world;
	}

	public override void Run()
	{
		var sa = world.SectorAction;

		SectorActionResult result;

		switch (direction)
		{
			case 0:
				// Waiting.
				if (--topCountDown == 0)
				{
					switch (type)
					{
						case VerticalDoorType.BlazeRaise:
							// Time to go back down.
							direction = -1;
							world.StartSound(sector.SoundOrigin, Sfx.BDCLS, SfxType.Misc);
							break;

						case VerticalDoorType.Normal:
							// Time to go back down.
							direction = -1;
							world.StartSound(sector.SoundOrigin, Sfx.DORCLS, SfxType.Misc);
							break;

						case VerticalDoorType.Close30ThenOpen:
							direction = 1;
							world.StartSound(sector.SoundOrigin, Sfx.DOROPN, SfxType.Misc);
							break;

						default:
							break;
					}
				}
				break;

			case 2:
				// Initial wait.
				if (--topCountDown == 0)
				{
					switch (type)
					{
						case VerticalDoorType.RaiseIn5Mins:
							direction = 1;
							type = VerticalDoorType.Normal;
							world.StartSound(sector.SoundOrigin, Sfx.DOROPN, SfxType.Misc);
							break;

						default:
							break;
					}
				}
				break;

			case -1:
				// Down.
				result = sa.MovePlane(
					sector,
					speed,
					sector.FloorHeight,
					false, 1, direction);
				if (result == SectorActionResult.PastDestination)
				{
					switch (type)
					{
						case VerticalDoorType.BlazeRaise:
						case VerticalDoorType.BlazeClose:
							sector.SpecialData = null;
							// Unlink and free.
							world.Thinkers.Remove(this);
							sector.DisableFrameInterpolationForOneFrame();
							world.StartSound(sector.SoundOrigin, Sfx.BDCLS, SfxType.Misc);
							break;

						case VerticalDoorType.Normal:
						case VerticalDoorType.Close:
							sector.SpecialData = null;
							// Unlink and free.
							world.Thinkers.Remove(this);
							sector.DisableFrameInterpolationForOneFrame();
							break;

						case VerticalDoorType.Close30ThenOpen:
							direction = 0;
							topCountDown = 35 * 30;
							break;

						default:
							break;
					}
				}
				else if (result == SectorActionResult.Crushed)
				{
					switch (type)
					{
						case VerticalDoorType.BlazeClose:
						case VerticalDoorType.Close: // Do not go back up!
							break;

						default:
							direction = 1;
							world.StartSound(sector.SoundOrigin, Sfx.DOROPN, SfxType.Misc);
							break;
					}
				}
				break;

			case 1:
				// Up.
				result = sa.MovePlane(
					sector,
					speed,
					topHeight,
					false, 1, direction);

				if (result == SectorActionResult.PastDestination)
				{
					switch (type)
					{
						case VerticalDoorType.BlazeRaise:
						case VerticalDoorType.Normal:
							// Wait at top.
							direction = 0;
							topCountDown = topWait;
							break;

						case VerticalDoorType.Close30ThenOpen:
						case VerticalDoorType.BlazeOpen:
						case VerticalDoorType.Open:
							sector.SpecialData = null;
							// Unlink and free.
							world.Thinkers.Remove(this);
							sector.DisableFrameInterpolationForOneFrame();
							break;

						default:
							break;
					}
				}
				break;
		}
	}

	public VerticalDoorType Type
	{
		get => type;
		set => type = value;
	}

	public Sector Sector
	{
		get => sector;
		set => sector = value;
	}

	public Fixed TopHeight
	{
		get => topHeight;
		set => topHeight = value;
	}

	public Fixed Speed
	{
		get => speed;
		set => speed = value;
	}

	public int Direction
	{
		get => direction;
		set => direction = value;
	}

	public int TopWait
	{
		get => topWait;
		set => topWait = value;
	}

	public int TopCountDown
	{
		get => topCountDown;
		set => topCountDown = value;
	}
}
