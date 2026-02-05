extends GridContainer
const flower1 = preload("res://flowers/peach-flowers.jpg")
const flower2 = preload("res://flowers/red-rose-flowers-840x473.jpg")
const flower3 = preload("res://flowers/v4_Just-Happy-24090922502.jpg")

var tetromino_type = 0 
# 0 = square 
# 1 = T
# 2 = L
# 3 = Reverse L
# 4 = squig _|-
# 5 = Rev squig -|_
# 6 = line
#lass flower:
var one = randi() % 2
var two = randi() % 2
var three = randi() % 2
var four = randi() % 2
var array1 = [one,two,three,four]
var photos = [flower1,flower2,flower3]
var abled

func _ready() -> void:
	for child in $".".get_children():
		child.custom_minimum_size = Vector2(128,128)
	tetromino_type = randi() % 6
	print(tetromino_type)
	generated()
	#$"1/sprite1/Sprite2D".texture =flower1




func generated():
	if tetromino_type == 0:
		disabling($"5")
		disabling($"6")
		disabling($"7")
		abled = [$"1",$"2",$"3",$"4"]
	if tetromino_type == 1:
		disabling($"2")
		disabling($"6")
		disabling($"7")
		abled = [$"1",$"3",$"4",$"5"]
	if tetromino_type == 3:
		disabling($"1")
		disabling($"3")
		disabling($"7")
		abled = [$"2",$"4",$"5",$"6"]
	if tetromino_type == 4:
		disabling($"2")
		disabling($"5")
		disabling($"7")
		abled = [$"1",$"3",$"4",$"6"]
	if tetromino_type == 5:
		disabling($"1")
		disabling($"6")
		disabling($"7")
		abled = [$"2",$"3",$"4",$"5"]
	if tetromino_type == 6:
		disabling($"2")
		disabling($"4")
		disabling($"6")
		abled = [$"1",$"3",$"5",$"7"]
	if tetromino_type == 2:
		disabling($"2")
		disabling($"4")
		disabling($"7")
		abled = [$"1",$"3",$"5",$"6"]
	var i1 =0
	for i in abled:
		var i2 = i1
		i1 +=1
		print(i2)
		print(abled[i2])
		for child in abled[i2].get_children():
			if child is Sprite2D:
				child.get_child(0).texture = photos[array1[i2]]




func disabling(path: Node):
	for child in path.get_children():
		for child2 in child.get_children():
			if child2 is CollisionShape2D:
				child2.set_deferred("disabled",true)
		if child is Sprite2D:
			child.visible = false
