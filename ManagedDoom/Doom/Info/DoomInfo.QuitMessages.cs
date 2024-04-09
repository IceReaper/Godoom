/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using ManagedDoom.Doom.Common;

namespace ManagedDoom.Doom.Info;

public static partial class DoomInfo
{
	public static class QuitMessages
	{
		public static readonly IReadOnlyList<string> Doom = new string[]
		{
			Strings.QUITMSG,
			"please don't leave, there's more\ndemons to toast!",
			"let's beat it -- this is turning\ninto a bloodbath!",
			"i wouldn't leave if i were you.\ndos is much worse.",
			"you're trying to say you like dos\nbetter than me, right?",
			"don't leave yet -- there's a\ndemon around that corner!",
			"ya know, next time you come in here\ni'm gonna toast ya.",
			"go ahead and leave. see if i care."
		};

		public static readonly IReadOnlyList<string> Doom2 = new string[]
		{
			"you want to quit?\nthen, thou hast lost an eighth!",
			"don't go now, there's a \ndimensional shambler waiting\nat the dos prompt!",
			"get outta here and go back\nto your boring programs.",
			"if i were your boss, i'd \n deathmatch ya in a minute!",
			"look, bud. you leave now\nand you forfeit your body count!",
			"just leave. when you come\nback, i'll be waiting with a bat.",
			"you're lucky i don't smack\nyou for thinking about leaving."
		};

		public static readonly IReadOnlyList<string> FinalDoom = new string[]
		{
			"fuck you, pussy!\nget the fuck out!",
			"you quit and i'll jizz\nin your cystholes!",
			"if you leave, i'll make\nthe lord drink my jizz.",
			"hey, ron! can we say\n'fuck' in the game?",
			"i'd leave: this is just\nmore monsters and levels.\nwhat a load.",
			"suck it down, asshole!\nyou're a fucking wimp!",
			"don't quit now! we're \nstill spending your money!"
		};
	}
}
