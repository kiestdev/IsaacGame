extends Node2D
var cur_scale = 0
var pos_offset = Vector2(0,0)
var tet_offset = Vector2(0,0)
var touchin_da_bounds
signal frame
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.
func _physics_process(_delta: float) -> void:
	emit_signal("frame")
# Called every frame. 'delta' is the elapsed time since the previous frame.
var clicked = 0
var focused
func _input(event):
	if Input.is_action_just_pressed("click") and get_meta("placed") == false:
		if (event.position.x >= global_position.x and event.position.x <= (global_position.x + (40)))and(event.position.y >= global_position.y and event.position.y <= (global_position.y + (40))):
			clicked += 1
			if clicked > 1:
				return
			print("clicked")
			print("focused")
			focused = true
			var in_bound = true
			while focused:
				global_position = get_global_mouse_position() - Vector2(40. * cur_scale,40. * cur_scale) + pos_offset
				if Input.is_action_just_released("click") and in_bound == true:#Input.is_action_just_pressed("click"):
					focused = false
				await frame#get_tree().create_timer(1.0/120).timeout
			if focused == false:#position.snapped(Vector2(128*cur_scale,128*cur_scale))
				set_meta("placed",true)
		#print("Mouse Click/Unclick at: ", event.position)
		#print((event.position.x >= position.x and event.position.x <= (position.x + 256))and(event.position.y >= position.y and event.position.y <= (position.y + 512)))


func _on_area_2d_area_entered(area: Area2D) -> void:
	if area.is_in_group("bounds"):
		touchin_da_bounds = true
		print("inside")


func _on_area_2d_area_exited(area: Area2D) -> void:
	if area.is_in_group("bounds"):
		touchin_da_bounds = false
		print("outside")
