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
    public GameObject finalGemText;
    public GameObject finalCoinText;
    public GameObject finalDeaths;

    public static int score;
    public static int coins;
    public static int gems;
    public static float timeRemaining;
    public static int numberOfDeaths;

    int TotalCoins = 0;
    int TotalGems = 0;

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

    public GameObject retryButton;
    public GameObject continueButton;

    void Awake()
    {
        doneCalculatingScore = false;
        doneCalculatingTime = false;
        numberOfDeaths = 0;
        score = 0;
        coins = 0;
        gems = 0;
        GameOver = false;
        win = false;
        timeRemaining = 120;
        if(timerSlider!= null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
    }

	// Use this for initialization
	void Start () 
    {
        doneCalculatingScore = false;
        doneCalculatingTime = false;
        numberOfDeaths = 0;
        score = 0;
        coins = 0;
        gems = 0;
        GameOver = false;
        win = false;
        timeRemaining = 120;
        if (timerSlider != null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
	}

	// Update is called once per frame
	void Update () 
    {
        if (TotalCoins == 0)
        {
            TotalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        }
        if (TotalGems == 0)
        {
            TotalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        }
        AudioSource audio = GetComponent<AudioSource>();
        if (!GameOver)
        {
            if (Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Q))
            {
                win = true;
                GameOver = true;
                GameObject.Find("ScorePanel").SetActive(false);
                GameObject.Find("UIParent").SetActive(false);
                GameObject.Find("PowerBar").SetActive(false);
                GameObject.Find("TimeRemainingSlider").SetActive(false);
                GameObject.Find("PauseButtonBack").SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().speed = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
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
	                scoreText.GetComponent<Text>().text = score.ToString();
				if(coinText != null)
                    coinText.GetComponent<Text>().text = coins + "/" + TotalCoins + " coins";
				if(gemText != null)
					gemText.GetComponent<Text>().text = gems + "/" + TotalGems + " gems";
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
            if(GameObject.Find("ScorePanel"))
                GameObject.Find("ScorePanel").SetActive(false);
            if (GameObject.Find("UIParent"))
                GameObject.Find("UIParent").SetActive(false);
            if (GameObject.Find("PowerBar"))
                GameObject.Find("PowerBar").SetActive(false);
            if (GameObject.Find("TimeRemainingSlider"))
                GameObject.Find("TimeRemainingSlider").SetActive(false);
            if (GameObject.Find("PauseButtonBack"))
                GameObject.Find("PauseButtonBack").SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().speed = 0;
            gameOverPanel.SetActive(true);
            if (win)
            {
                finalOutcomeText.GetComponent<Text>().text = "Level Complete!";
                retryButton.SetActive(false);
            }
            else
            {
                finalOutcomeText.GetComponent<Text>().text = "You ran out of time";
                continueButton.SetActive(false);
            }

            finalScoreText.GetComponent<Text>().text = ((int)finalScore).ToString();
            finalDeaths.GetComponent<Text>().text = "Number of Deaths : " + numberOfDeaths;
            finalGemText.GetComponent<Text>().text = "Gems : " + gems + "/" + TotalGems;
            finalCoinText.GetComponent<Text>().text = "Coins : " + coins + "/" + TotalCoins;
            finalTimeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)TimeTaken;
            if (TimeTaken <= timeRemaining && doneCalculatingScore)
            {
                TimeTaken += Time.deltaTime * 100;
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

            if (finalScore <= score)
            {
                finalScore += Time.deltaTime *100;
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
            finalScoreText.GetComponent<Text>().text = ((int)finalScore).ToString();
            finalDeaths.GetComponent<Text>().text = "Number of Deaths : " + numberOfDeaths;
            finalGemText.GetComponent<Text>().text = "Gems : " + gems + "/" + TotalGems;
            finalCoinText.GetComponent<Text>().text = "Coins : " + coins + "/" + TotalCoins;
            finalTimeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)TimeTaken;
            this.enabled = false;
        }
        
	}
}
