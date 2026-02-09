extends Control
var items = []
var powerups = []
var temp_owned = []
var shown = []
@onready var ButtonList = [$item1/Button,$item2/Button,$item3/Button,$item4/Button,$item5/Button,$item6/Button]
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	items = ready_list("res://class/gc_shop/item_lists/items.json")
	powerups = ready_list("res://class/gc_shop/item_lists/powerups.json")
	run_powerups()

func ready_list(file_closed):
	var file = FileAccess.open(file_closed, FileAccess.READ)
	var content = JSON.parse_string(file.get_as_text())
	return content

func run_powerups():
	for child in get_children():
		var rand = 0
		if child.get_meta("item") == true and child is Control and not child is BoxContainer:
			print("item")
			var new = false
			var cycle = 0
			while new == false:
				print("redo")
				rand = randi() % 5
				print(int(powerups[rand]['ID']))
				if temp_owned.has(int(powerups[rand]['ID'])) == false and shown.has(int(powerups[rand]['ID'])) == false:
					new = true
				cycle += 1
				if cycle >= 100:
					get_tree().quit()
			shown.append(rand)
			print("shown, ",shown)
		else:
			rand = randi() % 4
		#var texture1 = preload(powerups[rand]['sprite'])
		if child is Control and not child is BoxContainer:
			child.get_child(1).text = powerups[rand]['name']
			child.get_child(2).text = str(int(powerups[rand]['price']))
			child.get_child(0).texture = load(powerups[rand]['sprite'])
			ButtonList[rand].set_meta("item_id",powerups[rand]['ID']) ##fix_this
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
			button.disabled = true
			temp_owned.append($item1/Button.get_meta("item_id"))
			print(temp_owned)
