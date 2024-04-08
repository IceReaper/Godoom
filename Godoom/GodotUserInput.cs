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
using ManagedDoom.Doom.Event;
using ManagedDoom.Doom.Game;
using ManagedDoom.Doom.World;
using ManagedDoom.UserInput;

namespace Godoom;

public class GodotUserInput : IUserInput
{
	private readonly Config _config;
	private readonly GodotDoom _doom;

	private readonly bool _useMouse;

	private readonly bool[] _weaponKeys = new bool[7];

	private int _turnHeld;

	private bool _mouseGrabbed;
	private float _mouseX;
	private float _mouseY;
	private float _mousePrevX;
	private float _mousePrevY;
	private float _mouseDeltaX;
	private float _mouseDeltaY;

	public int MaxMouseSensitivity => 15;

	public int MouseSensitivity
	{
		get => _config.mouse_sensitivity;
		set => _config.mouse_sensitivity = value;
	}

	public GodotUserInput(Config config, Window window, GodotDoom doom, bool useMouse)
	{
		_config = config;
		_doom = doom;

		_useMouse = useMouse;

		window.WindowInput += OnInput;
	}

	private void OnInput(InputEvent @event)
	{
		switch (@event)
		{
			case InputEventKey { Pressed: true } keyEvent:
				_doom.Doom.PostEvent(new DoomEvent(EventType.KeyDown, GodotToDoom(keyEvent.KeyLabel)));

				break;

			case InputEventKey { Pressed: false } keyEvent:
				_doom.Doom.PostEvent(new DoomEvent(EventType.KeyUp, GodotToDoom(keyEvent.KeyLabel)));

				break;

			case InputEventMouseMotion mouseEvent when _useMouse && _mouseGrabbed:
			{
				_mouseX += mouseEvent.Relative.X;
				_mouseY += mouseEvent.Relative.Y;

				break;
			}
		}
	}

	public void BuildTicCmd(TicCmd cmd)
	{
		var keyForward = IsPressed(_config.key_forward);
		var keyBackward = IsPressed(_config.key_backward);
		var keyStrafeLeft = IsPressed(_config.key_strafeleft);
		var keyStrafeRight = IsPressed(_config.key_straferight);
		var keyTurnLeft = IsPressed(_config.key_turnleft);
		var keyTurnRight = IsPressed(_config.key_turnright);
		var keyFire = IsPressed(_config.key_fire);
		var keyUse = IsPressed(_config.key_use);
		var keyRun = IsPressed(_config.key_run);
		var keyStrafe = IsPressed(_config.key_strafe);

		_weaponKeys[0] = Input.IsKeyPressed(Key.Key1);
		_weaponKeys[1] = Input.IsKeyPressed(Key.Key2);
		_weaponKeys[2] = Input.IsKeyPressed(Key.Key3);
		_weaponKeys[3] = Input.IsKeyPressed(Key.Key4);
		_weaponKeys[4] = Input.IsKeyPressed(Key.Key5);
		_weaponKeys[5] = Input.IsKeyPressed(Key.Key6);
		_weaponKeys[6] = Input.IsKeyPressed(Key.Key7);

		cmd.Clear();

		var speed = keyRun != _config.game_alwaysrun ? 1 : 0;
		var forward = 0;
		var side = 0;

		_turnHeld = keyTurnLeft || keyTurnRight ? _turnHeld + 1 : 0;

		var turnSpeed = _turnHeld < PlayerBehavior.SlowTurnTics ? 2 : speed;

		if (keyStrafe)
		{
			if (keyTurnRight)
				side += PlayerBehavior.SideMove[speed];

			if (keyTurnLeft)
				side -= PlayerBehavior.SideMove[speed];
		}
		else
		{
			if (keyTurnRight)
				cmd.AngleTurn -= (short)PlayerBehavior.AngleTurn[turnSpeed];

			if (keyTurnLeft)
				cmd.AngleTurn += (short)PlayerBehavior.AngleTurn[turnSpeed];
		}

		if (keyForward)
			forward += PlayerBehavior.ForwardMove[speed];

		if (keyBackward)
			forward -= PlayerBehavior.ForwardMove[speed];

		if (keyStrafeLeft)
			side -= PlayerBehavior.SideMove[speed];

		if (keyStrafeRight)
			side += PlayerBehavior.SideMove[speed];

		if (keyFire)
			cmd.Buttons |= TicCmdButtons.Attack;

		if (keyUse)
			cmd.Buttons |= TicCmdButtons.Use;

		for (var i = 0; i < _weaponKeys.Length; i++)
		{
			if (!_weaponKeys[i])
				continue;

			cmd.Buttons |= TicCmdButtons.Change;
			cmd.Buttons |= (byte)(i << TicCmdButtons.WeaponShift);

			break;
		}

		UpdateMouse();

		var mouseX = (int)MathF.Round(_config.mouse_sensitivity * -_mouseDeltaX / 2);
		var mouseY = (int)MathF.Round(_config.mouse_sensitivity * _mouseDeltaY / 2);

		forward += mouseY;

		if (keyStrafe)
			side += mouseX * 2;
		else
			cmd.AngleTurn -= (short)(mouseX * 8);

		cmd.ForwardMove += (sbyte)Math.Clamp(forward, -PlayerBehavior.MaxMove, PlayerBehavior.MaxMove);
		cmd.SideMove += (sbyte)Math.Clamp(side, -PlayerBehavior.MaxMove, PlayerBehavior.MaxMove);
	}

