using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeAndScore : MonoBehaviour {

    public GameObject scoreText;
    public GameObject timeRemainingText;

    public static int score;
    public static float timeRemaining;

	// Use this for initialization
	void Start () {
        timeRemaining = 3000;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            scoreText.GetComponent<Text>().text = "Score : " + score;
            timeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)timeRemaining;
        }
        else
        {
            //Application.LoadLevel("GameOver"); Should end the game (game over or restart level)
        }
	}
}
