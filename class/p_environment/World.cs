using Godot;
using System;

public partial class World : Node2D
{
	PackedScene shop = GD.Load<PackedScene>("res://class/gc_shop/PShopInterface.tscn");
	PackedScene level = GD.Load<PackedScene>("res://class/p_environment/Llevel.tscn");
	public int currency = 0;
	public int stage = 0;
	public Node curScene ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		curScene = GetNode<Node>("LLevel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_child_exiting_tree(Node node)
	{
		if(node.Name == "LLevel"){
			Node instance = shop.Instantiate();
			AddChild(instance);
			curScene = instance;
		}
		else if(node.Name == "Shop")
		{
			Node instance = level.Instantiate();
			AddChild(instance);
			curScene = instance;
		}
	}
}
