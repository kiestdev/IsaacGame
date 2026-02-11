using Godot;
using System;

public partial class mouse : Control
{
	public override void _Ready()
	{
		// Load the custom images for the mouse cursor.
		var arrow = ResourceLoader.Load("res://art/ui/mouse.png");
		var beam = ResourceLoader.Load("res://art/ui/mouse.png");
		var click = ResourceLoader.Load("res://art/ui/mouseclick.png");

		// Changes only the arrow shape of the cursor.
		// This is similar to changing it in the project settings.
		Input.SetCustomMouseCursor(arrow);

		// Changes a specific shape of the cursor (here, the I-beam shape).
		Input.SetCustomMouseCursor(beam, Input.CursorShape.Ibeam);
		
		// Change click shape
		Input.SetCustomMouseCursor(click, Input.CursorShape.PointingHand);
	}
}
