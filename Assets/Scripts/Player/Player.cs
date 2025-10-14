using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float velocity = 1.0f;
    public float rotationVelocity = 30.0f;
    public float maxAngleRotation = 45.0f;

    void Start()
    {
        
    }

    void Update()
    {
        float Xmovement = Input.GetAxis("Horizontal");
        float Ymovement = Input.GetAxis("Vertical");

        this.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Xmovement, Ymovement) * this.velocity;
        if(
            (this.GetComponent<Transform>().eulerAngles.z < maxAngleRotation & Xmovement > 0)
        ||  (this.GetComponent<Transform>().eulerAngles.z > -maxAngleRotation & Xmovement < 0)
          )
        this.GetComponent<Transform>().eulerAngles = new Vector3(0f, 0f, this.GetComponent<Transform>().eulerAngles.z - Xmovement * Time.deltaTime * this.rotationVelocity);
    }
}
