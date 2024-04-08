/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using Godot;
using ManagedDoom;
using ManagedDoom.Doom.Game;

namespace Godoom;

public static class GodotConfigUtilities
{
	public static Config GetConfig()
	{
		var config = new Config(ConfigUtilities.GetConfigPath());

		var videoMode = GetDefaultVideoMode();

		if (!config.IsRestoredFromFile)
		{
			config.video_screenwidth = videoMode.X;
			config.video_screenheight = videoMode.Y;
		}

		config.video_screenwidth = Math.Clamp(config.video_screenwidth, 320, videoMode.X);
		config.video_screenheight = Math.Clamp(config.video_screenheight, 200, videoMode.Y);

		return config;
	}

	private static Vector2I GetDefaultVideoMode()
	{
		var monitor = DisplayServer.ScreenGetSize();

		const int baseWidth = 640;
		const int baseHeight = 400;

		var currentWidth = baseWidth;
		var currentHeight = baseHeight;

		while (true)
		{
			var nextWidth = currentWidth + baseWidth;
			var nextHeight = currentHeight + baseHeight;

			if (nextWidth >= 0.9 * monitor.X || nextHeight >= 0.9 * monitor.Y)
				break;

			currentWidth = nextWidth;
			currentHeight = nextHeight;
		}

		return new Vector2I(currentWidth, currentHeight);
	}

	public static GodotMusic? GetMusicInstance(Config config, GameContent content, Node3D node)
	{
		var soundFontPath = Path.Combine(ConfigUtilities.GetExeDirectory(), config.audio_soundfont);

		if (File.Exists(soundFontPath))
			return new GodotMusic(config, content, node, soundFontPath);

		GD.PrintErr($"SoundFont '{config.audio_soundfont}' was not found!");

		return null;
	}
}
