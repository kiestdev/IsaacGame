using Godot;
using System;
using System.Collections.Generic;

/*
 *  Usages:
 *      provide functions to the UI Interface
 */

struct UserTransaction {
    public int selection;
    public string name;
    public string description;
    public int price;
    public int quantity;
    public Item type;
};

public struct OwnedItem {
    public int quantity;
    public string name;
    public string description;
    public Item type;
}

public enum Item {
    POWER_WASP,
    POWER_HONEYBEE,
    POWER_BUMBLEBEE,
    POWER_HUMMINGBIRD,
    POWER_BUTTERFLY,
    POWER_MOTH,
    POWER_RED_LADYBUG,
    POWER_YELLOW_LADYBUG,
    ITEM_GNOME,
    ITEM_FOUNTAIN,
    ITEM_FERTILIZER,
    ITEM_LEAFBLOWER,
    ITEM_LAWNCHAIR,
    SERVICE_REROLL
}

public struct DevItem {
    public string ingame_name;
    public string formal_name;
    public string description;
    public int price;
    public Item type;
}

public partial class PShopInterface : Control {

    public Button LeaveShop, BuyButton, ClearButton;
    public Label ItemTitle, ItemDesc, ItemQ, ItemPrice;
    List<DevItem> UserShopItemsRegistrar;
    UserTransaction ut;
    GCController usd;
    private Item[] currentPull = new Item[3];
    
    public void Roll() {
        currentPull[0] = GetRandomItem();
        currentPull[1] = GetRandomItem();
        currentPull[2] = GetRandomItem();
        currentPull[3] = GetRandomItem();
    }
    public void BuyReroll() {
        if (usd.money < 8 + usd.rerollCount) {
            GD.Print("Broke boy");
        }
        else {
            usd.money -= (8 + usd.rerollCount);
            usd.rerollCount++;
            Roll();
        }
    }
    public override void _Ready() {
        base._Ready();
        LeaveShop =     SFButtonAttach("PUpperDiv/BLeaveShop", bLeaveShop);
        BuyButton =     SFButtonAttach("PLowerDiv/BBuy", bBuyItem);
        ItemTitle =     GetNode<Label>("PItemData/LTitle");
        ItemDesc =      GetNode<Label>("PItemData/LDesc");
        ItemQ =         GetNode<Label>("PItemData/LQ");
        ItemPrice =     GetNode<Label>("PItemData/LPrice");
        usd =           GetNode<GCController>("UserSaveData");
        SetProcess(true);
        currentPull[0] = GetRandomItem();
    }
    public void RegisterPurchasableItem(DevItem item) {
        foreach (DevItem devItem in UserShopItemsRegistrar) {
            if (devItem.formal_name == item.formal_name) {
                UserShopItemsRegistrar.Remove(devItem);
            }
        }
        UserShopItemsRegistrar.Add(item);
        // Readds the item so, if we want, we can the price and descriptions from elsewhere
        // bazinga! working economy manager!
        GD.Print("Registered Type:" + item.formal_name);
    }
    public void _Process(float delta) {
        base._Process(delta);
        ut.price = UserShopItemsRegistrar[ut.selection].price * ut.quantity;
    }
    public void SetItemTitle(String title) {
        ItemTitle.Text = title;
    }
    public void SetItemDesc(String desc) {
        ItemDesc.Text = desc;
    }
    public void bSelectItem(int target) {
        if (ut.selection == target) return;
        ut.selection = target;
        SetItemTitle(UserShopItemsRegistrar[ut.selection].ingame_name);
        SetItemDesc(UserShopItemsRegistrar[ut.selection].description);
    }
    public void bLeaveShop() {
        // ... idk the scene logic behind this
    }
    public OwnedItem Purchase() {
        usd.money -= ut.price;
        OwnedItem a; 
        a.name = ut.name;
        a.quantity = ut.quantity;
        a.type = ut.type;
        a.description = ut.description;
        return a;
    }
    public void bBuyItem() {
        // ...
        if (usd.money < ut.price) {
            GD.PrintErr("is bankrupt");
        }
        else {
            usd.AddOwnedItem(Purchase());
        }
    }
    private Item GetRandomItem() {  
        Random random = new Random();
        Item i;
        i = (Item)random.Next(1, 12);
        return i;
    }
    private Button SFButtonAttach(String name, Action callback) {
        Button button = GetNode<Button>(name);
        if (button == null) {
            GD.PrintErr("Item doesnt exist" + name);
            return null;
        } 
        button.Pressed += callback;
        return button;
    }
}
