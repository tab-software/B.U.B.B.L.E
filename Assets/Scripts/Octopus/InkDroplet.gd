extends Sprite2D

const VEL = 120
const DIST_TO_FREE = 1000

var initialPosition

func _ready() -> void:
	$Area2D.add_to_group("InkDroplet")
	self.initialPosition = self.global_position

func _process(delta: float) -> void:
	self.position.y += delta * VEL
	#if self.initialPosition.distance_to(self.global_position) > 1.0:
		#queue_free()