	private bool IsPressed(KeyBinding keyBinding)
	{
		if (keyBinding.Keys.Any(static key => Input.IsKeyPressed(DoomToGodot(key))))
			return true;

		return _mouseGrabbed && keyBinding.MouseButtons.Any(static mouseButton => Input.IsMouseButtonPressed(DoomToGodot(mouseButton)));
	}

	public void Reset()
	{
		if (!_useMouse)
			return;

		_mouseX = 0;
		_mouseY = 0;
		_mousePrevX = _mouseX;
		_mousePrevY = _mouseY;
		_mouseDeltaX = 0;
		_mouseDeltaY = 0;
	}

	public void GrabMouse()
	{
		if (!_useMouse || _mouseGrabbed)
			return;

		Input.MouseMode = Input.MouseModeEnum.Captured;
		_mouseGrabbed = true;

		_mouseX = 0;
		_mouseY = 0;
		_mousePrevX = _mouseX;
		_mousePrevY = _mouseY;
		_mouseDeltaX = 0;
		_mouseDeltaY = 0;
	}

	public void ReleaseMouse()
	{
		if (!_useMouse || !_mouseGrabbed)
			return;

		Input.MouseMode = Input.MouseModeEnum.Visible;
		_mouseGrabbed = false;
	}

	private void UpdateMouse()
	{
		if (!_useMouse || !_mouseGrabbed)
			return;

		_mousePrevX = _mouseX;
		_mousePrevY = _mouseY;
		_mouseX = 0;
		_mouseY = 0;
		_mouseDeltaX = _mouseX - _mousePrevX;
		_mouseDeltaY = _mouseY - _mousePrevY;

		if (_config.mouse_disableyaxis)
			_mouseDeltaY = 0;
	}

