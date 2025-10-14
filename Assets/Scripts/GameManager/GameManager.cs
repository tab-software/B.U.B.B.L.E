using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawners;
    public bool initilized = false;
    public GameObject shottedBox;
    public GameObject PauseGameObject;
    void Start()
    {
        
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
                GameObject.Find("SuperFish").GetComponent<SuperFish>().directionY = -1.0f;
                this.initilized = true;
            }
        }
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
    }
}
