/*
 * Copyright (c) The Godoom Developers and Contributors
 * This file is part of Godoom, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

using Godot;
using ManagedDoom;

namespace Godoom;

public class GodotDoom : IDisposable
{
	private readonly Config _config;
	private readonly GameContent _content;

	private readonly GodotVideo _video;
	private readonly GodotSound? _sound;
	private readonly GodotMusic? _music;

	public Doom Doom { get; }

	private readonly double _updateDelay;
	private readonly double _renderDelay;

	private double _updateAccumulator;
	private double _renderAccumulator;

	public string QuitMessage => Doom.QuitMessage;
	public Exception? Exception { get; private set; }

	public GodotDoom(CommandLineArgs args, Window window, Node3D node)
	{
		_config = GodotConfigUtilities.GetConfig();
		_content = new GameContent(args);

		_video = new GodotVideo(_config, _content, window, node);
		_sound = !args.nosound.Present && !args.nosfx.Present ? new GodotSound(_config, _content, node) : null;
		_music = !args.nosound.Present && !args.nomusic.Present ? GodotConfigUtilities.GetMusicInstance(_config, _content, node) : null;
		var userInput = new GodotUserInput(_config, window, this, !args.nomouse.Present);

		Doom = new Doom(args, _config, _content, _video, _sound, _music, userInput);

		var fpsScale = args.timedemo.Present ? 1 : _config.video_fpsscale;

		if (!args.timedemo.Present)
		{
			_config.video_fpsscale = Math.Clamp(_config.video_fpsscale, 1, 100);
			_updateDelay = 1.0 / 35;
			_renderDelay = 1.0 / (35 * fpsScale);
		}

		window.Size = new Vector2I(_config.video_screenwidth, _config.video_screenheight);
		window.Mode = _config.video_fullscreen ? Window.ModeEnum.Fullscreen : Window.ModeEnum.Windowed;
		window.MoveToCenter();
	}

	public bool OnUpdate(double delta)
	{
		try
		{
			_updateAccumulator += delta;
			_renderAccumulator += delta;

			while (_updateAccumulator >= _updateDelay)
			{
				_updateAccumulator -= _updateDelay;

				if (Doom.Update() == UpdateResult.Completed)
					return true;
			}

			while (_renderAccumulator >= _renderDelay)
			{
				_renderAccumulator -= _renderDelay;

				_video.Render(Doom, Fixed.FromDouble(_renderDelay));
			}
		}
		catch (Exception exception)
		{
			Exception = exception;

			return true;
		}

		return false;
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);

		_music?.Dispose();
		_sound?.Dispose();
		_video.Dispose();

		_config.Save(ConfigUtilities.GetConfigPath());

		_content.Dispose();
	}
}
