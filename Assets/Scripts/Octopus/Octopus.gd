extends Node2D

const TIME = 10

@export var active = false
var tween
var lastDrop = 0.0

func activate():
	self.active = true

func reset():
	self.active = false
	self.rotation_degrees = 45

func finishTween():
	self.reset()
	self.active = false

func _physics_process(delta):
	if self.active:
		if self.tween == null or (not self.tween.is_running()):
			self.tween = create_tween()
			self.tween.tween_property(self, "rotation_degrees", -45, self.TIME)
			self.tween.finished.connect(func(): finishTween())
		$Sprite.rotation_degrees = -rotation_degrees

func _ready() -> void:
	self.reset()
	$Sprite.play()
