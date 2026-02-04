using Godot;
using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("printedq");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile(("res://class/p_environment/world.tscn"));
	}

	public async void _on_quit_pressed()
    {
		GetTree().Quit();
		GD.Print("printed");
	}
}
