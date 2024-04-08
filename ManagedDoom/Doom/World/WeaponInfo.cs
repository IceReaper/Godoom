/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System;

namespace ManagedDoom
{
	public sealed class WeaponInfo
	{
		private AmmoType ammo;
		private MobjState upState;
		private MobjState downState;
		private MobjState readyState;
		private MobjState attackState;
		private MobjState flashState;

		public WeaponInfo(
			AmmoType ammo,
			MobjState upState,
			MobjState downState,
			MobjState readyState,
			MobjState attackState,
			MobjState flashState)
		{
			this.ammo = ammo;
			this.upState = upState;
			this.downState = downState;
			this.readyState = readyState;
			this.attackState = attackState;
			this.flashState = flashState;
		}

		public AmmoType Ammo
		{
			get => ammo;
			set => ammo = value;
		}

		public MobjState UpState
		{
			get => upState;
			set => upState = value;
		}

		public MobjState DownState
		{
			get => downState;
			set => downState = value;
		}

		public MobjState ReadyState
		{
			get => readyState;
			set => readyState = value;
		}

		public MobjState AttackState
		{
			get => attackState;
			set => attackState = value;
		}

		public MobjState FlashState
		{
			get => flashState;
			set => flashState = value;
		}
	}
}
