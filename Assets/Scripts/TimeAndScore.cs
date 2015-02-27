using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeAndScore : MonoBehaviour {

    public GameObject scoreText;
    public GameObject timeRemainingText;

    public static int score;
    public static float timeRemaining;

    public static bool GameOver;
    public static bool win = false;

    public GameObject gameOverPanel;

    void Awake()
    {
        score = 0;
        GameOver = false;
        win = false;
        timeRemaining = 1000;
    }

	// Use this for initialization
	void Start () {
        timeRemaining = 1000;
        score = 0;
	}

	// Update is called once per frame
	void Update () {
        if (!GameOver)
        {
            if (Input.GetKey(KeyCode.L))
            {
                win = true;
                GameOver = true;
            }
            if (timeRemaining <= 150)
            {
                GetComponent<AudioSource>().pitch = 1.01f;
            }
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                scoreText.GetComponent<Text>().text = "Score : " + score;
                timeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)timeRemaining;
            }
            else
            {
                GameOver = true;
            }
        }
        else
        {
            this.enabled = false;
            gameOverPanel.SetActive(true);
        }
	}
}
