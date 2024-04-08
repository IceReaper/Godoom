/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Game;
using ManagedDoom.Doom.World;

namespace ManagedDoom.Doom.Info;

public static partial class DoomInfo
{
	private class PlayerActions
	{
		public void Light0(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Light0(player);
		}

		public void WeaponReady(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.WeaponReady(player, psp);
		}

		public void Lower(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Lower(player, psp);
		}

		public void Raise(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Raise(player, psp);
		}

		public void Punch(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Punch(player);
		}

		public void ReFire(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.ReFire(player);
		}

		public void FirePistol(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FirePistol(player);
		}

		public void Light1(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Light1(player);
		}

		public void FireShotgun(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FireShotgun(player);
		}

		public void Light2(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Light2(player);
		}

		public void FireShotgun2(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FireShotgun2(player);
		}

		public void CheckReload(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.CheckReload(player);
		}

		public void OpenShotgun2(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.OpenShotgun2(player);
		}

		public void LoadShotgun2(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.LoadShotgun2(player);
		}

		public void CloseShotgun2(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.CloseShotgun2(player);
		}

		public void FireCGun(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FireCGun(player, psp);
		}

		public void GunFlash(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.GunFlash(player);
		}

		public void FireMissile(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FireMissile(player);
		}

		public void Saw(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.Saw(player);
		}

		public void FirePlasma(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FirePlasma(player);
		}

		public void BFGsound(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.A_BFGsound(player);
		}

		public void FireBFG(World.World world, Player player, PlayerSpriteDef psp)
		{
			world.WeaponBehavior.FireBFG(player);
		}
	}
}
