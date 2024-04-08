/*
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
	private class MobjActions
	{
		public void BFGSpray(World.World world, Mobj actor)
		{
			world.WeaponBehavior.BFGSpray(actor);
		}

		public void Explode(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Explode(actor);
		}

		public void Pain(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Pain(actor);
		}

		public void PlayerScream(World.World world, Mobj actor)
		{
			world.PlayerBehavior.PlayerScream(actor);
		}

		public void Fall(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Fall(actor);
		}

		public void XScream(World.World world, Mobj actor)
		{
			world.MonsterBehavior.XScream(actor);
		}

		public void Look(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Look(actor);
		}

		public void Chase(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Chase(actor);
		}

		public void FaceTarget(World.World world, Mobj actor)
		{
			world.MonsterBehavior.FaceTarget(actor);
		}

		public void PosAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.PosAttack(actor);
		}

		public void Scream(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Scream(actor);
		}

		public void SPosAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SPosAttack(actor);
		}

		public void VileChase(World.World world, Mobj actor)
		{
			world.MonsterBehavior.VileChase(actor);
		}

		public void VileStart(World.World world, Mobj actor)
		{
			world.MonsterBehavior.VileStart(actor);
		}

		public void VileTarget(World.World world, Mobj actor)
		{
			world.MonsterBehavior.VileTarget(actor);
		}

		public void VileAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.VileAttack(actor);
		}

		public void StartFire(World.World world, Mobj actor)
		{
			world.MonsterBehavior.StartFire(actor);
		}

		public void Fire(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Fire(actor);
		}

		public void FireCrackle(World.World world, Mobj actor)
		{
			world.MonsterBehavior.FireCrackle(actor);
		}

		public void Tracer(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Tracer(actor);
		}

		public void SkelWhoosh(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SkelWhoosh(actor);
		}

		public void SkelFist(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SkelFist(actor);
		}

		public void SkelMissile(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SkelMissile(actor);
		}

		public void FatRaise(World.World world, Mobj actor)
		{
			world.MonsterBehavior.FatRaise(actor);
		}

		public void FatAttack1(World.World world, Mobj actor)
		{
			world.MonsterBehavior.FatAttack1(actor);
		}

		public void FatAttack2(World.World world, Mobj actor)
		{
			world.MonsterBehavior.FatAttack2(actor);
		}

		public void FatAttack3(World.World world, Mobj actor)
		{
			world.MonsterBehavior.FatAttack3(actor);
		}

		public void BossDeath(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BossDeath(actor);
		}

		public void CPosAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.CPosAttack(actor);
		}

		public void CPosRefire(World.World world, Mobj actor)
		{
			world.MonsterBehavior.CPosRefire(actor);
		}

		public void TroopAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.TroopAttack(actor);
		}

		public void SargAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SargAttack(actor);
		}

		public void HeadAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.HeadAttack(actor);
		}

		public void BruisAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BruisAttack(actor);
		}

		public void SkullAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SkullAttack(actor);
		}

		public void Metal(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Metal(actor);
		}

		public void SpidRefire(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SpidRefire(actor);
		}

		public void BabyMetal(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BabyMetal(actor);
		}

		public void BspiAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BspiAttack(actor);
		}

		public void Hoof(World.World world, Mobj actor)
		{
			world.MonsterBehavior.Hoof(actor);
		}

		public void CyberAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.CyberAttack(actor);
		}

		public void PainAttack(World.World world, Mobj actor)
		{
			world.MonsterBehavior.PainAttack(actor);
		}

		public void PainDie(World.World world, Mobj actor)
		{
			world.MonsterBehavior.PainDie(actor);
		}

		public void KeenDie(World.World world, Mobj actor)
		{
			world.MonsterBehavior.KeenDie(actor);
		}

		public void BrainPain(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BrainPain(actor);
		}

		public void BrainScream(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BrainScream(actor);
		}

		public void BrainDie(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BrainDie(actor);
		}

		public void BrainAwake(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BrainAwake(actor);
		}

		public void BrainSpit(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BrainSpit(actor);
		}

		public void SpawnSound(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SpawnSound(actor);
		}

		public void SpawnFly(World.World world, Mobj actor)
		{
			world.MonsterBehavior.SpawnFly(actor);
		}

		public void BrainExplode(World.World world, Mobj actor)
		{
			world.MonsterBehavior.BrainExplode(actor);
		}
	}
}
