/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using System.Globalization;

namespace ManagedDoom;

public static class ConfigUtilities
{
	private static readonly string[] WadNames = ["DOOM2.WAD", "PLUTONIA.WAD", "TNT.WAD", "DOOM.WAD", "DOOM1.WAD", "FREEDOOM2.WAD", "FREEDOOM1.WAD"];

	public static string DataPath => Directory.GetCurrentDirectory();
	public static string ConfigPath => Path.Combine(DataPath, "config.cfg");

	private static string GetDefaultWadPath()
	{
		return WadNames.FirstOrDefault(File.Exists) ?? throw new FileNotFoundException("No IWAD was found!");
	}

	public static bool IsIwad(string path)
	{
		return WadNames.Contains(Path.GetFileName(path).ToUpper(CultureInfo.InvariantCulture));
	}

	public static string[] GetWadPaths(CommandLineArgs args)
	{
		var wadPaths = new List<string> { args.Wad ?? GetDefaultWadPath() };

		wadPaths.AddRange(args.File);

		return [.. wadPaths];
	}
}