	private static DoomKey GodotToDoom(Key key)
	{
		return key switch
		{
			Key.A => DoomKey.A,
			Key.B => DoomKey.B,
			Key.C => DoomKey.C,
			Key.D => DoomKey.D,
			Key.E => DoomKey.E,
			Key.F => DoomKey.F,
			Key.G => DoomKey.G,
			Key.H => DoomKey.H,
			Key.I => DoomKey.I,
			Key.J => DoomKey.J,
			Key.K => DoomKey.K,
			Key.L => DoomKey.L,
			Key.M => DoomKey.M,
			Key.N => DoomKey.N,
			Key.O => DoomKey.O,
			Key.P => DoomKey.P,
			Key.Q => DoomKey.Q,
			Key.R => DoomKey.R,
			Key.S => DoomKey.S,
			Key.T => DoomKey.T,
			Key.U => DoomKey.U,
			Key.V => DoomKey.V,
			Key.W => DoomKey.W,
			Key.X => DoomKey.X,
			Key.Y => DoomKey.Y,
			Key.Z => DoomKey.Z,
			Key.Key0 => DoomKey.Num0,
			Key.Key1 => DoomKey.Num1,
			Key.Key2 => DoomKey.Num2,
			Key.Key3 => DoomKey.Num3,
			Key.Key4 => DoomKey.Num4,
			Key.Key5 => DoomKey.Num5,
			Key.Key6 => DoomKey.Num6,
			Key.Key7 => DoomKey.Num7,
			Key.Key8 => DoomKey.Num8,
			Key.Key9 => DoomKey.Num9,
			Key.Escape => DoomKey.Escape,

			//// Key.CtrlLeft => DoomKey.LControl,
			//// Key.ShiftLeft => DoomKey.LShift,
			//// Key.AltLeft => DoomKey.LAlt,
			//// Key.MetaLeft => DoomKey.LSystem,
			//// Key.CtrlRight => DoomKey.RControl,
			//// Key.ShiftRight => DoomKey.RShift,
			//// Key.AltRight => DoomKey.RAlt,
			//// Key.MetaRight => DoomKey.RSystem,
			Key.Ctrl => DoomKey.LControl,
			Key.Shift => DoomKey.LShift,
			Key.Alt => DoomKey.LAlt,
			Key.Meta => DoomKey.LSystem,

			Key.Menu => DoomKey.Menu,
			Key.Bracketleft => DoomKey.LBracket,
			Key.Bracketright => DoomKey.RBracket,
			Key.Semicolon => DoomKey.Semicolon,
			Key.Comma => DoomKey.Comma,
			Key.Period => DoomKey.Period,
			Key.Quotedbl => DoomKey.Quote,
			Key.Slash => DoomKey.Slash,
			Key.Backslash => DoomKey.Backslash,
			Key.Asciitilde => DoomKey.Tilde,
			Key.Equal => DoomKey.Equal,
			Key.Underscore => DoomKey.Hyphen,
			Key.Space => DoomKey.Space,
			Key.Enter => DoomKey.Enter,
			Key.Backspace => DoomKey.Backspace,
			Key.Tab => DoomKey.Tab,
			Key.Pageup => DoomKey.PageUp,
			Key.Pagedown => DoomKey.PageDown,
			Key.End => DoomKey.End,
			Key.Home => DoomKey.Home,
			Key.Insert => DoomKey.Insert,
			Key.Delete => DoomKey.Delete,
			Key.KpAdd => DoomKey.Add,
			Key.KpSubtract => DoomKey.Subtract,
			Key.KpMultiply => DoomKey.Multiply,
			Key.KpDivide => DoomKey.Divide,
			Key.Left => DoomKey.Left,
			Key.Right => DoomKey.Right,
			Key.Up => DoomKey.Up,
			Key.Down => DoomKey.Down,
			Key.Kp0 => DoomKey.Numpad0,
			Key.Kp1 => DoomKey.Numpad1,
			Key.Kp2 => DoomKey.Numpad2,
			Key.Kp3 => DoomKey.Numpad3,
			Key.Kp4 => DoomKey.Numpad4,
			Key.Kp5 => DoomKey.Numpad5,
			Key.Kp6 => DoomKey.Numpad6,
			Key.Kp7 => DoomKey.Numpad7,
			Key.Kp8 => DoomKey.Numpad8,
			Key.Kp9 => DoomKey.Numpad9,
			Key.F1 => DoomKey.F1,
			Key.F2 => DoomKey.F2,
			Key.F3 => DoomKey.F3,
			Key.F4 => DoomKey.F4,
			Key.F5 => DoomKey.F5,
			Key.F6 => DoomKey.F6,
			Key.F7 => DoomKey.F7,
			Key.F8 => DoomKey.F8,
			Key.F9 => DoomKey.F9,
			Key.F10 => DoomKey.F10,
			Key.F11 => DoomKey.F11,
			Key.F12 => DoomKey.F12,
			Key.F13 => DoomKey.F13,
			Key.F14 => DoomKey.F14,
			Key.F15 => DoomKey.F15,
			Key.Pause => DoomKey.Pause,
			_ => DoomKey.Unknown
		};
	}

