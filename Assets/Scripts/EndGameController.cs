﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameController : MonoBehaviour {

    public static bool done = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!done)
            {
                done = true;
                string levelname = Application.loadedLevelName + "Complete";
                PlayerPrefs.SetInt(levelname, 1);
                if (TimeAndScore.numberOfDeaths == 0)
                {
                    levelname += "NoDeaths";
                    PlayerPrefs.SetInt(levelname, 1);
                }
                TimeAndScore.score -= TimeAndScore.numberOfDeaths * 10;
                TimeAndScore.score += (int)TimeAndScore.timeRemaining;
                TimeAndScore.score += (int)GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().foxPowerLevelUI.GetComponent<Slider>().value * 20;
                TimeAndScore.score += (int)GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().wolfPowerLevelUI.GetComponent<Slider>().value * 20;
                AchievementTracker.updateAchievements();
                TimeAndScore.GameOver = true;
                TimeAndScore.UpdateMap = true;
                TimeAndScore.win = true;
                GameObject.Find("ScorePanel").SetActive(false);
                GameObject.Find("UIParent").SetActive(false);
                GameObject.Find("PowerBar").SetActive(false);
                GameObject.Find("TimeRemainingSlider").SetActive(false);
                GameObject.Find("PauseButtonBack").SetActive(false);
                other.GetComponent<PlayerController>().speed = 0;
            }
        }
    }
}
