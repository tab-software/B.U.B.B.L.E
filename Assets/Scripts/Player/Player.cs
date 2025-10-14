using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 1.0f;
    public float rotationVelocity = 50.0f;
    public float maxAngleRotation = 30.0f;
    public float swimmingEffectAmplitude = 1.0f; 
    public float swimmingEffectFrecuency = 1.0f; 

    private float angle = 0.0f;

    private float eyesDist = 0.1f;
    private Vector2 initialPosition;

    void Start()
    {
        this.initialPosition = this.GetComponent<Transform>().position;
    }

    private void pointArmToMouse(String nameArmObject)
    {
        Vector2 direction = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - this.GetComponent<Transform>().Find(nameArmObject).GetComponent<Transform>().position;

        float newAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.GetComponent<Transform>().Find(nameArmObject).GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, newAngle + (float)90);
    }

    private void movement()
    {
        float Xmovement = Input.GetAxis("Horizontal");

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
    }

    void smiwingEffect()
    {
        this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x,
                                                        this.initialPosition.y + swimmingEffectAmplitude*(float)Math.Sin(2.0*Math.PI*swimmingEffectFrecuency*Time.time),
                                                        this.GetComponent<Transform>().position.z);
    }

    void animateEyes()
    {
        Vector2 direction = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - this.GetComponent<Transform>().position;
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        Debug.Log("a");
        Debug.Log(angleInDegrees);
        Debug.Log("b");
        Debug.Log(this.transform.GetComponent<Transform>().Find("PlayerSprite/Eyes/EyesSprite").localPosition);
        this.transform.GetComponent<Transform>().Find("PlayerSprite/Eyes/EyesSprite").localPosition = new Vector3(this.eyesDist*(float)Math.Cos(angleInRadians), this.eyesDist*(float)Math.Sin(angleInRadians), 0);
    }

    void Update()
    {
        this.smiwingEffect();
        this.movement();
        this.pointArmToMouse("RArm");
        this.pointArmToMouse("LArm");
        this.animateEyes();
    }
    void OnCollide(GameObject gameObject)
    {
        
        if(gameObject.tag == "TRASH" || gameObject.tag == "FISH")
        {
            Destroy(gameObject);
            GameObject.Find("Camera").GetComponent<TCamera>().ApplyEffect("HSHAKE");
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        this.OnCollide(col.gameObject);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        this.OnCollide(col.gameObject);
    }
}
