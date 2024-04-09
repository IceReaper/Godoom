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

public partial class GodotMain : Node3D
{
	private GodotDoom? _app;

	public override void _Ready()
	{
		_app = new GodotDoom(new CommandLineArgs(OS.GetCmdlineArgs()), GetWindow(), this);
	}

	public override void _Process(double delta)
	{
		if (_app?.Exception != null || _app?.OnUpdate(delta) == true)
			GetTree().Quit();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (_app?.Exception != null)
				GD.PrintErr(_app.Exception);
			else if (_app?.QuitMessage != null)
				GD.Print(_app.QuitMessage);

			_app?.Dispose();
		}

		base.Dispose(disposing);
	}
}
