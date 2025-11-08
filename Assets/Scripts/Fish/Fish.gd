extends Area2D

const SPEED = 100

var isLookAtRight = false
@export var linearVel = Vector2(0, 0)

@onready var player = get_node("/root/Main/Player")

func _ready() -> void:
	$AnimatedSprite2D.play()

func _physics_process(delta: float) -> void:
	if get_meta("enabled"):
		var direction = (player.global_position - global_position).normalized()
		var velocity = delta * direction * SPEED
		self.global_position += velocity
		if ((velocity.x > 0) and (not isLookAtRight)) or ((velocity.x < 0) and (isLookAtRight)):
			scale.x = -1
			isLookAtRight = not isLookAtRight;
	self.global_position += linearVel * delta
