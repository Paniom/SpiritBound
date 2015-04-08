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
	public enum DeathType:int { Pit = 0, Geyser = 1, Boulder = 2, Other = 3 }; 
	public DeathType deathType = DeathType.Other;
	private string[] DeathMessage = new string[]{ 	"Your body has fallen but your spirit lives on",
													"Your body has been incinerated but your spirit lives on",
													"Your body has been toppled but your spirit lives on",
													"Needs to be defined but your spirit lives on" };
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
				GameObject deathtext = Instantiate(Resources.Load("DeathText")) as GameObject;
				deathtext.transform.parent = GameObject.Find("UI Canvas").transform;
				deathtext.GetComponentInChildren<Text>().text = DeathMessage[(int)deathType];
				deathtext.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				deathtext.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
				deathtext.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
				Destroy(deathtext, 1.0f);
				respawn = true;
            }
        }
    }
}
