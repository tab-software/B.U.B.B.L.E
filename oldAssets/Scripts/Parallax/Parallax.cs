using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float vel = 0.0f;
    private float offset = 0.0f;

    private void Update()
    {
        this.offset += Time.deltaTime * this.vel;
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", Vector2.right * this.offset);
    }
}
