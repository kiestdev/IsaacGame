using Godot;
using System;

public partial class BuyButton : Button
{
    public override void _Ready() {
        base._Ready();
        GD.Print("BuyButton::_Ready");
        Button b = (Button)GetNode("BuyButton");
        b.Pressed += Buy;
        b.Text = "Buy";
    }

   private void Buy() {
       GD.Print("BuyButton::Buy");
   }
    
}
