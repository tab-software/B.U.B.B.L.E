extends RigidBody2D

const SPEED = 200

@onready var player = get_node("/root/Main/Player")

func _ready() -> void:
	$AnimatedSprite2D.play()
	
func _physics_process(_delta: float) -> void:
	if get_meta("enabled"):
		var direction = (player.global_position - global_position).normalized()
		linear_velocity = direction * SPEED
		print(linear_velocity)
