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
	public static class Strings
	{
		public static readonly string PRESSKEY = "press a key.";
		public static readonly string PRESSYN = "press y or n.";
		public static readonly string QUITMSG = "are you sure you want to\nquit this great game?";
		public static readonly string LOADNET = "you can't do load while in a net game!\n\n" + PRESSKEY;
		public static readonly string QLOADNET = "you can't quickload during a netgame!\n\n" + PRESSKEY;
		public static readonly string QSAVESPOT = "you haven't picked a quicksave slot yet!\n\n" + PRESSKEY;
		public static readonly string SAVEDEAD = "you can't save if you aren't playing!\n\n" + PRESSKEY;
		public static readonly string QSPROMPT = "quicksave over your game named\n\n'%s'?\n\n" + PRESSYN;
		public static readonly string QLPROMPT = "do you want to quickload the game named\n\n'%s'?\n\n" + PRESSYN;

		public static readonly string NEWGAME =
			"you can't start a new game\n" +
			"while in a network game.\n\n" + PRESSKEY;

		public static readonly string NIGHTMARE =
			"are you sure? this skill level\n" +
			"isn't even remotely fair.\n\n" + PRESSYN;

		public static readonly string SWSTRING =
			"this is the shareware version of doom.\n\n" +
			"you need to order the entire trilogy.\n\n" + PRESSKEY;

		public static readonly string MSGOFF = "Messages OFF";
		public static readonly string MSGON = "Messages ON";
		public static readonly string NETEND = "you can't end a netgame!\n\n" + PRESSKEY;
		public static readonly string ENDGAME = "are you sure you want to end the game?\n\n" + PRESSYN;
		public static readonly string DOSY = "(press y to quit)";
		public static readonly string GAMMALVL0 = "Gamma correction OFF";
		public static readonly string GAMMALVL1 = "Gamma correction level 1";
		public static readonly string GAMMALVL2 = "Gamma correction level 2";
		public static readonly string GAMMALVL3 = "Gamma correction level 3";
		public static readonly string GAMMALVL4 = "Gamma correction level 4";
		public static readonly string EMPTYSTRING = "empty slot";
		public static readonly string GOTARMOR = "Picked up the armor.";
		public static readonly string GOTMEGA = "Picked up the MegaArmor!";
		public static readonly string GOTHTHBONUS = "Picked up a health bonus.";
		public static readonly string GOTARMBONUS = "Picked up an armor bonus.";
		public static readonly string GOTSTIM = "Picked up a stimpack.";
		public static readonly string GOTMEDINEED = "Picked up a medikit that you REALLY need!";
		public static readonly string GOTMEDIKIT = "Picked up a medikit.";
		public static readonly string GOTSUPER = "Supercharge!";
		public static readonly string GOTBLUECARD = "Picked up a blue keycard.";
		public static readonly string GOTYELWCARD = "Picked up a yellow keycard.";
		public static readonly string GOTREDCARD = "Picked up a red keycard.";
		public static readonly string GOTBLUESKUL = "Picked up a blue skull key.";
		public static readonly string GOTYELWSKUL = "Picked up a yellow skull key.";
		public static readonly string GOTREDSKULL = "Picked up a red skull key.";
		public static readonly string GOTINVUL = "Invulnerability!";
		public static readonly string GOTBERSERK = "Berserk!";
		public static readonly string GOTINVIS = "Partial Invisibility";
		public static readonly string GOTSUIT = "Radiation Shielding Suit";
		public static readonly string GOTMAP = "Computer Area Map";
		public static readonly string GOTVISOR = "Light Amplification Visor";
		public static readonly string GOTMSPHERE = "MegaSphere!";
		public static readonly string GOTCLIP = "Picked up a clip.";
		public static readonly string GOTCLIPBOX = "Picked up a box of bullets.";
		public static readonly string GOTROCKET = "Picked up a rocket.";
		public static readonly string GOTROCKBOX = "Picked up a box of rockets.";
		public static readonly string GOTCELL = "Picked up an energy cell.";
		public static readonly string GOTCELLBOX = "Picked up an energy cell pack.";
		public static readonly string GOTSHELLS = "Picked up 4 shotgun shells.";
		public static readonly string GOTSHELLBOX = "Picked up a box of shotgun shells.";
		public static readonly string GOTBACKPACK = "Picked up a backpack full of ammo!";
		public static readonly string GOTBFG9000 = "You got the BFG9000!  Oh, yes.";
		public static readonly string GOTCHAINGUN = "You got the chaingun!";
		public static readonly string GOTCHAINSAW = "A chainsaw!  Find some meat!";
		public static readonly string GOTLAUNCHER = "You got the rocket launcher!";
		public static readonly string GOTPLASMA = "You got the plasma gun!";
		public static readonly string GOTSHOTGUN = "You got the shotgun!";
		public static readonly string GOTSHOTGUN2 = "You got the super shotgun!";
		public static readonly string PD_BLUEO = "You need a blue key to activate this object";
		public static readonly string PD_REDO = "You need a red key to activate this object";
		public static readonly string PD_YELLOWO = "You need a yellow key to activate this object";
		public static readonly string PD_BLUEK = "You need a blue key to open this door";
		public static readonly string PD_REDK = "You need a red key to open this door";
		public static readonly string PD_YELLOWK = "You need a yellow key to open this door";
		public static readonly string GGSAVED = "game saved.";
		public static readonly string HUSTR_E1M1 = "E1M1: Hangar";
		public static readonly string HUSTR_E1M2 = "E1M2: Nuclear Plant";
		public static readonly string HUSTR_E1M3 = "E1M3: Toxin Refinery";
		public static readonly string HUSTR_E1M4 = "E1M4: Command Control";
		public static readonly string HUSTR_E1M5 = "E1M5: Phobos Lab";
		public static readonly string HUSTR_E1M6 = "E1M6: Central Processing";
		public static readonly string HUSTR_E1M7 = "E1M7: Computer Station";
		public static readonly string HUSTR_E1M8 = "E1M8: Phobos Anomaly";
		public static readonly string HUSTR_E1M9 = "E1M9: Military Base";
		public static readonly string HUSTR_E2M1 = "E2M1: Deimos Anomaly";
		public static readonly string HUSTR_E2M2 = "E2M2: Containment Area";
		public static readonly string HUSTR_E2M3 = "E2M3: Refinery";
		public static readonly string HUSTR_E2M4 = "E2M4: Deimos Lab";
		public static readonly string HUSTR_E2M5 = "E2M5: Command Center";
		public static readonly string HUSTR_E2M6 = "E2M6: Halls of the Damned";
		public static readonly string HUSTR_E2M7 = "E2M7: Spawning Vats";
		public static readonly string HUSTR_E2M8 = "E2M8: Tower of Babel";
		public static readonly string HUSTR_E2M9 = "E2M9: Fortress of Mystery";
		public static readonly string HUSTR_E3M1 = "E3M1: Hell Keep";
		public static readonly string HUSTR_E3M2 = "E3M2: Slough of Despair";
		public static readonly string HUSTR_E3M3 = "E3M3: Pandemonium";
		public static readonly string HUSTR_E3M4 = "E3M4: House of Pain";
		public static readonly string HUSTR_E3M5 = "E3M5: Unholy Cathedral";
		public static readonly string HUSTR_E3M6 = "E3M6: Mt. Erebus";
		public static readonly string HUSTR_E3M7 = "E3M7: Limbo";
		public static readonly string HUSTR_E3M8 = "E3M8: Dis";
		public static readonly string HUSTR_E3M9 = "E3M9: Warrens";
		public static readonly string HUSTR_E4M1 = "E4M1: Hell Beneath";
		public static readonly string HUSTR_E4M2 = "E4M2: Perfect Hatred";
		public static readonly string HUSTR_E4M3 = "E4M3: Sever The Wicked";
		public static readonly string HUSTR_E4M4 = "E4M4: Unruly Evil";
		public static readonly string HUSTR_E4M5 = "E4M5: They Will Repent";
		public static readonly string HUSTR_E4M6 = "E4M6: Against Thee Wickedly";
		public static readonly string HUSTR_E4M7 = "E4M7: And Hell Followed";
		public static readonly string HUSTR_E4M8 = "E4M8: Unto The Cruel";
		public static readonly string HUSTR_E4M9 = "E4M9: Fear";
		public static readonly string HUSTR_1 = "level 1: entryway";
		public static readonly string HUSTR_2 = "level 2: underhalls";
		public static readonly string HUSTR_3 = "level 3: the gantlet";
		public static readonly string HUSTR_4 = "level 4: the focus";
		public static readonly string HUSTR_5 = "level 5: the waste tunnels";
		public static readonly string HUSTR_6 = "level 6: the crusher";
		public static readonly string HUSTR_7 = "level 7: dead simple";
		public static readonly string HUSTR_8 = "level 8: tricks and traps";
		public static readonly string HUSTR_9 = "level 9: the pit";
		public static readonly string HUSTR_10 = "level 10: refueling base";
		public static readonly string HUSTR_11 = "level 11: 'o' of destruction!";
		public static readonly string HUSTR_12 = "level 12: the factory";
		public static readonly string HUSTR_13 = "level 13: downtown";
		public static readonly string HUSTR_14 = "level 14: the inmost dens";
		public static readonly string HUSTR_15 = "level 15: industrial zone";
		public static readonly string HUSTR_16 = "level 16: suburbs";
		public static readonly string HUSTR_17 = "level 17: tenements";
		public static readonly string HUSTR_18 = "level 18: the courtyard";
		public static readonly string HUSTR_19 = "level 19: the citadel";
		public static readonly string HUSTR_20 = "level 20: gotcha!";
		public static readonly string HUSTR_21 = "level 21: nirvana";
		public static readonly string HUSTR_22 = "level 22: the catacombs";
		public static readonly string HUSTR_23 = "level 23: barrels o' fun";
		public static readonly string HUSTR_24 = "level 24: the chasm";
		public static readonly string HUSTR_25 = "level 25: bloodfalls";
		public static readonly string HUSTR_26 = "level 26: the abandoned mines";
		public static readonly string HUSTR_27 = "level 27: monster condo";
		public static readonly string HUSTR_28 = "level 28: the spirit world";
		public static readonly string HUSTR_29 = "level 29: the living end";
		public static readonly string HUSTR_30 = "level 30: icon of sin";
		public static readonly string HUSTR_31 = "level 31: wolfenstein";
		public static readonly string HUSTR_32 = "level 32: grosse";
		public static readonly string PHUSTR_1 = "level 1: congo";
		public static readonly string PHUSTR_2 = "level 2: well of souls";
		public static readonly string PHUSTR_3 = "level 3: aztec";
		public static readonly string PHUSTR_4 = "level 4: caged";
		public static readonly string PHUSTR_5 = "level 5: ghost town";
		public static readonly string PHUSTR_6 = "level 6: baron's lair";
		public static readonly string PHUSTR_7 = "level 7: caughtyard";
		public static readonly string PHUSTR_8 = "level 8: realm";
		public static readonly string PHUSTR_9 = "level 9: abattoire";
		public static readonly string PHUSTR_10 = "level 10: onslaught";
		public static readonly string PHUSTR_11 = "level 11: hunted";
		public static readonly string PHUSTR_12 = "level 12: speed";
		public static readonly string PHUSTR_13 = "level 13: the crypt";
		public static readonly string PHUSTR_14 = "level 14: genesis";
		public static readonly string PHUSTR_15 = "level 15: the twilight";
		public static readonly string PHUSTR_16 = "level 16: the omen";
		public static readonly string PHUSTR_17 = "level 17: compound";
		public static readonly string PHUSTR_18 = "level 18: neurosphere";
		public static readonly string PHUSTR_19 = "level 19: nme";
		public static readonly string PHUSTR_20 = "level 20: the death domain";
		public static readonly string PHUSTR_21 = "level 21: slayer";
		public static readonly string PHUSTR_22 = "level 22: impossible mission";
		public static readonly string PHUSTR_23 = "level 23: tombstone";
		public static readonly string PHUSTR_24 = "level 24: the final frontier";
		public static readonly string PHUSTR_25 = "level 25: the temple of darkness";
		public static readonly string PHUSTR_26 = "level 26: bunker";
		public static readonly string PHUSTR_27 = "level 27: anti-christ";
		public static readonly string PHUSTR_28 = "level 28: the sewers";
		public static readonly string PHUSTR_29 = "level 29: odyssey of noises";
		public static readonly string PHUSTR_30 = "level 30: the gateway of hell";
		public static readonly string PHUSTR_31 = "level 31: cyberden";
		public static readonly string PHUSTR_32 = "level 32: go 2 it";
		public static readonly string THUSTR_1 = "level 1: system control";
		public static readonly string THUSTR_2 = "level 2: human bbq";
		public static readonly string THUSTR_3 = "level 3: power control";
		public static readonly string THUSTR_4 = "level 4: wormhole";
		public static readonly string THUSTR_5 = "level 5: hanger";
		public static readonly string THUSTR_6 = "level 6: open season";
		public static readonly string THUSTR_7 = "level 7: prison";
		public static readonly string THUSTR_8 = "level 8: metal";
		public static readonly string THUSTR_9 = "level 9: stronghold";
		public static readonly string THUSTR_10 = "level 10: redemption";
		public static readonly string THUSTR_11 = "level 11: storage facility";
		public static readonly string THUSTR_12 = "level 12: crater";
		public static readonly string THUSTR_13 = "level 13: nukage processing";
		public static readonly string THUSTR_14 = "level 14: steel works";
		public static readonly string THUSTR_15 = "level 15: dead zone";
		public static readonly string THUSTR_16 = "level 16: deepest reaches";
		public static readonly string THUSTR_17 = "level 17: processing area";
		public static readonly string THUSTR_18 = "level 18: mill";
		public static readonly string THUSTR_19 = "level 19: shipping/respawning";
		public static readonly string THUSTR_20 = "level 20: central processing";
		public static readonly string THUSTR_21 = "level 21: administration center";
		public static readonly string THUSTR_22 = "level 22: habitat";
		public static readonly string THUSTR_23 = "level 23: lunar mining project";
		public static readonly string THUSTR_24 = "level 24: quarry";
		public static readonly string THUSTR_25 = "level 25: baron's den";
		public static readonly string THUSTR_26 = "level 26: ballistyx";
		public static readonly string THUSTR_27 = "level 27: mount pain";
		public static readonly string THUSTR_28 = "level 28: heck";
		public static readonly string THUSTR_29 = "level 29: river styx";
		public static readonly string THUSTR_30 = "level 30: last call";
		public static readonly string THUSTR_31 = "level 31: pharaoh";
		public static readonly string THUSTR_32 = "level 32: caribbean";
		public static readonly string AMSTR_FOLLOWON = "Follow Mode ON";
		public static readonly string AMSTR_FOLLOWOFF = "Follow Mode OFF";
		public static readonly string AMSTR_GRIDON = "Grid ON";
		public static readonly string AMSTR_GRIDOFF = "Grid OFF";
		public static readonly string AMSTR_MARKEDSPOT = "Marked Spot";
		public static readonly string AMSTR_MARKSCLEARED = "All Marks Cleared";
		public static readonly string STSTR_MUS = "Music Change";
		public static readonly string STSTR_NOMUS = "IMPOSSIBLE SELECTION";
		public static readonly string STSTR_DQDON = "Degreelessness Mode On";
		public static readonly string STSTR_DQDOFF = "Degreelessness Mode Off";
		public static readonly string STSTR_KFAADDED = "Very Happy Ammo Added";
		public static readonly string STSTR_FAADDED = "Ammo (no keys) Added";
		public static readonly string STSTR_NCON = "No Clipping Mode ON";
		public static readonly string STSTR_NCOFF = "No Clipping Mode OFF";
		public static readonly string STSTR_BEHOLD = "inVuln, Str, Inviso, Rad, Allmap, or Lite-amp";
		public static readonly string STSTR_BEHOLDX = "Power-up Toggled";
		public static readonly string STSTR_CHOPPERS = "... doesn't suck - GM";
		public static readonly string STSTR_CLEV = "Changing Level...";

		public static readonly string E1TEXT =
			"Once you beat the big badasses and\n" +
			"clean out the moon base you're supposed\n" +
			"to win, aren't you? Aren't you? Where's\n" +
			"your fat reward and ticket home? What\n" +
			"the hell is this? It's not supposed to\n" +
			"end this way!\n" +
			"\n" +
			"It stinks like rotten meat, but looks\n" +
			"like the lost Deimos base.  Looks like\n" +
			"you're stuck on The Shores of Hell.\n" +
			"The only way out is through.\n" +
			"\n" +
			"To continue the DOOM experience, play\n" +
			"The Shores of Hell and its amazing\n" +
			"sequel, Inferno!\n";

		public static readonly string E2TEXT =
			"You've done it! The hideous cyber-\n" +
			"demon lord that ruled the lost Deimos\n" +
			"moon base has been slain and you\n" +
			"are triumphant! But ... where are\n" +
			"you? You clamber to the edge of the\n" +
			"moon and look down to see the awful\n" +
			"truth.\n" +
			"\n" +
			"Deimos floats above Hell itself!\n" +
			"You've never heard of anyone escaping\n" +
			"from Hell, but you'll make the bastards\n" +
			"sorry they ever heard of you! Quickly,\n" +
			"you rappel down to  the surface of\n" +
			"Hell.\n" +
			"\n" +
			"Now, it's on to the final chapter of\n" +
			"DOOM! -- Inferno.";

		public static readonly string E3TEXT =
			"The loathsome spiderdemon that\n" +
			"masterminded the invasion of the moon\n" +
			"bases and caused so much death has had\n" +
			"its ass kicked for all time.\n" +
			"\n" +
			"A hidden doorway opens and you enter.\n" +
			"You've proven too tough for Hell to\n" +
			"contain, and now Hell at last plays\n" +
			"fair -- for you emerge from the door\n" +
			"to see the green fields of Earth!\n" +
			"Home at last.\n" +
			"\n" +
			"You wonder what's been happening on\n" +
			"Earth while you were battling evil\n" +
			"unleashed. It's good that no Hell-\n" +
			"spawn could have come through that\n" +
			"door with you ...";

		public static readonly string E4TEXT =
			"the spider mastermind must have sent forth\n" +
			"its legions of hellspawn before your\n" +
			"final confrontation with that terrible\n" +
			"beast from hell.  but you stepped forward\n" +
			"and brought forth eternal damnation and\n" +
			"suffering upon the horde as a true hero\n" +
			"would in the face of something so evil.\n" +
			"\n" +
			"besides, someone was gonna pay for what\n" +
			"happened to daisy, your pet rabbit.\n" +
			"\n" +
			"but now, you see spread before you more\n" +
			"potential pain and gibbitude as a nation\n" +
			"of demons run amok among our cities.\n" +
			"\n" +
			"next stop, hell on earth!";

		public static readonly string C1TEXT =
			"YOU HAVE ENTERED DEEPLY INTO THE INFESTED\n" +
			"STARPORT. BUT SOMETHING IS WRONG. THE\n" +
			"MONSTERS HAVE BROUGHT THEIR OWN REALITY\n" +
			"WITH THEM, AND THE STARPORT'S TECHNOLOGY\n" +
			"IS BEING SUBVERTED BY THEIR PRESENCE.\n" +
			"\n" +
			"AHEAD, YOU SEE AN OUTPOST OF HELL, A\n" +
			"FORTIFIED ZONE. IF YOU CAN GET PAST IT,\n" +
			"YOU CAN PENETRATE INTO THE HAUNTED HEART\n" +
			"OF THE STARBASE AND FIND THE CONTROLLING\n" +
			"SWITCH WHICH HOLDS EARTH'S POPULATION\n" +
			"HOSTAGE.";

		public static readonly string C2TEXT =
			"YOU HAVE WON! YOUR VICTORY HAS ENABLED\n" +
			"HUMANKIND TO EVACUATE EARTH AND ESCAPE\n" +
			"THE NIGHTMARE.  NOW YOU ARE THE ONLY\n" +
			"HUMAN LEFT ON THE FACE OF THE PLANET.\n" +
			"CANNIBAL MUTATIONS, CARNIVOROUS ALIENS,\n" +
			"AND EVIL SPIRITS ARE YOUR ONLY NEIGHBORS.\n" +
			"YOU SIT BACK AND WAIT FOR DEATH, CONTENT\n" +
			"THAT YOU HAVE SAVED YOUR SPECIES.\n" +
			"\n" +
			"BUT THEN, EARTH CONTROL BEAMS DOWN A\n" +
			"MESSAGE FROM SPACE: \"SENSORS HAVE LOCATED\n" +
			"THE SOURCE OF THE ALIEN INVASION. IF YOU\n" +
			"GO THERE, YOU MAY BE ABLE TO BLOCK THEIR\n" +
			"ENTRY.  THE ALIEN BASE IS IN THE HEART OF\n" +
			"YOUR OWN HOME CITY, NOT FAR FROM THE\n" +
			"STARPORT.\" SLOWLY AND PAINFULLY YOU GET\n" +
			"UP AND RETURN TO THE FRAY.";

		public static readonly string C3TEXT =
			"YOU ARE AT THE CORRUPT HEART OF THE CITY,\n" +
			"SURROUNDED BY THE CORPSES OF YOUR ENEMIES.\n" +
			"YOU SEE NO WAY TO DESTROY THE CREATURES'\n" +
			"ENTRYWAY ON THIS SIDE, SO YOU CLENCH YOUR\n" +
			"TEETH AND PLUNGE THROUGH IT.\n" +
			"\n" +
			"THERE MUST BE A WAY TO CLOSE IT ON THE\n" +
			"OTHER SIDE. WHAT DO YOU CARE IF YOU'VE\n" +
			"GOT TO GO THROUGH HELL TO GET TO IT?";

		public static readonly string C4TEXT =
			"THE HORRENDOUS VISAGE OF THE BIGGEST\n" +
			"DEMON YOU'VE EVER SEEN CRUMBLES BEFORE\n" +
			"YOU, AFTER YOU PUMP YOUR ROCKETS INTO\n" +
			"HIS EXPOSED BRAIN. THE MONSTER SHRIVELS\n" +
			"UP AND DIES, ITS THRASHING LIMBS\n" +
			"DEVASTATING UNTOLD MILES OF HELL'S\n" +
			"SURFACE.\n" +
			"\n" +
			"YOU'VE DONE IT. THE INVASION IS OVER.\n" +
			"EARTH IS SAVED. HELL IS A WRECK. YOU\n" +
			"WONDER WHERE BAD FOLKS WILL GO WHEN THEY\n" +
			"DIE, NOW. WIPING THE SWEAT FROM YOUR\n" +
			"FOREHEAD YOU BEGIN THE LONG TREK BACK\n" +
			"HOME. REBUILDING EARTH OUGHT TO BE A\n" +
			"LOT MORE FUN THAN RUINING IT WAS.\n";

		public static readonly string C5TEXT =
			"CONGRATULATIONS, YOU'VE FOUND THE SECRET\n" +
			"LEVEL! LOOKS LIKE IT'S BEEN BUILT BY\n" +
			"HUMANS, RATHER THAN DEMONS. YOU WONDER\n" +
			"WHO THE INMATES OF THIS CORNER OF HELL\n" +
			"WILL BE.";

		public static readonly string C6TEXT =
			"CONGRATULATIONS, YOU'VE FOUND THE\n" +
			"SUPER SECRET LEVEL!  YOU'D BETTER\n" +
			"BLAZE THROUGH THIS ONE!\n";

		public static readonly string P1TEXT =
			"You gloat over the steaming carcass of the\n" +
			"Guardian.  With its death, you've wrested\n" +
			"the Accelerator from the stinking claws\n" +
			"of Hell.  You relax and glance around the\n" +
			"room.  Damn!  There was supposed to be at\n" +
			"least one working prototype, but you can't\n" +
			"see it. The demons must have taken it.\n" +
			"\n" +
			"You must find the prototype, or all your\n" +
			"struggles will have been wasted. Keep\n" +
			"moving, keep fighting, keep killing.\n" +
			"Oh yes, keep living, too.";

		public static readonly string P2TEXT =
			"Even the deadly Arch-Vile labyrinth could\n" +
			"not stop you, and you've gotten to the\n" +
			"prototype Accelerator which is soon\n" +
			"efficiently and permanently deactivated.\n" +
			"\n" +
			"You're good at that kind of thing.";

		public static readonly string P3TEXT =
			"You've bashed and battered your way into\n" +
			"the heart of the devil-hive.  Time for a\n" +
			"Search-and-Destroy mission, aimed at the\n" +
			"Gatekeeper, whose foul offspring is\n" +
			"cascading to Earth.  Yeah, he's bad. But\n" +
			"you know who's worse!\n" +
			"\n" +
			"Grinning evilly, you check your gear, and\n" +
			"get ready to give the bastard a little Hell\n" +
			"of your own making!";

		public static readonly string P4TEXT =
			"The Gatekeeper's evil face is splattered\n" +
			"all over the place.  As its tattered corpse\n" +
			"collapses, an inverted Gate forms and\n" +
			"sucks down the shards of the last\n" +
			"prototype Accelerator, not to mention the\n" +
			"few remaining demons.  You're done. Hell\n" +
			"has gone back to pounding bad dead folks \n" +
			"instead of good live ones.  Remember to\n" +
			"tell your grandkids to put a rocket\n" +
			"launcher in your coffin. If you go to Hell\n" +
			"when you die, you'll need it for some\n" +
			"final cleaning-up ...";

		public static readonly string P5TEXT =
			"You've found the second-hardest level we\n" +
			"got. Hope you have a saved game a level or\n" +
			"two previous.  If not, be prepared to die\n" +
			"aplenty. For master marines only.";

		public static readonly string P6TEXT =
			"Betcha wondered just what WAS the hardest\n" +
			"level we had ready for ya?  Now you know.\n" +
			"No one gets out alive.";

		public static readonly string T1TEXT =
			"You've fought your way out of the infested\n" +
			"experimental labs.   It seems that UAC has\n" +
			"once again gulped it down.  With their\n" +
			"high turnover, it must be hard for poor\n" +
			"old UAC to buy corporate health insurance\n" +
			"nowadays..\n" +
			"\n" +
			"Ahead lies the military complex, now\n" +
			"swarming with diseased horrors hot to get\n" +
			"their teeth into you. With luck, the\n" +
			"complex still has some warlike ordnance\n" +
			"laying around.";

		public static readonly string T2TEXT =
			"You hear the grinding of heavy machinery\n" +
			"ahead.  You sure hope they're not stamping\n" +
			"out new hellspawn, but you're ready to\n" +
			"ream out a whole herd if you have to.\n" +
			"They might be planning a blood feast, but\n" +
			"you feel about as mean as two thousand\n" +
			"maniacs packed into one mad killer.\n" +
			"\n" +
			"You don't plan to go down easy.";

		public static readonly string T3TEXT =
			"The vista opening ahead looks real damn\n" +
			"familiar. Smells familiar, too -- like\n" +
			"fried excrement. You didn't like this\n" +
			"place before, and you sure as hell ain't\n" +
			"planning to like it now. The more you\n" +
			"brood on it, the madder you get.\n" +
			"Hefting your gun, an evil grin trickles\n" +
			"onto your face. Time to take some names.";

		public static readonly string T4TEXT =
			"Suddenly, all is silent, from one horizon\n" +
			"to the other. The agonizing echo of Hell\n" +
			"fades away, the nightmare sky turns to\n" +
			"blue, the heaps of monster corpses start \n" +
			"to evaporate along with the evil stench \n" +
			"that filled the air. Jeeze, maybe you've\n" +
			"done it. Have you really won?\n" +
			"\n" +
			"Something rumbles in the distance.\n" +
			"A blue light begins to glow inside the\n" +
			"ruined skull of the demon-spitter.";

		public static readonly string T5TEXT =
			"What now? Looks totally different. Kind\n" +
			"of like King Tut's condo. Well,\n" +
			"whatever's here can't be any worse\n" +
			"than usual. Can it?  Or maybe it's best\n" +
			"to let sleeping gods lie..";

		public static readonly string T6TEXT =
			"Time for a vacation. You've burst the\n" +
			"bowels of hell and by golly you're ready\n" +
			"for a break. You mutter to yourself,\n" +
			"Maybe someone else can kick Hell's ass\n" +
			"next time around. Ahead lies a quiet town,\n" +
			"with peaceful flowing water, quaint\n" +
			"buildings, and presumably no Hellspawn.\n" +
			"\n" +
			"As you step off the transport, you hear\n" +
			"the stomp of a cyberdemon's iron shoe.";

		public static readonly string CC_ZOMBIE = "ZOMBIEMAN";
		public static readonly string CC_SHOTGUN = "SHOTGUN GUY";
		public static readonly string CC_HEAVY = "HEAVY WEAPON DUDE";
		public static readonly string CC_IMP = "IMP";
		public static readonly string CC_DEMON = "DEMON";
		public static readonly string CC_LOST = "LOST SOUL";
		public static readonly string CC_CACO = "CACODEMON";
		public static readonly string CC_HELL = "HELL KNIGHT";
		public static readonly string CC_BARON = "BARON OF HELL";
		public static readonly string CC_ARACH = "ARACHNOTRON";
		public static readonly string CC_PAIN = "PAIN ELEMENTAL";
		public static readonly string CC_REVEN = "REVENANT";
		public static readonly string CC_MANCU = "MANCUBUS";
		public static readonly string CC_ARCH = "ARCH-VILE";
		public static readonly string CC_SPIDER = "THE SPIDER MASTERMIND";
		public static readonly string CC_CYBER = "THE CYBERDEMON";
		public static readonly string CC_HERO = "OUR HERO";
	}
}
