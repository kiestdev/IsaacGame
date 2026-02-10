using Godot;
using System;

public enum ItemType 
{
	Gnome,
	Fountain,
	Fertile,
	LeafBlower,
	Chair,
	misc
}

public partial class ItemHolder : Control
{
	ItemType item = ItemType.misc ; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		switch(item)
		{
			case ItemType.Gnome:
			{
				GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/gnome_fixed.png");
				break;
			}
			case ItemType.Fountain:
			{	
				GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/fountain_small.png");
				break;
			}
			case ItemType.Fertile:
			{
				GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/fertalizer_fixed.png");
				break;
			}
			case ItemType.Chair:
			{
				GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/items/lawnchair.png");
				break;
			}
			case ItemType.LeafBlower:
			{
				GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/items/leafblower.png");
				break;
			}
			case ItemType.misc:
			{
				GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/Big_coin_fixed.png");
				break;
			}
			
		}
		string texKey = (string)GetMeta("texVal");
		if(texKey == "gnome"){ GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/gnome_fixed.png"); }
		else if(texKey == "Fountain"){ GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/gnome_fixed.png"); }
		else if(texKey == "Fertile"){ GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/fertalizer_fixed.png"); }
		else if(texKey == "LeafBlower"){ GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/items/leafblower.png"); }
		else if(texKey == "Chair"){ GetNode<Sprite2D>("Itemsprite").Texture = (Texture2D)ResourceLoader.Load("res://art/items/lawnchair.png"); }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_control_mouse_entered()
	{
		GetNode<Node2D>("Tooltip").Visible = true;
	}
	public void _on_control_mouse_exited()
	{
		GetNode<Node2D>("Tooltip").Visible = false;
	}
}
