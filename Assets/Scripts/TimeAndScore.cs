using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeAndScore : MonoBehaviour 
{
    public GameObject scoreText;
    public GameObject coinText;
    public GameObject gemText;

    public GameObject finalScoreText;
    public GameObject finalTimeRemainingText;
    public GameObject finalOutcomeText;

    public static int score;
    public static int coins;
    public static int gems;
    public static float timeRemaining;

    public static bool GameOver;
    public static bool win = false;

    public GameObject gameOverPanel;
    public GameObject timerSlider;

    float TimeTaken = 0;
    float finalScore = 0;

    bool doneCalculatingScore = false;
    bool doneCalculatingTime = false;

    public AudioClip timeTick;
    public AudioClip scoreTick;

    void Awake()
    {
        doneCalculatingScore = false;
        doneCalculatingTime = false;
        score = 0;
        coins = 0;
        gems = 0;
        GameOver = false;
        win = false;
        timeRemaining = 300;
        if(timerSlider!= null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
    }

	// Use this for initialization
	void Start () 
    {
        doneCalculatingScore = false;
        doneCalculatingTime = false;
        score = 0;
        coins = 0;
        gems = 0;
        GameOver = false;
        win = false;
        timeRemaining = 300;
        if (timerSlider != null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
	}

	// Update is called once per frame
	void Update () 
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (!GameOver)
        {
            if (Input.GetKey(KeyCode.L))
            {
                win = true;
                GameOver = true;
            }
            if (timeRemaining <= 30)
            {
                GetComponent<AudioSource>().pitch = 1.02f;
            }
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                if (timerSlider != null)
                    timerSlider.GetComponent<Slider>().value = timeRemaining;
				if(scoreText != null)
	                scoreText.GetComponent<Text>().text = "Score  " + score;
				if(coinText != null)
                	coinText.GetComponent<Text>().text = coins + " / 10  coins";
				if(gemText != null)
					gemText.GetComponent<Text>().text = gems + " / 10  gems";
            }
            else
            {
                timeRemaining = 0;
                doneCalculatingTime = true;
                GameOver = true;
            }
        }
        else
        {
            gameOverPanel.SetActive(true);
            if (win)
                finalOutcomeText.GetComponent<Text>().text = "Level Complete";
            else
                finalOutcomeText.GetComponent<Text>().text = "You ran out of time";

            finalScoreText.GetComponent<Text>().text = "Score : " + (int)finalScore;
            finalTimeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)TimeTaken;
            if (TimeTaken < timeRemaining)
            {
                TimeTaken += Time.deltaTime * 100;
                Debug.Log(audio.isPlaying);
                if (!audio.isPlaying)
                {
                    audio.clip = timeTick;
                    audio.Play();
                }
            }
            else if (TimeTaken > timeRemaining)
            {
                TimeTaken = timeRemaining;
                doneCalculatingTime = true;
            }

            if (finalScore < score && doneCalculatingTime)
            {
                finalScore += Time.deltaTime *10;
                if (!audio.isPlaying)
                {
                    audio.clip = scoreTick;
                    audio.Play();
                }
            }
            else if (finalScore > score)
            {
                finalScore = score;
                doneCalculatingScore = true;
            }
            
        }
        if (doneCalculatingScore && doneCalculatingTime)
        {
            this.enabled = false;
        }
        
	}
}
