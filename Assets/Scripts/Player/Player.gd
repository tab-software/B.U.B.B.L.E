extends CharacterBody2D

const SPEED               = 20
const MAX_SPEED           = 200
const RSPEED              = 80
const MAX_ANGLE_ROTATION  = 15

const SMIWING_EFFECT_AMP  = 10
const SMIWING_EFFECT_FREQ = 0.1

const ANIMATION_EYES_DIST = 50

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

func animateEyes():
	var mouse_pos = get_viewport().get_mouse_position()
	var direction = mouse_pos - global_position
	var angle = direction.angle()
	$Sprites/Head/Eyes.position = Vector2(cos(angle), sin(angle)) * ANIMATION_EYES_DIST

func pointArms():
	var mouse_pos = get_global_mouse_position()
	var global_pos_arm = $Sprites/RArm.get_global_position()
	var direction = mouse_pos - global_pos_arm
	$Sprites/RArm.rotation = direction.angle()
	global_pos_arm = $Sprites/LArm.get_global_position()
	direction = mouse_pos - global_pos_arm
	$Sprites/LArm.rotation = direction.angle()

func shotManagment():
	pass

func _physics_process(delta):
	movement(delta)
	smiwingEffect()
	animateEyes()
	pointArms()
	shotManagment()
	move_and_slide()

func _on_ready() -> void:
	$Sprites/Swirl.play()
