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
using ManagedDoom.Video;
using Node = Godot.Node;

namespace Godoom;

public sealed class GodotVideo : IVideo, IDisposable
{
	private readonly Window _window;

	private readonly Renderer _renderer;

	private readonly byte[] _imageData;
	private readonly Image _image;
	private readonly ImageTexture _texture;
	private readonly TextureRect _textureRect;
	private readonly CanvasLayer _canvasLayer;

	public int WipeBandCount => _renderer.WipeBandCount;
	public int WipeHeight => _renderer.WipeHeight;

	public int MaxWindowSize => _renderer.MaxWindowSize;

	public int WindowSize
	{
		get => _renderer.WindowSize;
		set => _renderer.WindowSize = value;
	}

	public bool DisplayMessage
	{
		get => _renderer.DisplayMessage;
		set => _renderer.DisplayMessage = value;
	}

	public int MaxGammaCorrectionLevel => _renderer.MaxGammaCorrectionLevel;

	public int GammaCorrectionLevel
	{
		get => _renderer.GammaCorrectionLevel;
		set => _renderer.GammaCorrectionLevel = value;
	}

	public GodotVideo(Config config, GameContent content, Window window, Node node)
	{
		_window = window;

		_renderer = new Renderer(config, content);

		_imageData = new byte[4 * _renderer.Width * _renderer.Height];
		_image = Image.CreateFromData(_renderer.Width, _renderer.Height, false, Image.Format.Rgba8, _imageData);
		_texture = ImageTexture.CreateFromImage(_image);
		_textureRect = new TextureRect { Texture = _texture, TextureFilter = CanvasItem.TextureFilterEnum.Nearest };

		_canvasLayer = new CanvasLayer();
		_canvasLayer.AddChild(_textureRect);
		node.AddChild(_canvasLayer);

		_window.SizeChanged += Resize;

		Resize();
	}

	public void Render(Doom doom, Fixed frameFrac)
	{
		_renderer.Render(doom, _imageData, frameFrac);

		_image.SetData(_renderer.Height, _renderer.Width, false, Image.Format.Rgba8, _imageData);
		_image.Rotate90(ClockDirection.Clockwise);
		_image.FlipX();

		_texture.Update(_image);
	}

	public void Resize()
	{
		var scale = Math.Min(_window.Size.X / (float)_renderer.Width, _window.Size.Y / (float)_renderer.Height);

		_textureRect.Scale = new Vector2(scale, scale);
		_textureRect.Position = new Vector2(_window.Size.X - _renderer.Width * scale, _window.Size.Y - _renderer.Height * scale) / 2;
	}

	public void InitializeWipe()
	{
		_renderer.InitializeWipe();
	}

	public bool HasFocus()
	{
		return _window.HasFocus();
	}

	public void Dispose()
	{
		_canvasLayer.Dispose();
		_textureRect.Dispose();
		_texture.Dispose();
		_image.Dispose();
	}
}
