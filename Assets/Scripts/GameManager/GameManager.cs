using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawners;
    public bool initilized = false;
    public float levelDuration = 60.0f;
    private float initilizedAt = 0.0f;
    public GameObject shottedBox;
    public GameObject PauseGameObject;

    public GameObject initBar;
    public GameObject endBar;
    public GameObject logoBar;
    public GameObject progressBar;
    private float distBar = 0.0f;
    void Start()
    {
        this.distBar = this.endBar.transform.position.y - this.initBar.transform.position.y;
        this.logoBar.transform.position = this.initBar.transform.position;
        this.progressBar.SetActive(false);
        Debug.Log(this.distBar);
    }

    void Update()
    {
        if(!this.initilized)
        {
            if(shottedBox.GetComponent<BubbleableObject>().bubbled)
            {
                foreach (GameObject spawner in spawners)
                {
                    spawner.GetComponent<Spawner>().Enable();
                }
                this.progressBar.SetActive(true);
                this.initilizedAt = Time.time;
                GameObject.Find("SuperFish").GetComponent<SuperFish>().directionY = -1.0f;
                this.initilized = true;
            }
        }
        if(this.initilized)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(this.PauseGameObject.activeSelf)
                {
                    this.PauseGameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                }
                else
                {
                    this.PauseGameObject.SetActive(true);
                    Time.timeScale = 0.0f;    
                }
            }
            float y = this.distBar * ((Time.time - initilizedAt)/levelDuration);
            this.logoBar.transform.position = new Vector3(this.logoBar.transform.position.x,
                                                          this.initBar.transform.position.y + y,
                                                          this.logoBar.transform.position.z);
        }
    }
}
