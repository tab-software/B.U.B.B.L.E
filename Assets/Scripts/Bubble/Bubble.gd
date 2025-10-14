extends Area2D

const VELOCITY = 200

@export var direction = Vector2(0,0)
var playerNode

func _physics_process(delta):
	position += direction * VELOCITY * delta
	playerNode = get_parent().get_node("Player")
	if position.distance_to(playerNode.position) > 1000:
		queue_free()


func _on_body_entered(body: Node2D) -> void:
	if body.is_in_group("TRASH") and body.get_meta("enabled") and modulate.a != 0:
		body.set_deferred("collision_layer", 0)
		body.set_deferred("collision_mask", 0)
		modulate.a = 0
		body.set_meta("enabled", false)
		body.linear_velocity = Vector2(0, -20)
		var tween = create_tween()
		tween.tween_property(body.get_node("BubbleSprite2D"), "scale", Vector2(0.15, 0.15), 0.25)
		tween.tween_property(body, "modulate:a", 0, 2)
		tween.tween_callback(Callable(body, "queue_free"))
		tween.tween_callback(Callable(self, "queue_free"))
