using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float velocity = 1.0f;
    public float rotationVelocity = 50.0f;
    public float maxAngleRotation = 30.0f;
    public float swimmingEffectAmplitude = 1.0f; 
    public float swimmingEffectFrecuency = 1.0f; 

    private float initialYPosition = 0.0f;
    private float angle = 0.0f;

    void Start()
    {
        this.initialYPosition = this.GetComponent<Transform>().position.y;
    }

    void Update()
    {
        float Xmovement = Input.GetAxis("Horizontal");

        this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x,
                                                              this.initialYPosition + swimmingEffectAmplitude*(float)Math.Sin(2.0*Math.PI*swimmingEffectFrecuency*Time.time),
                                                              this.GetComponent<Transform>().position.z);

        this.angle = transform.eulerAngles.z;
        if (this.angle > 180) this.angle -= 360;

        this.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Xmovement, 0) * this.velocity;
        
        if (Xmovement != 0.0f)
        {
            if((this.angle < maxAngleRotation && Xmovement < 0)
            || (this.angle > -maxAngleRotation && Xmovement > 0))
            {
                this.GetComponent<Transform>().eulerAngles = new Vector3(0f, 0f, this.GetComponent<Transform>().eulerAngles.z - Xmovement * Time.deltaTime * this.rotationVelocity);
            }
        }
        else
        {
            if(this.angle > 0.0f)
            {
                this.GetComponent<Transform>().eulerAngles = new Vector3(0f, 0f, this.GetComponent<Transform>().eulerAngles.z - Time.deltaTime * this.rotationVelocity);
            }
            if(this.angle < 0.0f)
            {
                this.GetComponent<Transform>().eulerAngles = new Vector3(0f, 0f, this.GetComponent<Transform>().eulerAngles.z + Time.deltaTime * this.rotationVelocity);
            }
        }

        Vector2 direction = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - this.GetComponent<Transform>().Find("Arm").GetComponent<Transform>().position;

        float newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.GetComponent<Transform>().Find("Arm").GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, newAngle + (float)90);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "TRASH" || col.gameObject.tag == "FISH")
        {
            Destroy(col.gameObject);
            GameObject.Find("Camera").GetComponent<TCamera>().ApplyEffect("HSHAKE");
        }
    }
}
