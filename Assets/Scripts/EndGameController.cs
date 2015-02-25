using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameController : MonoBehaviour {

    float TimeTaken = 0;
    float score = 0;

    public GameObject scoreText;
    public GameObject timeRemainingText;
    public GameObject outcomeText;

	// Use this for initialization
	void Start () 
    {
        if (TimeAndScore.win)
            outcomeText.GetComponent<Text>().text = "Level Complete";
        else
            outcomeText.GetComponent<Text>().text = "You ran out of time";
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        scoreText.GetComponent<Text>().text = "Score : " + (int)score;
        timeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)TimeTaken;
        if (TimeTaken < TimeAndScore.timeRemaining)
            TimeTaken += Time.deltaTime*500;
        else if (TimeTaken > TimeAndScore.timeRemaining)
            TimeTaken = TimeAndScore.timeRemaining;

        if (score < TimeAndScore.score)
            score += Time.deltaTime * 100;
        else if (score > TimeAndScore.score)
            score = TimeAndScore.score;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TimeAndScore.GameOver = true;
        }
    }
}
