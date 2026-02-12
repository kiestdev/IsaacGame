using Godot;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

public partial class Llevel : Node2D
{
	PackedScene tetro = GD.Load<PackedScene>("res://class/p_environment/tetromino.tscn");
	private Node2D parent ;
	private int stage ;
	private int minimum ;
	private int reward = 3;
	private int Percent = 0;
	private Node2D TetroMain1 ;
	private Node2D TetroMain2 ;
	private Node2D TetroMain3 ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = (Node2D)GetParent() ;
		GD.Print(parent.Name);
		stage = (int)parent.GetMeta("Stage"); 
		minimum = (int)Math.Round((Math.Pow(2,stage)/10)+Math.Pow(stage,2)+10) ;
		GetNode<Label>("Level UI/ScoreContain/MinPer").Text = minimum.ToString() + "%";
		GetNode<Label>("Level UI/StageContain/StageNum").Text = stage.ToString();
		GD.Print("stage: ", stage, " | Percent: ", minimum);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Percent >= minimum)
		{
			parent.SetMeta("Currency",(int)parent.GetMeta("Currency")+reward);
		}
	}

	public void spawnTetromino(Marker2D used)
	{
		Control instance = (Control)tetro.Instantiate();
		AddChild(instance);
		instance.Position = used.Position ;
		instance.Rotation = Mathf.DegToRad(90);
		instance.Scale = new Vector2(0.2f,0.2f);
		TetroMain1.scale
	}
	public void _on_tetro_1_signal_button_down(){spawnTetromino(GetNode<Marker2D>("Marker2D"));}
	public void _on_tetro_2_signal_button_down(){spawnTetromino(GetNode<Marker2D>("Marker2D2"));}
	public void _on_tetro_3_signal_button_down(){spawnTetromino(GetNode<Marker2D>("Marker2D3"));}
	
}
