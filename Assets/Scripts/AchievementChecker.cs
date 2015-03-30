using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementChecker : MonoBehaviour 
{

    public GameObject Level1Complete;
    public GameObject Level2Complete;
    public GameObject Level3Complete;
    public GameObject Level4Complete;

    public GameObject Level1CompleteNoDeaths;
    public GameObject Level2CompleteNoDeaths;
    public GameObject Level3CompleteNoDeaths;
    public GameObject Level4CompleteNoDeaths;

    public GameObject TutorialComplete;

	// Use this for initialization
	void Start () 
    {
        CheckForAchievements();
	}

    public void CheckForAchievements()
    {
        //Check for tutorial complete
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            if (PlayerPrefs.GetInt("TutorialComplete") == 1)
                TutorialComplete.GetComponent<CanvasGroup>().alpha = 1;
            else
                TutorialComplete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 1 complete
        if (PlayerPrefs.HasKey("Level1Complete"))
        {
            if (PlayerPrefs.GetInt("Level1Complete") == 1)
                Level1Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level1Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 2 complete
        if (PlayerPrefs.HasKey("Level2Complete"))
        {
            if (PlayerPrefs.GetInt("Level2Complete") == 1)
                Level2Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level2Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 3 complete
        if (PlayerPrefs.HasKey("Level3Complete"))
        {
            if (PlayerPrefs.GetInt("Level3Complete") == 1)
                Level3Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level3Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 4 complete
        if (PlayerPrefs.HasKey("Level4Complete"))
        {
            if (PlayerPrefs.GetInt("Level4Complete") == 1)
                Level4Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level4Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 1 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level1CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level1CompleteNoDeaths") == 1)
                Level1CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level1CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 2 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level2CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level2CompleteNoDeaths") == 1)
                Level2CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level2CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 3 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level3CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level3CompleteNoDeaths") == 1)
                Level3CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level3CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for level 4 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level4CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level4CompleteNoDeaths") == 1)
                Level4CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level4CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
    }
}
