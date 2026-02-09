using Godot;
using System;

public partial class LevelUi : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_modeswitch_toggled(bool toggled)
	{
		GetNode<Node2D>("ItemHolder/Itemflow").Visible = !toggled;
		GetNode<Node2D>("ItemHolder/PowerFlow").Visible = toggled;
	}
}
