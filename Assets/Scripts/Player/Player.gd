extends CharacterBody2D

const SPEED               = 20
const MAX_SPEED           = 200
const RSPEED              = 80
const MAX_ANGLE_ROTATION  = 15

const SMIWING_EFFECT_AMP  = 10
const SMIWING_EFFECT_FREQ = 0.1

func movement(delta):
	var x_movement := Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	
	var angle := rotation_degrees
	if angle > 180:
		angle -= 360

	velocity = velocity.lerp(Vector2.ZERO, 0.05)

	if x_movement != 0.0:
		velocity.x += x_movement * SPEED
		velocity.x = clamp(velocity.x, -MAX_SPEED, MAX_SPEED)
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

func smiwingEffect():
	velocity.y = SMIWING_EFFECT_AMP*cos(2*PI*SMIWING_EFFECT_FREQ*Time.get_unix_time_from_system())
	print(velocity.y)

func _physics_process(delta):
	movement(delta)
	smiwingEffect()
	move_and_slide()
