using Godot;
using ManagedDoom;

namespace Godoom;

public partial class GodotMain : Node3D
{
	private GodotDoom? _app;

	public override void _Ready()
	{
		// TODO this is required for the positional audio. We should refactor this into the game code!
		AddChild(new Camera3D { Current = true });

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
