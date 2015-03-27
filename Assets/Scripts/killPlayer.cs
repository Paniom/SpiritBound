using UnityEngine;
using UnityEngine.UI;
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
                GameObject deathtext = Instantiate(Resources.Load("DeathText")) as GameObject;
                deathtext.transform.parent = GameObject.Find("UI Canvas").transform;
                deathtext.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                deathtext.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                deathtext.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Destroy(deathtext, 1.0f);
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
                other.GetComponent<PlayerController>().foxPowerLevelUI.GetComponent<Slider>().value = 20;
                other.GetComponent<PlayerController>().wolfPowerLevelUI.GetComponent<Slider>().value = 20;
                other.rigidbody.velocity = new Vector3(0, 0, 0);
                TimeAndScore.score -= 10;
                if (TimeAndScore.score < 0)
                {
                    TimeAndScore.score = 0;
                }
                TimeAndScore.numberOfDeaths++;
                TimeAndScore.timeRemaining = SetLastSpawn.checkpointTime;
				Time.timeScale = 0;
				respawn = true;
                
            }
        }
    }
}
