extends CharacterBody2D

const SPEED = 200
const RSPEED = 200
const MAX_ANGLE_ROTATION = 15

func movement(delta):
	var x_movement := Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	
	var angle := rotation_degrees
	if angle > 180:
		angle -= 360

	velocity.x = x_movement * SPEED
	velocity.y = 0
	move_and_slide()

	if x_movement != 0.0:
		if (angle > -MAX_ANGLE_ROTATION and x_movement < 0) or (angle < MAX_ANGLE_ROTATION and x_movement > 0):
			rotation_degrees += x_movement * delta * RSPEED
	else:
		if abs(angle) > 1:
			if angle > 0.0:
				rotation_degrees -= delta * RSPEED
			elif angle < 0.0:
				rotation_degrees += delta * RSPEED
		else:
			angle = 0
			
func _physics_process(delta):
	movement(delta)
	move_and_slide()
