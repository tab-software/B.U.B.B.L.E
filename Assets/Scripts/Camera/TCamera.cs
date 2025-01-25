using System;
using UnityEngine;

public class TCamera : MonoBehaviour
{
    private Vector3 initialPosition;

    private float amplitude;
    private float shakeFreq = 10.0f;

    private float desaceleration = 10.0f;

    public enum Effect
    {
        HSHAKE,
    }

    private Effect appliedEffect;

    void Start()
    {
        this.initialPosition = this.GetComponent<Transform>().position;
    }

    void Update()
    {
        if(amplitude > 0.0f)
        {
            amplitude += -Time.deltaTime * this.desaceleration;
        }
        if(amplitude < 0.0f)
            amplitude = 0.0f;

        if(this.appliedEffect == Effect.HSHAKE)
        {
            this.GetComponent<Transform>().position = new Vector3(initialPosition.x + this.amplitude*(float)Math.Sin(this.shakeFreq*Math.PI*Time.time*2.0), initialPosition.y, initialPosition.z);
        }
    }

    public void ApplyEffect(String effect)
    {
        if(effect == "HSHAKE")
        {
            this.appliedEffect = Effect.HSHAKE;
            this.amplitude = 2.0f;
            this.desaceleration = 5.0f;
        }
    }
}
