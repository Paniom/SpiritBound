using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeAndScore : MonoBehaviour 
{

    public GameObject scoreText;
    public GameObject timeRemainingText;

    public static int score;
    public static float timeRemaining;

    public static bool GameOver;
    public static bool win = false;

    public GameObject gameOverPanel;
    public GameObject timerSlider;
    public GameObject sunSlider;
    public GameObject moonSlider;

    void Awake()
    {
        score = 0;
        GameOver = false;
        win = false;
        timeRemaining = 1000;
        if(timerSlider!= null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
        if (moonSlider != null)
            moonSlider.GetComponent<Slider>().maxValue = timeRemaining*2;
        if (sunSlider != null)
            sunSlider.GetComponent<Slider>().maxValue = timeRemaining*2;
    }

	// Use this for initialization
	void Start () 
    {
        score = 0;
        GameOver = false;
        win = false;
        timeRemaining = 1000;
        if (timerSlider != null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
        if (moonSlider != null)
            moonSlider.GetComponent<Slider>().maxValue = timeRemaining * 2;
        if (sunSlider != null)
            sunSlider.GetComponent<Slider>().maxValue = timeRemaining * 2;
	}

	// Update is called once per frame
	void Update () 
    {
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
                if (timerSlider != null)
                    timerSlider.GetComponent<Slider>().value = timeRemaining;
                if (moonSlider != null)
                    moonSlider.GetComponent<Slider>().value = timeRemaining;
                if (sunSlider != null)
                    sunSlider.GetComponent<Slider>().value = timeRemaining;

                scoreText.GetComponent<Text>().text = "Score : " + score;
                timeRemainingText.GetComponent<Text>().text = "Time Remaining : " + (int)timeRemaining;
            }
            else
            {
                timeRemaining = 0;
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
