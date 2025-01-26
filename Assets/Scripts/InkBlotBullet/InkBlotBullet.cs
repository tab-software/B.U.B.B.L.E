using UnityEngine;

public class InkBlotBullet : MonoBehaviour
{
    public GameObject effect;
    private void OnCollide(GameObject gameObject)
    {
        if(gameObject.tag == "PLAYER")
        {
            Instantiate(effect);
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
