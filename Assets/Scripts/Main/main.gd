extends Node2D

const LEVEL_DURATION = 60.0
var intializedAt = 0.0

var onGame = false

const SCREEN_SHAKE_AMP = 100.0
const SCREEN_SHAKE_FREQ = 10.0
const SCREEN_SHAKE_DAMPING = 10.0
const SCREEN_SHAKE_DURATION = 0.1
var   screenShakeAmplitude = 0.0

func screenShake():
	screenShakeAmplitude = SCREEN_SHAKE_AMP
	var tween = create_tween()
	tween.tween_property(self, "screenShakeAmplitude", 0.0, 0.25)

const TRASH_GEN_MIN_DELAY = 1.5
const TRASH_GEN_INITIAL_DELAY = 3
var   trashGenDelay = TRASH_GEN_INITIAL_DELAY
var   trashGenNext = 0
var   trashPrefab

func trashGeneration():
	if Time.get_unix_time_from_system() > trashGenNext:
		var instantiatedTrash = trashPrefab.instantiate()
		var collision = $Control/TrashGenArea/CollisionShape2D
		var shape = collision.shape as RectangleShape2D
		instantiatedTrash.position = collision.global_position
		instantiatedTrash.position.x += randf_range(-shape.extents.x, shape.extents.x)
		add_sibling(instantiatedTrash)
		trashGenNext = Time.get_unix_time_from_system() + trashGenDelay

const FISH_GEN_MIN_DELAY = 1.5
const FISH_GEN_INITIAL_DELAY = 3
var   fishGenDelay = FISH_GEN_INITIAL_DELAY
var   fishGenNext = 0
var   fishPrefab

func fishGeneration():
	if Time.get_unix_time_from_system() > fishGenNext:
		var instantiatedFish = fishPrefab.instantiate()
		var collision
		if randi() % 2 == 0:
			collision = $Control/FishGenAreaL/CollisionShape2D
		else:
			collision = $Control/FishGenAreaR/CollisionShape2D
		var shape = collision.shape as RectangleShape2D
		instantiatedFish.position = collision.global_position
		instantiatedFish.position.y += randf_range(-shape.extents.y, shape.extents.y)
		add_sibling(instantiatedFish)
		fishGenNext = Time.get_unix_time_from_system() + fishGenDelay

const PARALLAX_EFFECT_VEL = 100

func _ready() -> void:
	trashPrefab = preload("res://Assets/Prefabs/Trash/Box.tscn")
	fishPrefab = preload("res://Assets/Prefabs/Fish/Fish.tscn")
	#onGame = true

func _process(_delta: float) -> void:
	position.y = screenShakeAmplitude * sin(2*PI*SCREEN_SHAKE_FREQ*Time.get_unix_time_from_system())
	if onGame:
		trashGeneration()
		fishGeneration()
		var progress = (Time.get_unix_time_from_system() - intializedAt)/LEVEL_DURATION
		print(progress)
		var color = Color(
			(1.0/255.0) * (32 * max(1.0 - progress, 0.0)),
			(1.0/255.0) * (54 + 136 * min(progress, 1.0)),
			(1.0/255.0) * (87 + 168 * min(progress, 1.0))
		)
		print(color)
		print(max(1.0 - progress, 0.0))
		print(min(progress, 1.0))
		$Background.texture.gradient.set_color(0, color)
		#$Background.texture.
	else:
		if not $MainTitleScreen/Box.get_meta("enabled"):
			var tween = create_tween()
			tween.tween_property($MainTitleScreen, "modulate:a", 0.0, 1)
			tween.tween_property($UI, "modulate:a", 1.0, 1)
			intializedAt = Time.get_unix_time_from_system()
			onGame = true
