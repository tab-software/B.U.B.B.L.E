using UnityEngine;

public class SuperFish : MonoBehaviour
{
    public float YVelocity = 5.0f;
    private float topPositionY = 0.0f;
    public float directionY = 0.0f;
    private void Start()
    {
        this.topPositionY = this.transform.position.y;
    }
    private void Update()
    {
        if(directionY < 0.0f)
        {
            this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x,
                                                                  this.GetComponent<Transform>().position.y - YVelocity * Time.deltaTime,
                                                                  this.GetComponent<Transform>().position.z); 
            if(this.transform.position.y <= -8.0)
                directionY = 0.0f;
        }
        else if(directionY > 0.0f)
        {
            this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x,
                                                                  this.GetComponent<Transform>().position.y + YVelocity * Time.deltaTime,
                                                                  this.GetComponent<Transform>().position.z); 
            if(this.transform.position.y >= this.topPositionY)
                directionY = 0.0f;
        }
        float newPositionX = GameObject.Find("Player").GetComponent<Transform>().position.x;
        newPositionX += 0.78f;
        this.GetComponent<Transform>().position = new Vector3(newPositionX,
                                                              this.GetComponent<Transform>().position.y,
                                                              this.GetComponent<Transform>().position.z);
    }
}
