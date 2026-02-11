extends Control
var items = []
var powerups = []
var temp_owned = []
var temp_items = []
var shown = []
var coins = 400
@onready var ButtonList = [$item1/Button,$item2/Button,$item3/Button,$item4/Button,$item5/Button,$item6/Button]
@onready var sold_out = preload("res://art/ui/sold.png")
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$USDCoinCt/LCoins.text = str(coins)
	items = ready_list("res://class/gc_shop/item_lists/items.json")
	powerups = ready_list("res://class/gc_shop/item_lists/powerups.json")
	run_powerups()

func ready_list(file_closed):
	var file = FileAccess.open(file_closed, FileAccess.READ)
	var content = JSON.parse_string(file.get_as_text())
	return content
func run_powerups():
	shown = []
	var node_num = 0
	for child in get_children():
		var child_num = 0
		if child is Control and not child is BoxContainer:
			child.get_child(3).disabled = false
			child_num = node_num
			node_num += 1
			print("child, ",child_num)
		var rand = 0
		var is_sold_out = false
		print("	",child_num+1)
		print("	slot_5, ",(child_num == 4 and temp_items.size() < 5))
		print("	slot_6, ",(child_num == 5 and temp_items.size() < 4))
		print("	size, ",temp_items.size())
		print("	slot_All, ",((child_num == 4 and temp_items.size() < 4)or(child_num == 5 and temp_items.size() < 5)))
		print("	has",temp_items)
		if child.get_meta("item") == true and child is Control and not child is BoxContainer and ((child_num == 4 and temp_items.size() < 5)or(child_num == 5 and temp_items.size() < 4)):
			print("item")
			var new = false
			var cycle = 0
			while new == false:
				print(child_num,", redo")
				rand = randi() % 5
				if cycle <= 6:
					print("	",(items[rand]['ID']))
					print("	",temp_items)
					print("	",temp_items.has(int(items[rand]['ID'])))
					print("	",cycle)
				if temp_items.has((items[rand]['ID'])) == false and shown.has(int(items[rand]['ID'])) == false:
					new = true
				cycle += 1 
				if cycle >= 100:
					is_sold_out = true
					break
			shown.append(rand)
			print("shown, ",shown)
			print(is_sold_out)
		elif child.get_meta("item") == true and child is Control and not child is BoxContainer:
			is_sold_out = true
		else:
			rand = randi() % 4
		#var texture1 = preload(powerups[rand]['sprite'])
		if child is Control and not child is BoxContainer and not child is Button:
			if child.get_meta("item") == true and child is Control and not child is BoxContainer:
				if is_sold_out == false:
					child.get_child(1).text = items[rand]['name']
					child.get_child(2).text = str(int(items[rand]['price']))
					child.get_child(0).texture = load(items[rand]['sprite'])
					ButtonList[child_num].set_meta("item_id",items[rand]['ID']) ##fix_this
				else:
					child.get_child(1).text = ""
					child.get_child(2).text = ""
					child.get_child(0).texture = sold_out
					ButtonList[child_num].disabled = true
			else:
				child.get_child(1).text = powerups[rand]['name']
				child.get_child(2).text = str(int(powerups[rand]['price']))
				child.get_child(0).texture = load(powerups[rand]['sprite'])
				ButtonList[child_num].set_meta("item_id",powerups[rand]['ID'])
			#var purchased = false
			#if child.get_child(3) is Button:
				#await child.get_child(3).button_down
				#print("pressed")
				#while purchased == false:
					#if child.get_child(3).button_pressed == true:
						#child.get_child(3).button_pressed = false
						#purchased = true
						#temp_owned.append((int(powerups[rand]['ID'])))
					#await get_tree().create_timer(1.0/120).timeout

func _physics_process(_delta: float) -> void:
	for button in ButtonList:
		if button.button_pressed:
			if button == $item5/Button or button == $item6/Button:
				print(int(items[button.get_meta("item_id")]['price']))
				if int(items[button.get_meta("item_id")]['price']) <= coins:
					coins -= int(items[button.get_meta("item_id")]['price'])
					button.disabled = true
					button.get_parent().get_child(0).texture = sold_out
					button.get_parent().get_child(1).text = ""
					button.get_parent().get_child(2).text = ""
					$USDCoinCt/LCoins.text = str(coins)
					get_parent().items.append(button.get_meta("item_id"))
			else:
				if int(powerups[button.get_meta("item_id")- 5]['price']) <= coins:
					coins -= int(powerups[button.get_meta("item_id")- 5]['price'])
					button.disabled = true
					button.get_parent().get_child(0).texture = sold_out
					$USDCoinCt/LCoins.text = str(coins)
					get_parent().powerups.append(button.get_meta("item_id"))
			print(temp_owned,temp_items)


func _on_reroll_button_down() -> void:
	if coins >= 8:
		coins -= 8
		run_powerups()
		$USDCoinCt/LCoins.text = str(coins)


func _on_b_leave_shop_button_down() -> void:
	$".".visible = false
	print("left")
	queue_free()
	visible = true
