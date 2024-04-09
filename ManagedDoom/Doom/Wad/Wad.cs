/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Common;
using ManagedDoom.Doom.Game;
using System.Globalization;
using System.Runtime.ExceptionServices;

namespace ManagedDoom.Doom.Wad;

public sealed class Wad : IDisposable
{
	private readonly List<string> _names = [];
	private readonly List<Stream> _streams = [];
	private readonly List<LumpInfo> _lumpInfos = [];

	public IReadOnlyList<LumpInfo> LumpInfos => _lumpInfos;

	public GameVersion GameVersion { get; }
	public GameMode GameMode { get; }
	public MissionPack MissionPack { get; }

	public Wad(params string[] fileNames)
	{
		try
		{
			Console.Write("Open WAD files: ");

			foreach (var fileName in fileNames)
				AddFile(fileName);

			GameMode = GetGameMode(_names);
			MissionPack = GetMissionPack(_names);
			GameVersion = GetGameVersion(_names);

			Console.WriteLine("OK (" + string.Join(", ", fileNames.Select(Path.GetFileName)) + ")");
		}
		catch (Exception e)
		{
			Console.WriteLine("Failed");
			Dispose();
			ExceptionDispatchInfo.Throw(e);
		}
	}

	private void AddFile(string fileName)
	{
		_names.Add(Path.GetFileNameWithoutExtension(fileName).ToLower(CultureInfo.InvariantCulture));

		var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
		_streams.Add(stream);

		var data = new byte[12];

		if (stream.Read(data, 0, data.Length) != data.Length)
			throw new AggregateException("Failed to read the WAD file.");

		var identification = DoomInterop.ToString(data, 0, 4);
		var lumpCount = BitConverter.ToInt32(data, 4);
		var lumpInfoTableOffset = BitConverter.ToInt32(data, 8);

		if (identification != "IWAD" && identification != "PWAD")
			throw new AggregateException("The file is not a WAD file.");

		data = new byte[LumpInfo.DataSize * lumpCount];
		stream.Seek(lumpInfoTableOffset, SeekOrigin.Begin);

		if (stream.Read(data, 0, data.Length) != data.Length)
			throw new AggregateException("Failed to read the WAD file.");

		for (var i = 0; i < lumpCount; i++)
		{
			var offset = LumpInfo.DataSize * i;

			var lumpInfo = new LumpInfo(
				DoomInterop.ToString(data, offset + 8, 8),
				stream,
				BitConverter.ToInt32(data, offset),
				BitConverter.ToInt32(data, offset + 4)
			);

			_lumpInfos.Add(lumpInfo);
		}
	}

	public int GetLumpNumber(string name)
	{
		for (var i = _lumpInfos.Count - 1; i >= 0; i--)
		{
			if (_lumpInfos[i].Name == name)
				return i;
		}

		return -1;
	}

	public int GetLumpSize(int number)
	{
		return _lumpInfos[number].Size;
	}

	public byte[] ReadLump(int number)
	{
		var lumpInfo = _lumpInfos[number];

		var data = new byte[lumpInfo.Size];

		lumpInfo.Stream.Seek(lumpInfo.Position, SeekOrigin.Begin);
		var read = lumpInfo.Stream.Read(data, 0, lumpInfo.Size);

		if (read != lumpInfo.Size)
			throw new AggregateException("Failed to read the lump " + number + ".");

		return data;
	}

	public byte[] ReadLump(string name)
	{
		var lumpNumber = GetLumpNumber(name);

		if (lumpNumber == -1)
			throw new AggregateException("The lump '" + name + "' was not found.");

		return ReadLump(lumpNumber);
	}

	public void Dispose()
	{
		Console.WriteLine("Close WAD files.");

		foreach (var stream in _streams)
			stream.Dispose();

		_streams.Clear();
	}

	private static GameVersion GetGameVersion(IEnumerable<string> names)
	{
		foreach (var name in names)
		{
			switch (name.ToLower(CultureInfo.InvariantCulture))
			{
				case "doom2":
				case "freedoom2":
					return GameVersion.Version109;

				case "doom":
				case "doom1":
				case "freedoom1":
					return GameVersion.Ultimate;

				case "plutonia":
				case "tnt":
					return GameVersion.Final;
			}
		}

		return GameVersion.Version109;
	}

	private static GameMode GetGameMode(IEnumerable<string> names)
	{
		foreach (var name in names)
		{
			switch (name.ToLower(CultureInfo.InvariantCulture))
			{
				case "doom2":
				case "plutonia":
				case "tnt":
				case "freedoom2":
					return GameMode.Commercial;

				case "doom":
				case "freedoom1":
					return GameMode.Retail;

				case "doom1":
					return GameMode.Shareware;
			}
		}

		return GameMode.Indetermined;
	}

	private static MissionPack GetMissionPack(IEnumerable<string> names)
	{
		foreach (var name in names)
		{
			switch (name.ToLower(CultureInfo.InvariantCulture))
			{
				case "plutonia":
					return MissionPack.Plutonia;

				case "tnt":
					return MissionPack.Tnt;
			}
		}

		return MissionPack.Doom2;
	}
}
