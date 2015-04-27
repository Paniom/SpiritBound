using UnityEngine;
using System.Collections;

public class GoTo : MonoBehaviour 
{
    public GameObject MainMenuPanel;
    public GameObject CreditsPanel;
    public GameObject SettingsPanel;
    public GameObject AchievementsPanel;
    public GameObject AreYouSure;

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
        Application.LoadLevel("LevelSelect");
        EndGameController.done = false;
    }

    public void GoToAchievementsPage()
    {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        AchievementsPanel.SetActive(true);
        EndGameController.done = false;
    }

    public void GoToTitlePage()
    {
        Time.timeScale = 1;
        Application.LoadLevel("StartScreen");
        EndGameController.done = false;
    }

    public void GoToLevel(string nameOfLevel)
    {
        Time.timeScale = 1;
        LoadingScreen.levelToLoad = nameOfLevel;
        Application.LoadLevel("LoadingScreen");
        EndGameController.done = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        AchievementsPanel.SetActive(false);
        EndGameController.done = false;
    }

    public void GoToCredits()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        AchievementsPanel.SetActive(false);
        EndGameController.done = false;
    }

    public void GoToSettings()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        AchievementsPanel.SetActive(false);
        EndGameController.done = false;
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ResetPrompt()
    {
        AreYouSure.SetActive(true);
    }

    public void DontClearStats()
    {
        AreYouSure.SetActive(false);
    }

    public void ClearStats()
    {
        AreYouSure.SetActive(false);
        //AchievementTracker.level1HighScore = 0;
        //AchievementTracker.level2HighScore = 0;
        //AchievementTracker.level3HighScore = 0;
        //AchievementTracker.level4HighScore = 0;
        //AchievementTracker.level5HighScore = 0;

        //AchievementTracker.level1FastestTime = 0;
        //AchievementTracker.level2FastestTime = 0;
        //AchievementTracker.level3FastestTime = 0;
        //AchievementTracker.level4FastestTime = 0;
        //AchievementTracker.level5FastestTime = 0;

        //AchievementTracker.Level_0Complete = 0;
        //AchievementTracker.Level_1Complete = 0;
        //AchievementTracker.Level_2Complete = 0;
        //AchievementTracker.Level_3Complete = 0;
        //AchievementTracker.Level_4Complete = 0;
        //AchievementTracker.Level_5Complete = 0;

        //AchievementTracker.Level_1CompleteNoDeaths = 0;
        //AchievementTracker.Level_2CompleteNoDeaths = 0;
        //AchievementTracker.Level_3CompleteNoDeaths = 0;
        //AchievementTracker.Level_4CompleteNoDeaths = 0;
        //AchievementTracker.Level_5CompleteNoDeaths = 0;

        PlayerPrefs.SetInt("Level_0Complete", 0);
        PlayerPrefs.SetInt("Level_1Complete", 0);
        PlayerPrefs.SetInt("Level_2Complete", 0);
        PlayerPrefs.SetInt("Level_3Complete", 0);
        PlayerPrefs.SetInt("Level_4Complete", 0);
        PlayerPrefs.SetInt("Level_5Complete", 0);

        PlayerPrefs.SetInt("Level_1CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level_2CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level_3CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level_4CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level_5CompleteNoDeaths", 0);

        PlayerPrefs.SetInt("level1HighScore", 0);
        PlayerPrefs.SetInt("level2HighScore", 0);
        PlayerPrefs.SetInt("level3HighScore", 0);
        PlayerPrefs.SetInt("level4HighScore", 0);
        PlayerPrefs.SetInt("level5HighScore", 0);

        PlayerPrefs.SetInt("level1FastestTime", 0);
        PlayerPrefs.SetInt("level2FastestTime", 0);
        PlayerPrefs.SetInt("level3FastestTime", 0);
        PlayerPrefs.SetInt("level4FastestTime", 0);
        PlayerPrefs.SetInt("level5FastestTime", 0);
        AchievementTracker.updateAchievements();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
