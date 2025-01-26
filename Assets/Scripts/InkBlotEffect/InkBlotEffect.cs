using Unity.VisualScripting;
using UnityEngine;

public class InkBlot : MonoBehaviour
{
    public float velocity = 10.0f;

    private void Start()
    {
        this.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        if(this.transform.localScale.x < 4.0f)
            this.transform.localScale = new Vector3(
                this.transform.localScale.x + velocity * Time.deltaTime,
                this.transform.localScale.y + velocity * Time.deltaTime,
                this.transform.localScale.z
            );
        else
            this.GetComponent<SpriteRenderer>().color = new Vector4(
                this.GetComponent<SpriteRenderer>().color.r,
                this.GetComponent<SpriteRenderer>().color.g,
                this.GetComponent<SpriteRenderer>().color.b,
                this.GetComponent<SpriteRenderer>().color.a - velocity * Time.deltaTime * 0.1f
            );
        if(this.GetComponent<SpriteRenderer>().color.a <= 0.0f)
            Destroy(this.gameObject);
    }
}
