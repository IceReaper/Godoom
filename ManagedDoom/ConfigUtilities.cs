/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom;

public static class ConfigUtilities
{
	private static readonly string[] iwadNames = new string[]
	{
		"DOOM2.WAD",
		"PLUTONIA.WAD",
		"TNT.WAD",
		"DOOM.WAD",
		"DOOM1.WAD",
		"FREEDOOM2.WAD",
		"FREEDOOM1.WAD"
	};

	public static string GetExeDirectory()
	{
		return Directory.GetCurrentDirectory();
	}

	public static string GetConfigPath()
	{
		return Path.Combine(GetExeDirectory(), "managed-doom.cfg");
	}

	public static string GetDefaultIwadPath()
	{
		var exeDirectory = GetExeDirectory();
		foreach (var name in iwadNames)
		{
			var path = Path.Combine(exeDirectory, name);
			if (File.Exists(path))
			{
				return path;
			}
		}

		var currentDirectory = Directory.GetCurrentDirectory();
		foreach (var name in iwadNames)
		{
			var path = Path.Combine(currentDirectory, name);
			if (File.Exists(path))
			{
				return path;
			}
		}

		throw new Exception("No IWAD was found!");
	}

	public static bool IsIwad(string path)
	{
		var name = Path.GetFileName(path).ToUpper();
		return iwadNames.Contains(name);
	}

	public static string[] GetWadPaths(CommandLineArgs args)
	{
		var wadPaths = new List<string>();

		if (args.iwad.Present)
		{
			wadPaths.Add(args.iwad.Value);
		}
		else
		{
			wadPaths.Add(GetDefaultIwadPath());
		}

		if (args.file.Present)
		{
			foreach (var path in args.file.Value)
			{
				wadPaths.Add(path);
			}
		}

		return wadPaths.ToArray();
	}
}
