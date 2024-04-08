# Godoom

Godoom is a Godot fork of [Managed Doom](https://github.com/sinshu/managed-doom) by sinshu focused on:
1. Migration to Godot
2. Modernizing the codebase
3. Replacing the 2d renderer with Godot's 3d renderer
4. Adding support for multiplayer
5. Supporting custom games and mods
6. Adding quality of life improvements

Compatibility with the original Doom games is not a priority, however it will always be supported.
Due to several changes in the codebase, the gameplay experience may slightly differ.

## Roadmap
Not in any particular order and without any time schedule.

- [x] Port the game to Godot
- [ ] Implement modern c# code standards
- [ ] Implement advanced 3d rendering
- [ ] Implement assets from folder
- [ ] Implement assets from zip
- [ ] Implement png texture support
- [ ] Implement wav sound support
- [ ] Implement game controller support
- [ ] Implement touch screen support
- [ ] Implement multiplayer
- [ ] Implement mod support

And whatever people suggest which sounds good enough to be added here :)

## License

Godoom is distributed under the [GPLv3 license](COPYING).

Godoom uses the following nuget libraries:
* [MeltySynth](https://github.com/sinshu/meltysynth/) ([MIT license](licenses/LICENSE_MeltySynth.txt))

Godoom uses the following bundled libraries:
* [TimGM6mb](https://musescore.org/en/handbook/soundfonts-and-sfz-files) ([GPLv2 license](licenses/LICENSE_TimGM6mb.txt))

## Special Thanks

[sinshu](https://github.com/sinshu): Thank you so much for the effort of porting Doom to C# which made this project possible!
