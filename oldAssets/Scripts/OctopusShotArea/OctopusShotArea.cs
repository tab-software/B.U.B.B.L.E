using UnityEngine;

public class OctopusShotArea : MonoBehaviour
{
    private void OnCollide(GameObject gameObject)
    {
        if(gameObject.tag == "OCTOPUS")
        {
            gameObject.GetComponent<Octopus>().onShootArea = true;
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
    private void OnExit(GameObject gameObject)
    {
        if (gameObject.tag == "OCTOPUS")
        {
            gameObject.GetComponent<Octopus>().onShootArea = false;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        this.OnExit(col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        this.OnExit(col.gameObject);
    }
}
