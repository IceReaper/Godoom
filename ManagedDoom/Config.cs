/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.UserInput;

namespace ManagedDoom;

public sealed class Config
{
	public KeyBinding KeyForward { get; }
	public KeyBinding KeyBackward { get; }
	public KeyBinding KeyStrafeLeft { get; }
	public KeyBinding KeyStrafeRight { get; }
	public KeyBinding KeyFire { get; }
	public KeyBinding KeyUse { get; }
	public KeyBinding KeyRun { get; }

	public int MouseSensitivity { get; set; }

	public bool GameAlwaysRun { get; }

	public int VideoScreenWidth { get; set; }
	public int VideoScreenHeight { get; set; }
	public bool VideoFullscreen { get; }
	public bool VideoHighResolution { get; }
	public bool VideoDisplayMessage { get; set; }
	public int VideoGameScreenSize { get; set; }
	public int VideoGammaCorrection { get; set; }
	public int VideoFpsScale { get; set; }

	public int AudioSoundVolume { get; set; }
	public int AudioMusicVolume { get; set; }
	public bool AudioRandomPitch { get; }
	public string AudioSoundFont { get; }
	public bool AudioMusicEffect { get; }

	public bool IsRestoredFromFile { get; }

	public Config(string path)
	{
		var configFile = new Dictionary<string, string>();

		if (File.Exists(path))
		{
			configFile = File.ReadLines(path)
				.Select(static line => line.Split('=', StringSplitOptions.RemoveEmptyEntries))
				.Where(static split => split.Length == 2)
				.ToDictionary(static split => split[0].Trim(), static split => split[1].Trim());

			IsRestoredFromFile = true;
		}

		KeyForward = GetKeyBinding(configFile, nameof(KeyForward), new KeyBinding([DoomKey.W]));
		KeyBackward = GetKeyBinding(configFile, nameof(KeyBackward), new KeyBinding([DoomKey.S]));
		KeyStrafeLeft = GetKeyBinding(configFile, nameof(KeyStrafeLeft), new KeyBinding([DoomKey.A]));
		KeyStrafeRight = GetKeyBinding(configFile, nameof(KeyStrafeRight), new KeyBinding([DoomKey.D]));
		KeyFire = GetKeyBinding(configFile, nameof(KeyFire), new KeyBinding([], [DoomMouseButton.Mouse1]));
		KeyUse = GetKeyBinding(configFile, nameof(KeyUse), new KeyBinding([DoomKey.E]));
		KeyRun = GetKeyBinding(configFile, nameof(KeyRun), new KeyBinding([DoomKey.LShift]));

		MouseSensitivity = GetInt(configFile, nameof(MouseSensitivity), 8);

		GameAlwaysRun = GetBool(configFile, nameof(GameAlwaysRun), true);

		VideoScreenWidth = GetInt(configFile, nameof(VideoScreenWidth), 640);
		VideoScreenHeight = GetInt(configFile, nameof(VideoScreenHeight), 400);
		VideoFullscreen = GetBool(configFile, nameof(VideoFullscreen), false);
		VideoHighResolution = GetBool(configFile, nameof(VideoHighResolution), true);
		VideoDisplayMessage = GetBool(configFile, nameof(VideoDisplayMessage), true);
		VideoGameScreenSize = GetInt(configFile, nameof(VideoGameScreenSize), 7);
		VideoGammaCorrection = GetInt(configFile, nameof(VideoGammaCorrection), 2);
		VideoFpsScale = GetInt(configFile, nameof(VideoFpsScale), 2);

		AudioSoundVolume = GetInt(configFile, nameof(AudioSoundVolume), 8);
		AudioMusicVolume = GetInt(configFile, nameof(AudioMusicVolume), 8);
		AudioRandomPitch = GetBool(configFile, nameof(AudioRandomPitch), true);
		AudioSoundFont = GetString(configFile, nameof(AudioSoundFont), "TimGM6mb.sf2");
		AudioMusicEffect = GetBool(configFile, nameof(AudioMusicEffect), true);
	}

	public void Save(string path)
	{
		using var writer = new StreamWriter(path);

		writer.WriteLine(nameof(KeyForward) + " = " + KeyForward);
		writer.WriteLine(nameof(KeyBackward) + " = " + KeyBackward);
		writer.WriteLine(nameof(KeyStrafeLeft) + " = " + KeyStrafeLeft);
		writer.WriteLine(nameof(KeyStrafeRight) + " = " + KeyStrafeRight);
		writer.WriteLine(nameof(KeyFire) + " = " + KeyFire);
		writer.WriteLine(nameof(KeyUse) + " = " + KeyUse);
		writer.WriteLine(nameof(KeyRun) + " = " + KeyRun);

		writer.WriteLine(nameof(MouseSensitivity) + " = " + MouseSensitivity);

		writer.WriteLine(nameof(GameAlwaysRun) + " = " + GameAlwaysRun);

		writer.WriteLine(nameof(VideoScreenWidth) + " = " + VideoScreenWidth);
		writer.WriteLine(nameof(VideoScreenHeight) + " = " + VideoScreenHeight);
		writer.WriteLine(nameof(VideoFullscreen) + " = " + VideoFullscreen);
		writer.WriteLine(nameof(VideoHighResolution) + " = " + VideoHighResolution);
		writer.WriteLine(nameof(VideoDisplayMessage) + " = " + VideoDisplayMessage);
		writer.WriteLine(nameof(VideoGameScreenSize) + " = " + VideoGameScreenSize);
		writer.WriteLine(nameof(VideoGammaCorrection) + " = " + VideoGammaCorrection);
		writer.WriteLine(nameof(VideoFpsScale) + " = " + VideoFpsScale);

		writer.WriteLine(nameof(AudioSoundVolume) + " = " + AudioSoundVolume);
		writer.WriteLine(nameof(AudioMusicVolume) + " = " + AudioMusicVolume);
		writer.WriteLine(nameof(AudioRandomPitch) + " = " + AudioRandomPitch);
		writer.WriteLine(nameof(AudioSoundFont) + " = " + AudioSoundFont);
		writer.WriteLine(nameof(AudioMusicEffect) + " = " + AudioMusicEffect);
	}

	private static string GetString(Dictionary<string, string> configFile, string key, string defaultValue)
	{
		return configFile.GetValueOrDefault(key, defaultValue);
	}

	private static int GetInt(Dictionary<string, string> configFile, string key, int defaultValue)
	{
		return configFile.TryGetValue(key, out var stringValue) && int.TryParse(stringValue, out var value) ? value : defaultValue;
	}

	private static bool GetBool(Dictionary<string, string> configFile, string key, bool defaultValue)
	{
		return configFile.TryGetValue(key, out var stringValue) && bool.TryParse(stringValue, out var value) ? value : defaultValue;
	}

	private static KeyBinding GetKeyBinding(Dictionary<string, string> configFile, string key, KeyBinding defaultValue)
	{
		return configFile.TryGetValue(key, out var stringValue) ? KeyBinding.Parse(stringValue) : defaultValue;
	}
}
