/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Game;

namespace ManagedDoom.Doom.Intermission;

public class IntermissionInfo
{
	// Episode number (0-2).
	private int episode;

	// If true, splash the secret level.
	private bool didSecret;

	// Previous and next levels, origin 0.
	private int lastLevel;
	private int nextLevel;

	private int maxKillCount;
	private int maxItemCount;
	private int maxSecretCount;
	private int totalFrags;

	// The par time.
	private int parTime;

	private PlayerScores[] players;

	public IntermissionInfo()
	{
		players = new PlayerScores[Player.MaxPlayerCount];
		for (var i = 0; i < Player.MaxPlayerCount; i++)
		{
			players[i] = new PlayerScores();
		}
	}

	public int Episode
	{
		get => episode;
		set => episode = value;
	}

	public bool DidSecret
	{
		get => didSecret;
		set => didSecret = value;
	}

	public int LastLevel
	{
		get => lastLevel;
		set => lastLevel = value;
	}

	public int NextLevel
	{
		get => nextLevel;
		set => nextLevel = value;
	}

	public int MaxKillCount
	{
		get => System.Math.Max(maxKillCount, 1);
		set => maxKillCount = value;
	}

	public int MaxItemCount
	{
		get => System.Math.Max(maxItemCount, 1);
		set => maxItemCount = value;
	}

	public int MaxSecretCount
	{
		get => System.Math.Max(maxSecretCount, 1);
		set => maxSecretCount = value;
	}

	public int TotalFrags
	{
		get => System.Math.Max(totalFrags, 1);
		set => totalFrags = value;
	}

	public int ParTime
	{
		get => parTime;
		set => parTime = value;
	}

	public PlayerScores[] Players
	{
		get => players;
	}
}
