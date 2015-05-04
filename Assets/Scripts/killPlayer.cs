using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class killPlayer : MonoBehaviour {

    public static GameObject lastSpawn;
    bool dead = false;
    float deathDelay = 2.0f;
	bool respawn = false;
	float respawnWait = 2.0f;
	Collider other;
	public enum DeathType:int { Pit = 0, Geyser = 1, Boulder = 2, Other = 3, Water = 4 }; 
	public DeathType deathType = DeathType.Other;
	private string[] DeathMessage = new string[]{ 	"Your body has fallen but your spirit lives on",
													"Your body has been incinerated but your spirit lives on",
													"Your body has been toppled but your spirit lives on",
													"Needs to be defined but your spirit lives on",
													"Your body has sunk but your spirit lives on"};
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
				deathDelay = 2.0f;
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
				respawnWait = 2.0f;
				respawn = false;
				Time.timeScale = 1;
				other.GetComponent<PlayerController>().playerDied(SetLastSpawn.RespawnCameraPos,SetLastSpawn.RespawnCameraRot);
                other.transform.rotation = SetLastSpawn.RespawnPlayerRot;
                other.transform.position = SetLastSpawn.RespawnPlayerPos;
			}
		}
	}

    void OnTriggerEnter(Collider o)
    {
		if(deathType != DeathType.Water)
		{
			playerDied(o);
		}
    }

	public void playerDied(Collider o)
	{
		other = o;
		if (other.tag == "Player")
		{
			if (!dead)
			{
				dead = true;
				
				if (deathType == DeathType.Pit)
				{
					if (other.GetComponent<PlayerController>().duskaloAnimator && other.GetComponent<PlayerController>().stateMachine.getState() == "Muskalo")
						other.GetComponent<PlayerController>().duskaloAnimator.SetTrigger("deathPit");
					else if (other.GetComponent<PlayerController>().foxAnimator && other.GetComponent<PlayerController>().stateMachine.getState() == "Fox")
						other.GetComponent<PlayerController>().foxAnimator.SetTrigger("deathPit");
					else if (other.GetComponent<PlayerController>().wolfAnimator && other.GetComponent<PlayerController>().stateMachine.getState() == "Wolf")
						other.GetComponent<PlayerController>().wolfAnimator.SetTrigger("deathPit");
				}
				else
				{
					if (other.GetComponent<PlayerController>().duskaloAnimator && other.GetComponent<PlayerController>().stateMachine.getState() == "Muskalo")
						other.GetComponent<PlayerController>().duskaloAnimator.SetTrigger("deathStruck");
					else if (other.GetComponent<PlayerController>().foxAnimator && other.GetComponent<PlayerController>().stateMachine.getState() == "Fox")
						other.GetComponent<PlayerController>().foxAnimator.SetTrigger("deathStruck");
					else if (other.GetComponent<PlayerController>().wolfAnimator && other.GetComponent<PlayerController>().stateMachine.getState() == "Wolf")
						other.GetComponent<PlayerController>().wolfAnimator.SetTrigger("deathStruck");
				}
				
				foreach(GameObject gameObj in SetLastSpawn.PiecesToReset)
				{
					if (gameObj.tag == "Pickup")
					{
						gameObj.GetComponent<PickUpController>().Reset();
					}
					else if (gameObj.tag == "Breakable")
					{
						gameObj.GetComponent<BrushController>().Reset();
					}
					else if (gameObj.tag == "FallingPath")
					{
						gameObj.GetComponent<FallingPath>().Reset();
					}
				}
				
				other.GetComponent<PlayerController>().foxPowerLevelUI.GetComponent<Slider>().value = 15;
				other.GetComponent<PlayerController>().wolfPowerLevelUI.GetComponent<Slider>().value = 15;
                other.GetComponent<PlayerController>().hitWall = false;
                other.GetComponent<PlayerController>().ChangeToMuskalo();
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
				deathtext.transform.SetParent(GameObject.Find("HintsPanel").transform);
				deathtext.GetComponentInChildren<Text>().text = DeathMessage[(int)deathType];
				deathtext.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				deathtext.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
				deathtext.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
				Destroy(deathtext, 2.0f);
				respawn = true;
			}
		}
	}

	void OnTriggerStay()
	{

	}
}
