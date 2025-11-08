extends Area2D

const VELOCITY = 400

@export var direction = Vector2(0,0)
var playerNode

func _physics_process(delta):
	position += direction * VELOCITY * delta
	playerNode = get_parent().get_node("Player")
	if (position.distance_to(playerNode.position) > 1000) and (modulate.a != 0):
		queue_free()

func _on_area_entered(area: Area2D) -> void:
	if modulate.a != 0 and area.is_in_group("TRASH"):
		area.set_deferred("collision_mask", 0)
		modulate.a = 0
		area.set_meta("enabled", false)
		area.linearVel = Vector2(0, -20)
		var tween = create_tween()
		tween.tween_property(area.get_node("BubbleSprite2D"), "scale", Vector2(0.12, 0.12), 0.25)
		tween.tween_property(area, "modulate:a", 0, 2)
		tween.tween_callback(Callable(area, "queue_free"))
		tween.tween_callback(Callable(self, "queue_free"))
