using Godot;
using System;

public partial class Llevel : Node2D
{
	private Node2D parent ;
	private int stage ;
	private int minimum ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = (Node2D)GetParent().GetParent() ;
		GD.Print(parent.Name);
		stage = (int)parent.GetMeta("Stage"); 
		minimum = (int)Math.Round((Math.Pow(2,stage)/10)+Math.Pow(stage,2)+10) ;
		GetNode<Label>("Level UI/ScoreContain/MinPer").Text = minimum.ToString() + "%";
		GD.Print("stage: ", stage, " | Percent: ", minimum);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
