using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BubbleableObject : MonoBehaviour
{
    public bool bubbled = false;
    private bool lastFrameAreBubbled = false;
    private float bubbledAt = 0.0f;
    private float disappearsIn = 1.0f;
    private GameObject bubbleObject;
    
    private void Start()
    {
        this.bubbleObject = this.transform.Find("bubble").gameObject;
        this.bubbleObject.SetActive(false);
    }

    private void Update()
    {
        if(this.bubbled && !this.lastFrameAreBubbled)
            this.bubbledAt = Time.time;
        if(this.bubbled)
        {
            this.bubbleObject.SetActive(true);
            this.GetComponent<Rigidbody2D>().gravityScale = -0.5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(this.gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                               this.gameObject.GetComponent<SpriteRenderer>().color.g,
                                                                               this.gameObject.GetComponent<SpriteRenderer>().color.b,
                                                                               1.0f-Mathf.Min(((Time.time-this.bubbledAt)/this.disappearsIn), 1.0f));
            this.bubbleObject.GetComponent<SpriteRenderer>().color = new Vector4(this.gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                               this.gameObject.GetComponent<SpriteRenderer>().color.g,
                                                                               this.gameObject.GetComponent<SpriteRenderer>().color.b,
                                                                               0.40f-0.4f*Mathf.Min(((Time.time-this.bubbledAt)/this.disappearsIn), 1.0f));
        }
        if(this.bubbled)
            this.lastFrameAreBubbled = true;
    }

}
