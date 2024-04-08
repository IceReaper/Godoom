/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Info;
using System.Runtime.ExceptionServices;

namespace ManagedDoom.Doom.Graphics;

public sealed class TextureAnimation
{
	private TextureAnimationInfo[] animations;

	public TextureAnimation(ITextureLookup textures, IFlatLookup flats)
	{
		try
		{
			Console.Write("Load texture animation info: ");

			var list = new List<TextureAnimationInfo>();

			foreach (var animDef in DoomInfo.TextureAnimation)
			{
				int picNum;
				int basePic;
				if (animDef.IsTexture)
				{
					if (textures.GetNumber(animDef.StartName) == -1)
					{
						continue;
					}

					picNum = textures.GetNumber(animDef.EndName);
					basePic = textures.GetNumber(animDef.StartName);
				}
				else
				{
					if (flats.GetNumber(animDef.StartName) == -1)
					{
						continue;
					}

					picNum = flats.GetNumber(animDef.EndName);
					basePic = flats.GetNumber(animDef.StartName);
				}

				var anim = new TextureAnimationInfo(
					animDef.IsTexture,
					picNum,
					basePic,
					picNum - basePic + 1,
					animDef.Speed);

				if (anim.NumPics < 2)
				{
					throw new Exception("Bad animation cycle from " + animDef.StartName + " to " + animDef.EndName + "!");
				}

				list.Add(anim);
			}

			animations = list.ToArray();

			Console.WriteLine("OK");
		}
		catch (Exception e)
		{
			Console.WriteLine("Failed");
			ExceptionDispatchInfo.Throw(e);
		}
	}

	public TextureAnimationInfo[] Animations => animations;
}
