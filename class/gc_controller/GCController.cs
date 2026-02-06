using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class GCController : Node {
    public int money;
    public int level;
    public int scoreHighest;
    public int rerollCount;
    private List<OwnedItem> ownedItems = new List<OwnedItem>();
    
    public void AddOwnedItem(OwnedItem item) {
        bool added = false;
        for (int i = 0; i < ownedItems.Count; i++) {
            if (added == true) {
                break;
            }
            if (ownedItems[i].type == item.type) {
                int add = ownedItems[i].quantity + item.quantity;
                OwnedItem a = ownedItems[i];
                a.quantity = add;
                ownedItems[i] = a;
                added = true;
            }
        }
        if (!added) {
            ownedItems.Add(item);
        }
    }
    public void AddMoney(int i){money = +i;}
    public void AddLevel(int i){level = +i;}
    public bool isHighestScore(int score) {
        if (score > scoreHighest) return true;
        else return false;
    }
    
    
}
