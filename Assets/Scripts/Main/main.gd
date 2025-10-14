extends Node2D

const SCREEN_SHAKE_AMP = 100.0
const SCREEN_SHAKE_FREQ = 10.0
const SCREEN_SHAKE_DAMPING = 10.0
const SCREEN_SHAKE_DURATION = 0.1
var screenShakeAmplitude = 0.0

func screenShake():
	screenShakeAmplitude = SCREEN_SHAKE_AMP
	var tween = create_tween()
	tween.tween_property(self, "screenShakeAmplitude", 0.0, 0.25)

func _process(_delta: float) -> void:
	position.y = screenShakeAmplitude * sin(2*PI*SCREEN_SHAKE_FREQ*Time.get_unix_time_from_system())
