extends CharacterBody2D

const SPEED = 100

var isLookAtRight = false

@onready var player = get_node("/root/Main/Player")

func _ready() -> void:
	$AnimatedSprite2D.play()
	
func _physics_process(_delta: float) -> void:
	if get_meta("enabled"):
		var direction = (player.global_position - global_position).normalized()
		velocity = direction * SPEED
		if ((velocity.x > 0) and (not isLookAtRight)) or ((velocity.x < 0) and (isLookAtRight)):
			scale.x = -1
			isLookAtRight = not isLookAtRight;
	move_and_slide()
