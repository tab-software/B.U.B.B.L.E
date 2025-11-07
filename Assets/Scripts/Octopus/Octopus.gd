extends Node2D

const TIME = 10
const TIME_BETWEEN_DROPS = 1

@export var active = false
var tween

var latestDrop = 0
var inkDropletPrefab

func activate():
	self.active = true
	self.latestDrop = Time.get_unix_time_from_system()

func reset():
	self.active = false
	self.rotation_degrees = 45

func finishTween():
	self.reset()
	self.active = false

func _ready() -> void:
	inkDropletPrefab = preload("res://Assets/Prefabs/Misc/InkDroplet.tscn")
	self.reset()
	$Sprite.play()

func _physics_process(delta):
	if self.active:
		if ( Time.get_unix_time_from_system() - self.latestDrop ) > TIME_BETWEEN_DROPS:
			var inkDropletInst = inkDropletPrefab.instantiate()
			inkDropletInst.global_position = $Sprite.get_global_position()
			get_parent().add_child(inkDropletInst)
			self.latestDrop = Time.get_unix_time_from_system()
		if self.tween == null or (not self.tween.is_running()):
			self.tween = create_tween()
			self.tween.tween_property(self, "rotation_degrees", -45, self.TIME)
			self.tween.finished.connect(func(): finishTween())
		$Sprite.rotation_degrees = -rotation_degrees
