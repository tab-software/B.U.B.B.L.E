extends CharacterBody2D

var mobileOS = false

const SPEED               = 20
const MAX_SPEED           = 200
const RSPEED              = 80
const MAX_ANGLE_ROTATION  = 15

var x_movement = 0

func movement(delta):
	if not mobileOS:
		x_movement = Input.get_action_strength("RIGHT_MOVEMENT") - Input.get_action_strength("LEFT_MOVEMENT")
	#If mobile OS the input is managed by Mobile.gd

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

const SMIWING_EFFECT_AMP  = 10
const SMIWING_EFFECT_FREQ = 0.1

func smiwingEffect():
	velocity.y = SMIWING_EFFECT_AMP*cos(2*PI*SMIWING_EFFECT_FREQ*Time.get_unix_time_from_system())

const ANIMATION_EYES_DIST = 50

func animateEyes():
	if not mobileOS:
		var mouse_pos = get_viewport().get_mouse_position()
		var direction = mouse_pos - global_position
		var angle = direction.angle()
		$Sprites/Head/Eyes.position = Vector2(cos(angle), sin(angle)) * ANIMATION_EYES_DIST

const SHOOTS_PER_SECOND = 5
const TIME_BEWEEN_SHOOTS = 1.0 / SHOOTS_PER_SECOND
var lastShoot
var bubblePrefab
var mobileShotDirection

func armsManagment():
	var shoot
	shoot = (Time.get_unix_time_from_system() - lastShoot) > TIME_BEWEEN_SHOOTS
	var direction
	#Rigth Arm shoot
	if not mobileOS:
		shoot = shoot and Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT)
		var mouse_pos = get_global_mouse_position()
		var global_pos_arm = $Sprites/RArm.get_global_position()
		direction = mouse_pos - global_pos_arm
	else:
		shoot = shoot and (mobileShotDirection != null)
		direction = mobileShotDirection
	if shoot:
		var bubbleInst = bubblePrefab.instantiate()
		bubbleInst.position = $Sprites/RArm/Canyon.get_global_position()
		bubbleInst.direction = direction.normalized()
		add_sibling(bubbleInst)
		$AudioStreamPlayer2D.play()
	if direction != null:
		$Sprites/RArm.rotation = direction.angle()
	#Left Arm shoot
	if not mobileOS:
		shoot = shoot and Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT)
		var mouse_pos = get_global_mouse_position()
		var global_pos_arm = $Sprites/LArm.get_global_position()
		direction = mouse_pos - global_pos_arm
	else:
		shoot = shoot and (mobileShotDirection != null)
		direction = mobileShotDirection
	if shoot:
		var bubbleInst = bubblePrefab.instantiate()
		bubbleInst.position = $Sprites/LArm/Canyon.get_global_position()
		bubbleInst.direction = direction.normalized()
		add_sibling(bubbleInst)
	if direction != null:
		$Sprites/LArm.rotation = direction.angle()
	if shoot:
		lastShoot = Time.get_unix_time_from_system()

func _physics_process(delta):
	movement(delta)
	smiwingEffect()
	animateEyes()
	armsManagment()
	move_and_slide()

func _on_ready() -> void:
	bubblePrefab = preload("res://Assets/Prefabs/Bubble/bubble.tscn")
	lastShoot = Time.get_unix_time_from_system()
	$Sprites/Swirl.play()
	var hostOS = OS.get_name()
	if hostOS == "Web":
		var screen_size = DisplayServer.screen_get_size()
		mobileOS = screen_size.x < 900 or screen_size.y < 900 # is small screen
	else:
		mobileOS = (hostOS == "Android") or (hostOS == "iOS")
	if mobileOS:
		$"../MobileUI".modulate.a = 1.0

func _on_area_2d_area_entered(area: Area2D) -> void:
	if area.is_in_group("TRASH"):
		if area.get_meta("enabled"):
			$"..".screenShake()
			area.set_meta("enabled", false)
			var tween = create_tween()
			tween.tween_property(area, "modulate:a", 0.0, 1.0)
			tween.tween_callback(Callable(area, "queue_free"))

	if area.is_in_group("InkDroplet"):
		var tween = create_tween()
		tween.tween_property($"../InkBlot", "scale", Vector2(3, 3), 1.0)
		tween.tween_property($"../InkBlot", "modulate:a", 0, 1.0)
		tween.tween_callback(func() -> void:
			$"../InkBlot".modulate.a = 1.0
			$"../InkBlot".scale = Vector2(0, 0)
		)
		area.get_parent().queue_free()
