﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom.Doom.Intermission;

public sealed class Animation
{
	private Intermission im;
	private int number;

	private AnimationType type;
	private int period;
	private int frameCount;
	private int locationX;
	private int locationY;
	private int data;
	private string[] patches;
	private int patchNumber;
	private int nextTic;

	public Animation(Intermission intermission, AnimationInfo info, int number)
	{
		im = intermission;
		this.number = number;

		type = info.Type;
		period = info.Period;
		frameCount = info.Count;
		locationX = info.X;
		locationY = info.Y;
		data = info.Data;

		patches = new string[frameCount];
		for (var i = 0; i < frameCount; i++)
		{
			// MONDO HACK!
			if (im.Info.Episode != 1 || number != 8)
			{
				patches[i] = "WIA" + im.Info.Episode + number.ToString("00") + i.ToString("00");
			}
			else
			{
				// HACK ALERT!
				patches[i] = "WIA104" + i.ToString("00");
			}
		}
	}

	public void Reset(int bgCount)
	{
		patchNumber = -1;

		// Specify the next time to draw it.
		if (type == AnimationType.Always)
		{
			nextTic = bgCount + 1 + (im.Random.Next() % period);
		}
		else if (type == AnimationType.Random)
		{
			nextTic = bgCount + 1 + (im.Random.Next() % data);
		}
		else if (type == AnimationType.Level)
		{
			nextTic = bgCount + 1;
		}
	}

	public void Update(int bgCount)
	{
		if (bgCount == nextTic)
		{
			switch (type)
			{
				case AnimationType.Always:
					if (++patchNumber >= frameCount)
					{
						patchNumber = 0;
					}
					nextTic = bgCount + period;
					break;

				case AnimationType.Random:
					patchNumber++;
					if (patchNumber == frameCount)
					{
						patchNumber = -1;
						nextTic = bgCount + (im.Random.Next() % data);
					}
					else
					{
						nextTic = bgCount + period;
					}
					break;

				case AnimationType.Level:
					// Gawd-awful hack for level anims.
					if (!(im.State == IntermissionState.StatCount && number == 7) && im.Info.NextLevel == Data)
					{
						patchNumber++;
						if (patchNumber == frameCount)
						{
							patchNumber--;
						}
						nextTic = bgCount + period;
					}
					break;
			}
		}
	}

	public int LocationX => locationX;
	public int LocationY => locationY;
	public int Data => data;
	public IReadOnlyList<string> Patches => patches;
	public int PatchNumber => patchNumber;
}
