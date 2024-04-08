/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace ManagedDoom;

public sealed class CommandLineArgs
{
	public string? Wad { get; }
	public string[] File { get; }

	public CommandLineArgs(IReadOnlyCollection<string> args)
	{
		Wad = GetString(args, "-iwad");
		File = Check_file(args);

		// Check for drag & drop.
		if (args.Count == 0 || !args.All(static arg => arg.FirstOrDefault() != '-'))
			return;

		string? iWadPath = null;
		var pWadPaths = new List<string>();

		foreach (var path in args.Where(static path => Path.GetExtension(path).Equals(".wad", StringComparison.OrdinalIgnoreCase)))
		{
			if (ConfigUtilities.IsIwad(path))
				iWadPath = path;
			else
				pWadPaths.Add(path);
		}

		if (iWadPath != null)
			Wad = iWadPath;

		if (pWadPaths.Count > 0)
			File = [.. pWadPaths];
	}

	private static string[] Check_file(IEnumerable<string> args)
	{
		var values = GetValues(args, "-file");

		return values.Length >= 1 ? values : [];
	}

	private static string? GetString(IEnumerable<string> args, string name)
	{
		var values = GetValues(args, name);

		return values.Length == 1 ? values[0] : null;
	}

	private static string[] GetValues(IEnumerable<string> args, string name)
	{
		return args
			.SkipWhile(arg => arg != name)
			.Skip(1)
			.TakeWhile(static arg => arg[0] != '-')
			.ToArray();
	}
}
