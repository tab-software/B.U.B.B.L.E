extends Node2D

const SCREEN_SHAKE_AMP = 100.0
const SCREEN_SHAKE_FREQ = 10.0
const SCREEN_SHAKE_DAMPING = 10.0
const SCREEN_SHAKE_DURATION = 0.1
var   screenShakeAmplitude = 0.0

const TRASH_GEN_MIN_DELAY = 1.5
const TRASH_GEN_INITIAL_DELAY = 3
var   trashGenDelay = TRASH_GEN_INITIAL_DELAY
var   trashGenNext = 0
var   trash
var onGame = false

func screenShake():
	screenShakeAmplitude = SCREEN_SHAKE_AMP
	var tween = create_tween()
	tween.tween_property(self, "screenShakeAmplitude", 0.0, 0.25)

func trashGeneration():
	if Time.get_unix_time_from_system() > trashGenNext:
		var instantiatedTrash = trash.instantiate()
		var collision = $Control/TrashGenArea/CollisionShape2D
		var shape = $Control/TrashGenArea/CollisionShape2D.shape as RectangleShape2D
		instantiatedTrash.position = collision.global_position
		instantiatedTrash.position.x += randf_range(-shape.extents.x, shape.extents.x)
		add_sibling(instantiatedTrash)
		trashGenNext = Time.get_unix_time_from_system() + trashGenDelay

func _ready() -> void:
	trash = preload("res://Assets/Prefabs/Trash/Box.tscn")
	#onGame = true

func _process(_delta: float) -> void:
	position.y = screenShakeAmplitude * sin(2*PI*SCREEN_SHAKE_FREQ*Time.get_unix_time_from_system())
	if onGame:
		trashGeneration()
	else:
		if not $MainTitleScreen/Box.get_meta("enabled"):
			var tween = create_tween()
			tween.tween_property($MainTitleScreen, "modulate:a", 0.0, 1)
			onGame = true
