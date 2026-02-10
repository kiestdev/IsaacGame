using Godot;
using System;
using System.Collections.Generic;

// The base for every transaction made in the shop, Maps to DevIvem and OwnedItem
struct UserTransaction {
	public int selection;		// The contextual item. eg; 4 would equate to slot four of the options
	public string name;			// The item's formal name, NOT the user-end name
	public string description;	// the items USER END Descrition. not a formal identifier.
	public int price;			// the items USER END cost. not formal
	public int quantity;		// how many the player recieved
	public Item type;			// The Struct's Enum value
};

// Inventory Slot Data Structure
public struct OwnedItem {		
	public int quantity;		// how many of this type (for slot combining)
	public string name;			// the USER END name
	public string description;	// the USER END desc
	public Item type;			// the type, used primarilly for CMP funcs
}

// List of possible Services
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
	SERVICE_REROLL,
	SERVICE_UNDEFINED
}

// Developer's Item - Item modified only programatically by DEV made methods. Player will never interact with this 
public struct DevItem {
	public string ingame_name;	// The name shown
	public string formal_name;	// the 'class name' or node name
	public string description;	// the shown hover data
	public int price;			// cost for the client
	public Item type;			// Type
}

public partial class PShopInterface : Control {

	public Button LeaveShop, BuyButton, ClearButton;		// Clear is kill, Buy was supposed to be contextual to prevent misclicks
	public Label ItemTitle, ItemDesc, ItemQ, ItemPrice;		// Pointers to the now deleted Item Inspection 
	// These Two^ were written before the balatro style was added to the docs. Most of these vars are kill.
	
	List<DevItem> UserShopItemsRegistrar;		// List of possible Items that can be bought. These are not fixed so we can add things per level or per skill
	UserTransaction ut;							// The Active Transaction Context
	GCController usd;							// User Session Data, Data about the User and what they own and their state
	private Item[] currentPull = new Item[3];	// The four items for slots 1,2,3,4
	
	
	// Four Random Items occupy the current pull array
	public void Roll() {
		currentPull[0] = GetRandomItem();
		currentPull[1] = GetRandomItem();
		currentPull[2] = GetRandomItem();
		currentPull[3] = GetRandomItem();
	}
	
	// Accesses the 
	public void BuyReroll() {
		if (usd.money < 8 + usd.rerollCount) {		// USD owns client's money and how many times the reroll has happened
			GD.Print("Broke boy");				// Debug log, obv
		}
		else {
			usd.money -= (8 + usd.rerollCount);		// Buys the reroll
			usd.rerollCount++;						// Increment the Reroll for next purchase
			Roll();									// Rolls the things
		}
	}
	public override void _Ready() {
		base._Ready();
		//  A lot of these don't exist in the node tree because I was told that all the scenes would be preloaded. and modifired programatically
		//  These point to things that don't exist now, but WILL exist by the time this menu is even accessible.
		LeaveShop =     SFButtonAttach("PUpperDiv/BLeaveShop", bLeaveShop);	// Leave Button
		BuyButton =     SFButtonAttach("PLowerDiv/BBuy", bBuyItem);			// Contxtual Buy Button
		ItemTitle =     GetNode<Label>("PItemData/LTitle");					// DEPRECATED
		ItemDesc =      GetNode<Label>("PItemData/LDesc");					// DEPRECATED
		ItemQ =         GetNode<Label>("PItemData/LQ");						// DEPRECATED	
		ItemPrice =     GetNode<Label>("PItemData/LPrice");					// DEPRECATED
		usd =           GetNode<GCController>("UserSaveData");				// DEPRECATED
		SetProcess(true);														// enable per-frame
		currentPull[0] = GetRandomItem();											// Initializing Pull
		
		
	}
	
	// DEVONLY
	/*	This is called from any other file in the game so that we can dynamically create new items or the player to buy.
	 *	when entries collide, they update prices and descriptions. This is made for whatever reason we want. I know the game
	 *  doesn't have lore but that doesn't mean it won't in the future. 
	 *
	 *	Is it 2008 and the economy is $(ill_intent_word) itself?
	 *		- Hike all prices up 1000 percent
	 * 
	 *	Is the chair manufacturer filing for bankruptcy?
	 *		- Sell Less lawnchairs in the rolls
	 * 
	 *	Is the player not supposed to unlock cool powerups until they're done with the tutorial?
	 *		- Add a bool to the USD and check it in a separate file before admitting into the shop system
	 * 
	 *	rodata isn't limitless. That's why I didn't develop for it.
	 *	(as well I was 100% unaware JSON was event a native library in the engine)
	 *	This is an example of how that would be declared:
	 *
	 *	DevItem demoItem = new DevItem();
	 *	demoItem.ingame_name = "Super Cool thing that immediately wins the entire game";
	 *	demoItem.formal_name = "testItem01";
	 *	demoItem.description = "This is a test item, don't take seriously";
	 *	demoItem.type = Item.SERVICE_UNDEFINED;
	 *	Shop.RegisterPurchasableItem(demoItem);
	 */
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
	
	// Set data for the now-deprecated item info window
	public void _Process(float delta) {
		base._Process(delta);
		ut.price = UserShopItemsRegistrar[ut.selection].price * ut.quantity;
	}
	
	// BOTH OF THESE are self-explanitory by the symbol name
	public void SetItemTitle(String title) {
		ItemTitle.Text = title;
	}
	public void SetItemDesc(String desc) {
		ItemDesc.Text = desc;
	}

	// Switches thetransaction context to show for the newly selected object
	public void bSelectItem(int target) {
		if (ut.selection == target) return;
		ut.selection = target;
		SetItemTitle(UserShopItemsRegistrar[ut.selection].ingame_name);
		SetItemDesc(UserShopItemsRegistrar[ut.selection].description);
	}
	
	// Left this blank since this would likely be called by the game scene instead of itself
	public void bLeaveShop() {
		// ... idk the scene logic behind this
	}
	
	// Create the Owned Item type from the Transaction Context
	public OwnedItem Purchase() {
		usd.money -= ut.price;
		OwnedItem a; 
		a.name = ut.name;
		a.quantity = ut.quantity;
		a.type = ut.type;
		a.description = ut.description;
		return a;
	}
	
	// Check if we have money and confirm the purchase
	public void bBuyItem() {
		// ...
		if (usd.money < ut.price) {
			GD.PrintErr("is bankrupt");
		}
		else {
			usd.AddOwnedItem(Purchase());
		}
	}
	
	// Get Random Item for the Rolled Slots
	private Item GetRandomItem() {  
		Random random = new Random();
		Item i;
		i = (Item)random.Next(1, 12);
		return i;
	}
	
	// Macro, but #define in C# behaves differently than what im used to
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
