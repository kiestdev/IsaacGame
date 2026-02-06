extends GridContainer
const flower1 = preload("res://class/resources/RDaisy.tscn")
const flower2 = preload("res://class/resources/RForgetMeNot.tscn")
const flower3 = preload("res://class/resources/RPansy.tscn")
const flower4 = preload("res://class/resources/RSunflower.tscn")
var cur_scale 
var focused = false

var tetromino_type = 0 
# 0 = square 
# 1 = T
# 2 = L
# 3 = Reverse L
# 4 = squig _|-
# 5 = Rev squig -|_
# 6 = line
#lass flower:
var one = randi() % 3
var two = randi() % 3
var three = randi() % 3
var four = randi() % 3
var array1 = [one,two,three,four]
var photos = [flower1,flower2,flower3,flower4]
var abled

func _ready() -> void:
	#for child in $".".get_children():
		#child.custom_minimum_size = Vector2(128,128)
	tetromino_type = randi() % 6
	print(tetromino_type)
	cur_scale = $".".get_parent().scale.x
	generated()
	print(get_parent().get_meta("placed"))
	#$"1/sprite1/Sprite2D".texture =flower1




func generated():
	if tetromino_type == 0:
		disabling($"5")
		disabling($"6")
		disabling($"7")
		abled = [$"1",$"2",$"3",$"4"]
		$square.visible = true
	if tetromino_type == 1:
		disabling($"2")
		disabling($"6")
		disabling($"7")
		abled = [$"1",$"3",$"4",$"5"]
		$T.visible = true
	if tetromino_type == 3:
		disabling($"1")
		disabling($"3")
		disabling($"7")
		abled = [$"2",$"4",$"5",$"6"]
		$RevL.visible = true
	if tetromino_type == 4:
		disabling($"2")
		disabling($"5")
		disabling($"7")
		abled = [$"1",$"3",$"4",$"6"]
		$Squig.visible = true
	if tetromino_type == 5:
		disabling($"1")
		disabling($"6")
		disabling($"7")
		abled = [$"2",$"3",$"4",$"5"]
		$RevSquig.visible = true
	if tetromino_type == 6:
		disabling($"2")
		disabling($"4")
		disabling($"6")
		abled = [$"1",$"3",$"5",$"7"]
		$Line.visible = true
	if tetromino_type == 2:
		disabling($"2")
		disabling($"4")
		disabling($"7")
		abled = [$"1",$"3",$"5",$"6"]
		$L.visible = true
	var i1 =0
	for i in abled:
		var i2 = i1
		i1 +=1
		print(i2)
		print(abled[i2])
		for child in abled[i2].get_children():
			if child is Sprite2D:
				var instance = photos[array1[i2]].instantiate()
				child.add_child(instance)
				instance.position = Vector2(0,0)
				#for i3 in child.get_children:
					#i3.position = Vector2(0,0)




func disabling(path: Node):
	for child in path.get_children():
		for child2 in child.get_children():
			if child2 is CollisionShape2D:
				child2.set_deferred("disabled",true)
		if child is Sprite2D:
			child.visible = false


func _input(event):
	if Input.is_action_pressed("click") and get_parent().get_meta("placed") == false:
		print("clicked, ",event.position,", ",", ",global_position,", ",$".".get_parent().scale.x)
		if (event.position.x >= position.x and event.position.x <= (position.x + (256 * $".".get_parent().scale.x)))and(event.position.y >= position.y and event.position.y <= (position.y + (512 * $".".get_parent().scale.y))):
			focused = true
			print(focused)
			while focused:
				global_position = get_global_mouse_position() - Vector2(128.0 * cur_scale,128.0 * cur_scale)
				if Input.is_action_just_released("click"):#Input.is_action_just_pressed("click"):
					focused = false
					print(focused)
				await get_tree().create_timer(1.0/120).timeout
			if focused == false:
				position = position.snapped(Vector2(128*cur_scale,128*cur_scale))
				get_parent().set_meta("placed",true)
		#print("Mouse Click/Unclick at: ", event.position)
		#print((event.position.x >= position.x and event.position.x <= (position.x + 256))and(event.position.y >= position.y and event.position.y <= (position.y + 512)))
