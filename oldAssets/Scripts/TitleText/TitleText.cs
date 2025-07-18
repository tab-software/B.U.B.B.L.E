using Unity.VisualScripting;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    public float velocity = 5.0f;

    private void Update()
    {
        if(GameObject.Find("GameManager").GetComponent<GameManager>().initilized)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + this.velocity * Time.deltaTime);
            if(transform.position.y > 10.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
