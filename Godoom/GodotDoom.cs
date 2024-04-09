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
using ManagedDoom.Doom;
using ManagedDoom.Doom.Game;
using ManagedDoom.Doom.Math;

namespace Godoom;

public class GodotDoom : IDisposable
{
	private readonly Config _config;
	private readonly GameContent _content;

	private readonly GodotVideo _video;
	private readonly GodotSound _sound;
	private readonly GodotMusic _music;

	public Doom Doom { get; }

	private readonly double _updateDelay;
	private readonly double _renderDelay;

	private readonly Camera3D _camera;

	private double _updateAccumulator;
	private double _renderAccumulator;

	public string QuitMessage => Doom.QuitMessage;
	public Exception? Exception { get; private set; }

	public GodotDoom(CommandLineArgs args, Window window, Node node)
	{
		_config = GodotConfigUtilities.GetConfig();
		_content = new GameContent(args);

		_video = new GodotVideo(_config, _content, window, node);
		_sound = new GodotSound(_config, _content, node);
		_music = new GodotMusic(_config, _content, node, _config.AudioSoundFont);
		var userInput = new GodotUserInput(_config, window, this);

		Doom = new Doom(args, _config, _content, _video, _sound, _music, userInput);

		_config.VideoFpsScale = Math.Clamp(_config.VideoFpsScale, 1, 100);

		_updateDelay = 1.0 / 35;
		_renderDelay = 1.0 / (35 * _config.VideoFpsScale);

		window.Size = new Vector2I(_config.VideoScreenWidth, _config.VideoScreenHeight);
		window.Mode = _config.VideoFullscreen ? Window.ModeEnum.Fullscreen : Window.ModeEnum.Windowed;
		window.MoveToCenter();

		node.AddChild(_camera = new Camera3D { Current = true });
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

			var world = Doom.Game.World ?? Doom.Opening.DemoGame?.World;
			var player = world?.DisplayPlayer.Mobj;

			_camera.Position = new Vector3(player?.X.ToFloat() ?? 0, player?.Z.ToFloat() ?? 0, player?.Y.ToFloat() ?? 0);
			_camera.RotationDegrees = new Vector3(0, (float)(player?.Angle.ToDegree() ?? 0) - 90, 0);

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

		_camera.Dispose();

		_music.Dispose();
		_sound.Dispose();
		_video.Dispose();

		_config.Save(ConfigUtilities.ConfigPath);

		_content.Dispose();
	}
}
