/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Event;
using ManagedDoom.Doom.Game;
using System.Diagnostics;

namespace ManagedDoom.Doom.Opening;

public sealed class DemoPlayback
{
	private Demo demo;
	private TicCmd[] cmds;
	private DoomGame game;

	private Stopwatch stopwatch;
	private int frameCount;

	public DemoPlayback(CommandLineArgs args, GameContent content, GameOptions options, string demoName)
	{
		if (File.Exists(demoName))
		{
			demo = new Demo(demoName);
		}
		else if (File.Exists(demoName + ".lmp"))
		{
			demo = new Demo(demoName + ".lmp");
		}
		else
		{
			var lumpName = demoName.ToUpper();
			if (content.Wad.GetLumpNumber(lumpName) == -1)
			{
				throw new Exception("Demo '" + demoName + "' was not found!");
			}
			demo = new Demo(content.Wad.ReadLump(lumpName));
		}

		demo.Options.GameVersion = options.GameVersion;
		demo.Options.GameMode = options.GameMode;
		demo.Options.MissionPack = options.MissionPack;
		demo.Options.Video = options.Video;
		demo.Options.Sound = options.Sound;
		demo.Options.Music = options.Music;

		if (args.solonet.Present)
		{
			demo.Options.NetGame = true;
		}

		cmds = new TicCmd[Player.MaxPlayerCount];
		for (var i = 0; i < Player.MaxPlayerCount; i++)
		{
			cmds[i] = new TicCmd();
		}

		game = new DoomGame(content, demo.Options);
		game.DeferedInitNew();

		stopwatch = new Stopwatch();
	}

	public UpdateResult Update()
	{
		if (!stopwatch.IsRunning)
		{
			stopwatch.Start();
		}

		if (!demo.ReadCmd(cmds))
		{
			stopwatch.Stop();
			return UpdateResult.Completed;
		}
		else
		{
			frameCount++;
			return game.Update(cmds);
		}
	}

	public void DoEvent(DoomEvent e)
	{
		game.DoEvent(e);
	}

	public DoomGame Game => game;
	public double Fps => frameCount / stopwatch.Elapsed.TotalSeconds;
}
