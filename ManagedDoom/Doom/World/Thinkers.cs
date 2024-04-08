/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System.Collections;

namespace ManagedDoom.Doom.World;

public sealed class Thinkers
{
	private World world;

	public Thinkers(World world)
	{
		this.world = world;

		InitThinkers();
	}


	private Thinker cap;

	private void InitThinkers()
	{
		cap = new Thinker();
		cap.Prev = cap.Next = cap;
	}

	public void Add(Thinker thinker)
	{
		cap.Prev.Next = thinker;
		thinker.Next = cap;
		thinker.Prev = cap.Prev;
		cap.Prev = thinker;
	}

	public void Remove(Thinker thinker)
	{
		thinker.ThinkerState = ThinkerState.Removed;
	}

	public void Run()
	{
		var current = cap.Next;
		while (current != cap)
		{
			if (current.ThinkerState == ThinkerState.Removed)
			{
				// Time to remove it.
				current.Next.Prev = current.Prev;
				current.Prev.Next = current.Next;
			}
			else
			{
				if (current.ThinkerState == ThinkerState.Active)
				{
					current.Run();
				}
			}
			current = current.Next;
		}
	}

	public void UpdateFrameInterpolationInfo()
	{
		var current = cap.Next;
		while (current != cap)
		{
			current.UpdateFrameInterpolationInfo();
			current = current.Next;
		}
	}

	public void Reset()
	{
		cap.Prev = cap.Next = cap;
	}

	public ThinkerEnumerator GetEnumerator()
	{
		return new ThinkerEnumerator(this);
	}



	public struct ThinkerEnumerator : IEnumerator<Thinker>
	{
		private Thinkers thinkers;
		private Thinker current;

		public ThinkerEnumerator(Thinkers thinkers)
		{
			this.thinkers = thinkers;
			current = thinkers.cap;
		}

		public bool MoveNext()
		{
			while (true)
			{
				current = current.Next;
				if (current == thinkers.cap)
				{
					return false;
				}
				else if (current.ThinkerState != ThinkerState.Removed)
				{
					return true;
				}
			}
		}

		public void Reset()
		{
			current = thinkers.cap;
		}

		public void Dispose()
		{
		}

		public Thinker Current => current;

		object IEnumerator.Current => throw new NotImplementedException();
	}
}
