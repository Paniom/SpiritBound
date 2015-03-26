using UnityEngine;
using System.Collections;

public class killPlayer : MonoBehaviour {

    public static GameObject lastSpawn;
    bool dead = false;
    float deathDelay = 1.0f;
	bool respawn = false;
	float respawnWait = 1.0f;
	Collider other;
	// Use this for initialization
	void Start () {
	    
	}

	void FixedUpdate ()
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

	// Update is called once per frame
	void Update () 
    {
		if (respawn)
		{
			respawnWait -= Time.unscaledDeltaTime;
			if (respawnWait < 0)
			{
				respawnWait = 1.0f;
				respawn = false;
				Time.timeScale = 1;
				other.GetComponent<PlayerController>().newRotation(lastSpawn.GetComponent<SetLastSpawn>().playerRot);
				other.transform.position = lastSpawn.transform.position;
			}
		}
	}

    void OnTriggerEnter(Collider o)
    {
		other = o;
        if (other.tag == "Player")
        {
            if (!dead)
            {
                dead = true;
                TimeAndScore.numberOfDeaths++;
                TimeAndScore.timeRemaining = SetLastSpawn.checkpointTime;
				Time.timeScale = 0;
				respawn = true;
                
            }
        }
    }
}
