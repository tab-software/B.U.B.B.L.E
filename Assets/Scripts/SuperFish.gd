extends AnimatedSprite2D

var initialPos

func _ready() -> void:
	self.initialPos = self.global_position
	self.global_position.y += 250

func activate() -> void:
	var tween = create_tween()
	tween.tween_property(self, "global_position", self.initialPos, 10.0)
