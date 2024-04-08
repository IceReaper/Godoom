/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Math;
using System.Runtime.CompilerServices;

namespace ManagedDoom.Doom.World;

public static class BoxEx
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Fixed Top(this Fixed[] box)
	{
		return box[Box.Top];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Fixed Bottom(this Fixed[] box)
	{
		return box[Box.Bottom];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Fixed Left(this Fixed[] box)
	{
		return box[Box.Left];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Fixed Right(this Fixed[] box)
	{
		return box[Box.Right];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Top(this int[] box)
	{
		return box[Box.Top];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Bottom(this int[] box)
	{
		return box[Box.Bottom];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Left(this int[] box)
	{
		return box[Box.Left];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Right(this int[] box)
	{
		return box[Box.Right];
	}
}
