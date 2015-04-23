using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementChecker : MonoBehaviour 
{
    public GameObject Level_1Complete;
    public GameObject Level_2Complete;
    public GameObject Level_3Complete;
    public GameObject Level_4Complete;
    public GameObject Level_5Complete;

    public GameObject Level_1CompleteNoDeaths;
    public GameObject Level_2CompleteNoDeaths;
    public GameObject Level_3CompleteNoDeaths;
    public GameObject Level_4CompleteNoDeaths;
    public GameObject Level_5CompleteNoDeaths;

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
        else
        {
            TutorialComplete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 1 complete
        if (PlayerPrefs.HasKey("Level_1Complete"))
        {
            if (PlayerPrefs.GetInt("Level_1Complete") == 1)
                Level_1Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_1Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_1Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 2 complete
        if (PlayerPrefs.HasKey("Level_2Complete"))
        {
            if (PlayerPrefs.GetInt("Level_2Complete") == 1)
                Level_2Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_2Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_2Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 3 complete
        if (PlayerPrefs.HasKey("Level_3Complete"))
        {
            if (PlayerPrefs.GetInt("Level_3Complete") == 1)
                Level_3Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_3Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_3Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 4 complete
        if (PlayerPrefs.HasKey("Level_4Complete"))
        {
            if (PlayerPrefs.GetInt("Level_4Complete") == 1)
                Level_4Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_4Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_4Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 5 complete
        if (PlayerPrefs.HasKey("Level_5Complete"))
        {
            if (PlayerPrefs.GetInt("Level_5Complete") == 1)
                Level_5Complete.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_5Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_5Complete.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 1 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_1CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level_1CompleteNoDeaths") == 1)
                Level_1CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_1CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_1CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 2 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_2CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level_2CompleteNoDeaths") == 1)
                Level_2CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_2CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_2CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 3 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_3CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level_3CompleteNoDeaths") == 1)
                Level_3CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_3CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_3CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 4 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_4CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level_4CompleteNoDeaths") == 1)
                Level_4CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_4CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_4CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }

        //Check for Level_ 5 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_5CompleteNoDeaths"))
        {
            if (PlayerPrefs.GetInt("Level_5CompleteNoDeaths") == 1)
                Level_5CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 1;
            else
                Level_5CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
        else
        {
            Level_5CompleteNoDeaths.GetComponent<CanvasGroup>().alpha = 0.39f;
        }
    }
}
