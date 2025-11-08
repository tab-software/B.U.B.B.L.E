extends Node

@export var linearVel = Vector2(0, 0)

func _process(delta: float) -> void:
	self.global_position += linearVel * delta
