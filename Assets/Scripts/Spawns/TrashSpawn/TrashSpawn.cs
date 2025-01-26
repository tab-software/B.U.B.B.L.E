using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float timeBetweenSpawn = 1.0f;
    private float lastSpawn = 0.0f;
    void Start()
    {
    }

    void Update()
    {
        if(Time.time > (lastSpawn + timeBetweenSpawn))
        {
            Bounds bounds = this.GetComponent<BoxCollider2D>().bounds;
            Instantiate(prefabs[Random.Range(0, this.prefabs.Count)], new Vector2(Random.Range(bounds.min.x, bounds.max.x), this.GetComponent<Transform>().position.y), new Quaternion(0,0,0,0));
            lastSpawn = Time.time;
        }        
    }
}
