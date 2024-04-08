/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Graphics;

namespace ManagedDoom.Doom.Game;

public sealed class GameContent : IDisposable
{
	private Wad.Wad wad;
	private Palette palette;
	private ColorMap colorMap;
	private ITextureLookup textures;
	private IFlatLookup flats;
	private ISpriteLookup sprites;
	private TextureAnimation animation;

	private GameContent()
	{
	}

	public GameContent(CommandLineArgs args)
	{
		wad = new Wad.Wad(ConfigUtilities.GetWadPaths(args));

		DeHackEd.Initialize(args, wad);

		palette = new Palette(wad);
		colorMap = new ColorMap(wad);
		textures = new TextureLookup(wad);
		flats = new FlatLookup(wad);
		sprites = new SpriteLookup(wad);
		animation = new TextureAnimation(textures, flats);
	}

	public void Dispose()
	{
		if (wad != null)
		{
			wad.Dispose();
			wad = null;
		}
	}

	public Wad.Wad Wad => wad;
	public Palette Palette => palette;
	public ColorMap ColorMap => colorMap;
	public ITextureLookup Textures => textures;
	public IFlatLookup Flats => flats;
	public ISpriteLookup Sprites => sprites;
	public TextureAnimation Animation => animation;
}
