using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float timeBetweenSpawn = 1.0f;
    private float lastSpawn = 0.0f;
    private bool enabledSpawn = false;

    public void Enable()
    {
        this.enabledSpawn = true;
        this.lastSpawn = Time.time;
    }

    private void Update()
    {
        if(Time.time > (lastSpawn + timeBetweenSpawn) && this.enabledSpawn)
        {
            Bounds bounds = this.GetComponent<BoxCollider2D>().bounds;
            Instantiate(prefabs[Random.Range(0, this.prefabs.Count)], new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y)), new Quaternion(0,0,0,0));
            lastSpawn = Time.time;
        }        
    }
}
