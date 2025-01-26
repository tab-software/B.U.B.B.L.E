using System;
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
    public GameObject backgroundGradient;
    private bool showedSuperFish = false;
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
            float progress = ((Time.time - initilizedAt)/levelDuration);
            float y = this.distBar * progress;
            this.logoBar.transform.position = new Vector3(this.logoBar.transform.position.x,
                                                          this.initBar.transform.position.y + y,
                                                          this.logoBar.transform.position.z);
            Vector4 color = new Vector4(1.0f, 1.0f, 1.0f);
            color.x = (1.0f/255.0f) * (32 * Math.Max(1.0f - progress, 0.0f));
            color.y = (1.0f/255.0f) * (54 + 136 * Math.Min(progress, 1.0f));
            color.z = (1.0f/255.0f) * (87 + 168 * Math.Min(progress, 1.0f));
            this.backgroundGradient.GetComponent<Renderer>().material.SetColor("_ColorA", color);
            if (!showedSuperFish && progress > 0.9)
            {
                showedSuperFish = true;
                GameObject.Find("SuperFish").GetComponent<SuperFish>().directionY = 1.0f;
            }
        }
    }
}
