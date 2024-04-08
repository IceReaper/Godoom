﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Map;
using ManagedDoom.Doom.World;
using System.Text;

namespace ManagedDoom.Doom.Common;

public static class DoomDebug
{
	public static int CombineHash(int a, int b)
	{
		return (3 * a) ^ b;
	}

	public static int GetMobjHash(Mobj mobj)
	{
		var hash = 0;

		hash = CombineHash(hash, mobj.X.Data);
		hash = CombineHash(hash, mobj.Y.Data);
		hash = CombineHash(hash, mobj.Z.Data);

		hash = CombineHash(hash, (int)mobj.Angle.Data);
		hash = CombineHash(hash, (int)mobj.Sprite);
		hash = CombineHash(hash, mobj.Frame);

		hash = CombineHash(hash, mobj.FloorZ.Data);
		hash = CombineHash(hash, mobj.CeilingZ.Data);

		hash = CombineHash(hash, mobj.Radius.Data);
		hash = CombineHash(hash, mobj.Height.Data);

		hash = CombineHash(hash, mobj.MomX.Data);
		hash = CombineHash(hash, mobj.MomY.Data);
		hash = CombineHash(hash, mobj.MomZ.Data);

		hash = CombineHash(hash, mobj.Tics);
		hash = CombineHash(hash, (int)mobj.Flags);
		hash = CombineHash(hash, mobj.Health);

		hash = CombineHash(hash, (int)mobj.MoveDir);
		hash = CombineHash(hash, mobj.MoveCount);

		hash = CombineHash(hash, mobj.ReactionTime);
		hash = CombineHash(hash, mobj.Threshold);

		return hash;
	}

	public static int GetMobjHash(World.World world)
	{
		var hash = 0;
		foreach (var thinker in world.Thinkers)
		{
			var mobj = thinker as Mobj;
			if (mobj != null)
			{
				hash = CombineHash(hash, GetMobjHash(mobj));
			}
		}
		return hash;
	}

	private static string GetMobjCsv(Mobj mobj)
	{
		var sb = new StringBuilder();

		sb.Append(mobj.X.Data).Append(",");
		sb.Append(mobj.Y.Data).Append(",");
		sb.Append(mobj.Z.Data).Append(",");

		sb.Append((int)mobj.Angle.Data).Append(",");
		sb.Append((int)mobj.Sprite).Append(",");
		sb.Append(mobj.Frame).Append(",");

		sb.Append(mobj.FloorZ.Data).Append(",");
		sb.Append(mobj.CeilingZ.Data).Append(",");

		sb.Append(mobj.Radius.Data).Append(",");
		sb.Append(mobj.Height.Data).Append(",");

		sb.Append(mobj.MomX.Data).Append(",");
		sb.Append(mobj.MomY.Data).Append(",");
		sb.Append(mobj.MomZ.Data).Append(",");

		sb.Append((int)mobj.Tics).Append(",");
		sb.Append((int)mobj.Flags).Append(",");
		sb.Append(mobj.Health).Append(",");

		sb.Append((int)mobj.MoveDir).Append(",");
		sb.Append(mobj.MoveCount).Append(",");

		sb.Append(mobj.ReactionTime).Append(",");
		sb.Append(mobj.Threshold);

		return sb.ToString();
	}

	public static void DumpMobjCsv(string path, World.World world)
	{
		using (var writer = new StreamWriter(path))
		{
			foreach (var thinker in world.Thinkers)
			{
				var mobj = thinker as Mobj;
				if (mobj != null)
				{
					writer.WriteLine(GetMobjCsv(mobj));
				}
			}
		}
	}

	public static int GetSectorHash(Sector sector)
	{
		var hash = 0;

		hash = CombineHash(hash, sector.FloorHeight.Data);
		hash = CombineHash(hash, sector.CeilingHeight.Data);
		hash = CombineHash(hash, sector.LightLevel);

		return hash;
	}

	public static int GetSectorHash(World.World world)
	{
		var hash = 0;
		foreach (var sector in world.Map.Sectors)
		{
			hash = CombineHash(hash, GetSectorHash(sector));
		}
		return hash;
	}
}
