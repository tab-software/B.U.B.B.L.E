extends CharacterBody2D

const SPEED = 200

func _physics_process(_delta):
	var input = 0
	if Input.is_action_pressed("ui_left"):
		input -= 1
	if Input.is_action_pressed("ui_right"):
		input += 1

	
	velocity.x = input * SPEED
	move_and_slide()
