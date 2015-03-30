using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour 
{
    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;
    public GameObject SettingsPanel;
    public GameObject AchievementsPanel;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void StartGame()
    {
        Time.timeScale = 1;
        LoadingScreen.levelToLoad = "Level_1";
        Application.LoadLevel("LoadingScreen");
    }

    public void GoToAchievementsPage()
    {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        AchievementsPanel.SetActive(true);
    }

    public void GoToTitlePage()
    {
        Time.timeScale = 1;
        Application.LoadLevel("StartScreen");
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1;
        LoadingScreen.levelToLoad = "Level_1";
        Application.LoadLevel("LoadingScreen");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        AchievementsPanel.SetActive(false);
    }

    public void GoToCredits()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        AchievementsPanel.SetActive(false);
    }

    public void GoToSettings()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        AchievementsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
