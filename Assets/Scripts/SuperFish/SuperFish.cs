using UnityEngine;

public class SuperFish : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        float newPositionX = GameObject.Find("Player").GetComponent<Transform>().position.x;
        newPositionX += 0.78f;
        this.GetComponent<Transform>().position = new Vector3(newPositionX,
                                                              this.GetComponent<Transform>().position.y,
                                                              this.GetComponent<Transform>().position.z);
    }
}
