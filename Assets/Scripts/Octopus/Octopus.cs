using UnityEngine;

public class Octopus : MonoBehaviour
{
    public float radius = 5f;
    public GameObject OctopusRef;
    public float speed = 2f;
    private float angle = 0f;

    private void Start()
    {
        this.radius = Vector3.Distance(OctopusRef.transform.position, this.transform.position);
    }

    private void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(this.OctopusRef.transform.position.x + x, this.OctopusRef.transform.position.y + y, transform.position.z);
    }
}
