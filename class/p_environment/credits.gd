extends Button

func _on_pressed() -> void:
	get_tree().change_scene_to_file("res://class/p_environment/credits_page.tscn")
