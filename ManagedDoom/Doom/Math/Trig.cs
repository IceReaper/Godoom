/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;
using System.Runtime.CompilerServices;

namespace ManagedDoom
{
	public static partial class Trig
	{
		public const int FineAngleCount = 8192;
		public const int FineMask = FineAngleCount - 1;
		public const int AngleToFineShift = 19;

		private const int fineCosineOffset = FineAngleCount / 4;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed Tan(Angle anglePlus90)
		{
			return new Fixed(fineTangent[anglePlus90.Data >> AngleToFineShift]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed Tan(int fineAnglePlus90)
		{
			return new Fixed(fineTangent[fineAnglePlus90]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed Sin(Angle angle)
		{
			return new Fixed(fineSine[angle.Data >> AngleToFineShift]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed Sin(int fineAngle)
		{
			return new Fixed(fineSine[fineAngle]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed Cos(Angle angle)
		{
			return new Fixed(fineSine[(angle.Data >> AngleToFineShift) + fineCosineOffset]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed Cos(int fineAngle)
		{
			return new Fixed(fineSine[fineAngle + fineCosineOffset]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Angle TanToAngle(uint tan)
		{
			return new Angle(tanToAngle[tan]);
		}
	}
}
