using Godot;
using System;

public partial class LShop : Node2D {
    public override void _Ready() {
        base._Ready();
        GD.Print("LShop::_Ready");
        SDefUserView = GetNode<PShopInterface>("PShopInterface");
    }

    private Control SDefUserView;

    public override void _ExitTree() {
        base._ExitTree();
        GD.Print("LShop::_ExitTree");
        /*
         * The classes don't have public deconstructors,
         * this code is memory unsafe and must be fixed
         * before shipping date and builds.
         * C# is also garbage so it may just do this
         * automatically without me knowing :/
         */
    }
}
