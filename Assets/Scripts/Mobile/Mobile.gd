extends Node2D


@onready var player = get_node("/root/Main/Player")

const MOVEMENT_THRESHOLD = 0.75
var movementFinger = -1

func movementManagment(event):
	if event is InputEventScreenDrag:
		if event.index == movementFinger:
			var direction = (event.position - $MovementControlContainer.global_position).normalized()
			var radius = ($MovementControlContainer.texture.get_width() * $MovementControlContainer.scale.x) / 2
			$MovementControl.global_position = $MovementControlContainer.global_position + direction * radius * 0.5
			if (abs(direction.x) > MOVEMENT_THRESHOLD):
				player.x_movement = sign(direction.x)
	if event is InputEventScreenTouch:
		if event.pressed:
			var radius = ($MovementControlContainer.texture.get_width() * $MovementControlContainer.scale.x) / 2
			if (movementFinger == -1) and ($MovementControlContainer.global_position.distance_to(event.position) < radius):
				movementFinger = event.index
		else:
			if event.index == movementFinger:
				$MovementControl.global_position = $MovementControlContainer.global_position
				movementFinger = -1
				player.x_movement = 0

var shootFinger = -1

func shootManagment(event):
	if event is InputEventScreenDrag:
		if event.index == shootFinger:
			var direction = (event.position - $ShotControlContainer.global_position).normalized()
			var radius = ($ShotControlContainer.texture.get_width() * $ShotControlContainer.scale.x) / 2
			$ShotControl.global_position = $ShotControlContainer.global_position + direction * radius * 0.5
			player.mobileShotDirection = direction
	if event is InputEventScreenTouch:
		if event.pressed:
			var radius = ($ShotControlContainer.texture.get_width() * $ShotControlContainer.scale.x) / 2
			if (shootFinger == -1) and ($ShotControlContainer.global_position.distance_to(event.position) < radius):
				shootFinger = event.index
		else:
			if event.index == shootFinger:
				$ShotControl.global_position = $ShotControlContainer.global_position
				shootFinger = -1
				player.mobileShotDirection = null
				
func _input(event):
	if modulate.a == 1.0:
		movementManagment(event)
		shootManagment(event)
