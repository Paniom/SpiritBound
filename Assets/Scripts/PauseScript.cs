using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public GameObject MainPausePanel;
    public GameObject PausedPanel;
    //public GameObject AchievementsPanel;
    public GameObject SettingsPanel;
    public GameObject areYouSure;

    float startTime;
    bool startedUp;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (startTime + .01 < Time.time && startedUp == false)
        {
            MainPausePanel.SetActive(false);
            startedUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MainPausePanel.activeInHierarchy == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Pause()
    {
        if (!TimeAndScore.GameOver)
        {
            MainPausePanel.SetActive(true);
            PausedPanel.SetActive(true);
            SettingsPanel.SetActive(false);
            //AchievementsPanel.SetActive(false);
            areYouSure.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        MainPausePanel.SetActive(false);
        PausedPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        //AchievementsPanel.SetActive(false);
        areYouSure.SetActive(false);
        Time.timeScale = 1;
    }

    public void AreYouSure()
    {
        areYouSure.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("StartScreen");
    }

    public void GoToMainPanel()
    {
        SettingsPanel.SetActive(false);
        PausedPanel.SetActive(true);
        //AchievementsPanel.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void GoToSettings()
    {
        SettingsPanel.SetActive(true);
        PausedPanel.SetActive(false);
        //AchievementsPanel.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void GoToAchievementsPage()
    {
        SettingsPanel.SetActive(true);
        PausedPanel.SetActive(false);
        //AchievementsPanel.SetActive(false);
        areYouSure.SetActive(false);
    }
}
