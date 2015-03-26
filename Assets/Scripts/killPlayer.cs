using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class killPlayer : MonoBehaviour {

    public static GameObject lastSpawn;
    bool dead = false;
    float deathDelay = 1.0f;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (dead)
        {
            if (deathDelay < 0)
            {
                deathDelay = 1.0f;
                dead = false;
            }
            else
            {
                deathDelay -= Time.deltaTime;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!dead)
            {
                dead = true;
                other.GetComponent<PlayerController>().foxPowerLevelUI.GetComponent<Slider>().value = 20;
                other.GetComponent<PlayerController>().wolfPowerLevelUI.GetComponent<Slider>().value = 20;
                TimeAndScore.score -= 10;
                if (TimeAndScore.score < 0)
                {
                    TimeAndScore.score = 0;
                }
                TimeAndScore.numberOfDeaths++;
                TimeAndScore.timeRemaining = SetLastSpawn.checkpointTime;
                other.transform.position = lastSpawn.transform.position;
            }
        }
    }
}
