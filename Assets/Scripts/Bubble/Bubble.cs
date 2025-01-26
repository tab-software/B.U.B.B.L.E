using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float velocity = 0.0f;
    private void Update()
    {
        this.GetComponent<Rigidbody2D>().linearVelocity = -this.transform.up * this.velocity;
    }
    private void OnCollide(GameObject gameObject)
    {
        if(gameObject.tag == "TRASH" || gameObject.tag == "FISH")
        {
            if(!gameObject.GetComponent<BubbleableObject>().bubbled)
                Destroy(this);
            gameObject.GetComponent<BubbleableObject>().bubbled = true;
        }
        else if(gameObject.tag != "PLAYER" && gameObject.tag != "BUBBLE")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        this.OnCollide(col.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        this.OnCollide(col.gameObject);
    }
}