	private static Key DoomToGodot(DoomKey key)
	{
		return key switch
		{
			DoomKey.A => Key.A,
			DoomKey.B => Key.B,
			DoomKey.C => Key.C,
			DoomKey.D => Key.D,
			DoomKey.E => Key.E,
			DoomKey.F => Key.F,
			DoomKey.G => Key.G,
			DoomKey.H => Key.H,
			DoomKey.I => Key.I,
			DoomKey.J => Key.J,
			DoomKey.K => Key.K,
			DoomKey.L => Key.L,
			DoomKey.M => Key.M,
			DoomKey.N => Key.N,
			DoomKey.O => Key.O,
			DoomKey.P => Key.P,
			DoomKey.Q => Key.Q,
			DoomKey.R => Key.R,
			DoomKey.S => Key.S,
			DoomKey.T => Key.T,
			DoomKey.U => Key.U,
			DoomKey.V => Key.V,
			DoomKey.W => Key.W,
			DoomKey.X => Key.X,
			DoomKey.Y => Key.Y,
			DoomKey.Z => Key.Z,
			DoomKey.Num0 => Key.Key0,
			DoomKey.Num1 => Key.Key1,
			DoomKey.Num2 => Key.Key2,
			DoomKey.Num3 => Key.Key3,
			DoomKey.Num4 => Key.Key4,
			DoomKey.Num5 => Key.Key5,
			DoomKey.Num6 => Key.Key6,
			DoomKey.Num7 => Key.Key7,
			DoomKey.Num8 => Key.Key8,
			DoomKey.Num9 => Key.Key9,
			DoomKey.Escape => Key.Escape,

			//// DoomKey.LControl => Key.CtrlLeft,
			//// DoomKey.LShift => Key.ShiftLeft,
			//// DoomKey.LAlt => Key.AltLeft,
			//// DoomKey.LSystem => Key.MetaLeft,
			//// DoomKey.RControl => Key.CtrlRight,
			//// DoomKey.RShift => Key.ShiftRight,
			//// DoomKey.RAlt => Key.AltRight,
			//// DoomKey.RSystem => Key.MetaRight,
			DoomKey.LControl => Key.Ctrl,
			DoomKey.LShift => Key.Shift,
			DoomKey.LAlt => Key.Alt,
			DoomKey.LSystem => Key.Meta,

			DoomKey.Menu => Key.Menu,
			DoomKey.LBracket => Key.Bracketleft,
			DoomKey.RBracket => Key.Bracketright,
			DoomKey.Semicolon => Key.Semicolon,
			DoomKey.Comma => Key.Comma,
			DoomKey.Period => Key.Period,
			DoomKey.Quote => Key.Quotedbl,
			DoomKey.Slash => Key.Slash,
			DoomKey.Backslash => Key.Backslash,
			DoomKey.Tilde => Key.Asciitilde,
			DoomKey.Equal => Key.Equal,
			DoomKey.Hyphen => Key.Underscore,
			DoomKey.Space => Key.Space,
			DoomKey.Enter => Key.Enter,
			DoomKey.Backspace => Key.Backspace,
			DoomKey.Tab => Key.Tab,
			DoomKey.PageUp => Key.Pageup,
			DoomKey.PageDown => Key.Pagedown,
			DoomKey.End => Key.End,
			DoomKey.Home => Key.Home,
			DoomKey.Insert => Key.Insert,
			DoomKey.Delete => Key.Delete,
			DoomKey.Add => Key.KpAdd,
			DoomKey.Subtract => Key.KpSubtract,
			DoomKey.Multiply => Key.KpMultiply,
			DoomKey.Divide => Key.KpDivide,
			DoomKey.Left => Key.Left,
			DoomKey.Right => Key.Right,
			DoomKey.Up => Key.Up,
			DoomKey.Down => Key.Down,
			DoomKey.Numpad0 => Key.Kp0,
			DoomKey.Numpad1 => Key.Kp1,
			DoomKey.Numpad2 => Key.Kp2,
			DoomKey.Numpad3 => Key.Kp3,
			DoomKey.Numpad4 => Key.Kp4,
			DoomKey.Numpad5 => Key.Kp5,
			DoomKey.Numpad6 => Key.Kp6,
			DoomKey.Numpad7 => Key.Kp7,
			DoomKey.Numpad8 => Key.Kp8,
			DoomKey.Numpad9 => Key.Kp9,
			DoomKey.F1 => Key.F1,
			DoomKey.F2 => Key.F2,
			DoomKey.F3 => Key.F3,
			DoomKey.F4 => Key.F4,
			DoomKey.F5 => Key.F5,
			DoomKey.F6 => Key.F6,
			DoomKey.F7 => Key.F7,
			DoomKey.F8 => Key.F8,
			DoomKey.F9 => Key.F9,
			DoomKey.F10 => Key.F10,
			DoomKey.F11 => Key.F11,
			DoomKey.F12 => Key.F12,
			DoomKey.F13 => Key.F13,
			DoomKey.F14 => Key.F14,
			DoomKey.F15 => Key.F15,
			DoomKey.Pause => Key.Pause,
			_ => Key.Unknown
		};
	}

	private static MouseButton DoomToGodot(DoomMouseButton mouseButton)
	{
		return mouseButton switch
		{
			DoomMouseButton.Mouse1 => MouseButton.Left,
			DoomMouseButton.Mouse2 => MouseButton.Right,
			DoomMouseButton.Mouse3 => MouseButton.Middle,
			DoomMouseButton.Mouse4 => MouseButton.WheelUp,
			DoomMouseButton.Mouse5 => MouseButton.WheelDown,
			_ => MouseButton.None
		};
	}
}
