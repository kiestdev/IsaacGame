using Godot;
using System;

public partial class GCController : Node {
    public int money;
    public int level;
    public int scoreHighest;

    public void AddMoney(int i){money = +i;}

    public bool isHighestScore(int score) {
        if (score > scoreHighest) return true;
        else return false;
    }
    
    
}
