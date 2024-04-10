﻿/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.World;

namespace ManagedDoom.Doom.Info;

public static partial class DoomInfo
{
	public static readonly WeaponInfo[] WeaponInfos = new WeaponInfo[]
	{
		// fist
		new WeaponInfo(
			AmmoType.NoAmmo,
			MobjState.Punchup,
			MobjState.Punchdown,
			MobjState.Punch,
			MobjState.Punch1,
			MobjState.Null
		),

		// pistol
		new WeaponInfo(
			AmmoType.Clip,
			MobjState.Pistolup,
			MobjState.Pistoldown,
			MobjState.Pistol,
			MobjState.Pistol1,
			MobjState.Pistolflash
		),

		// shotgun
		new WeaponInfo(
			AmmoType.Shell,
			MobjState.Sgunup,
			MobjState.Sgundown,
			MobjState.Sgun,
			MobjState.Sgun1,
			MobjState.Sgunflash1
		),

		// chaingun
		new WeaponInfo(
			AmmoType.Clip,
			MobjState.Chainup,
			MobjState.Chaindown,
			MobjState.Chain,
			MobjState.Chain1,
			MobjState.Chainflash1
		),

		// missile launcher
		new WeaponInfo(
			AmmoType.Missile,
			MobjState.Missileup,
			MobjState.Missiledown,
			MobjState.Missile,
			MobjState.Missile1,
			MobjState.Missileflash1
		),

		// plasma rifle
		new WeaponInfo(
			AmmoType.Cell,
			MobjState.Plasmaup,
			MobjState.Plasmadown,
			MobjState.Plasma,
			MobjState.Plasma1,
			MobjState.Plasmaflash1
		),

		// bfg 9000
		new WeaponInfo(
			AmmoType.Cell,
			MobjState.Bfgup,
			MobjState.Bfgdown,
			MobjState.Bfg,
			MobjState.Bfg1,
			MobjState.Bfgflash1
		),

		// chainsaw
		new WeaponInfo(
			AmmoType.NoAmmo,
			MobjState.Sawup,
			MobjState.Sawdown,
			MobjState.Saw,
			MobjState.Saw1,
			MobjState.Null
		),

		// // super shotgun
		new WeaponInfo(
			AmmoType.Shell,
			MobjState.Dsgunup,
			MobjState.Dsgundown,
			MobjState.Dsgun,
			MobjState.Dsgun1,
			MobjState.Dsgunflash1
		)
	};
}
