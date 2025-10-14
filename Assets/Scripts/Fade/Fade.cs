using UnityEngine;

public class Fade : MonoBehaviour
{
    public float timeLapse = 10.0f;
    private bool running = true;
    private bool state = false;
    private float at = 0.0f;
    
    private void Start()
    {
        this.at = Time.time;
    }
    public void fade()
    {
        this.state = !this.state;
        this.running = true;
    }
    private void Update()
    {
        if(this.running)
        {
            if(this.state)
            {
                this.GetComponent<SpriteRenderer>().color = new Vector4(this.GetComponent<SpriteRenderer>().color.r,
                                                                        this.GetComponent<SpriteRenderer>().color.g,
                                                                        this.GetComponent<SpriteRenderer>().color.b,
                                                                        Mathf.Min((Time.time-this.at)/this.timeLapse, 1.0f));
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Vector4(this.GetComponent<SpriteRenderer>().color.r,
                                                                        this.GetComponent<SpriteRenderer>().color.g,
                                                                        this.GetComponent<SpriteRenderer>().color.b,
                                                                        1.0f-Mathf.Min((Time.time-this.at)/this.timeLapse, 1.0f));
            
            }
            if((Time.time-this.at)/this.timeLapse > 1.0f)
                    this.running = false;
        }
    }
}
