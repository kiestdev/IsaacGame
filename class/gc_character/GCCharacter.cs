using Godot;
using System;

// This is a learning resource since I've not used godot jet
public partial class GCCharacter : CollisionShape2D
{   
    /*
        I don't think we'll be handling physics on the player since
        sliding would feel like a hockey puck.
    */
    private Vector2 directionalVelocity;
    private Vector2 counterStrafeV;
    
    // Public editable data
    public float DAcceleration = 0.0f;
    public float DDeceleration = 0.0f;
    public float DMaxSpeed = 0.0f;
    public bool DAllowCounterStrafe = false;
    private Vector2 PlayerMove()
    {
        
    }

    private void Pause();
    public override void _Input(InputEvent @event) {
        base._Ready();
        if (@event.IsActionPressed("pause")) Pause();
        if(@event.IsActionPressed("move")) PlayerMove();
    }
    public override void _Ready() {
        base._Ready();
        
    }
}
