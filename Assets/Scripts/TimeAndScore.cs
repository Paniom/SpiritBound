﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeAndScore : MonoBehaviour 
{
    public GameObject scoreText;
    public GameObject coinText;
    public GameObject gemText;

    public static int score;
    public static int coins;
    public static int gems;
    public static float timeRemaining;

    public static bool GameOver;
    public static bool win = false;

    public GameObject gameOverPanel;
    public GameObject timerSlider;

    void Awake()
    {
        score = 0;
        coins = 0;
        gems = 0;
        GameOver = false;
        win = false;
        timeRemaining = 1000;
        if(timerSlider!= null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
    }

	// Use this for initialization
	void Start () 
    {
        score = 0;
        coins = 0;
        gems = 0;
        GameOver = false;
        win = false;
        timeRemaining = 1000;
        if (timerSlider != null)
            timerSlider.GetComponent<Slider>().maxValue = timeRemaining;
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

                scoreText.GetComponent<Text>().text = "Score  " + score;
                coinText.GetComponent<Text>().text = coins + " / 10  coins";
                gemText.GetComponent<Text>().text = gems + " / 10  gems";
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
