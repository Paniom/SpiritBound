using UnityEngine;
using System.Collections;

public class AchievementTracker : MonoBehaviour
{

    public static int Level1Complete = 0;
    public static int Level2Complete = 0;
    public static int Level3Complete = 0;
    public static int Level4Complete = 0;

    public static int Level1CompleteNoDeaths = 0;
    public static int Level2CompleteNoDeaths = 0;
    public static int Level3CompleteNoDeaths = 0;
    public static int Level4CompleteNoDeaths = 0;

    public static int TutorialComplete = 0;

    // Use this for initialization
    void Start()
    {
        SetAchievements();
    }

    public void ClearStats()
    {
        PlayerPrefs.SetInt("TutorialComplete", 0);
        PlayerPrefs.SetInt("Level1Complete", 0);
        PlayerPrefs.SetInt("Level2Complete", 0);
        PlayerPrefs.SetInt("Level3Complete", 0);
        PlayerPrefs.SetInt("Level4Complete", 0);
        PlayerPrefs.SetInt("Level1CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level2CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level3CompleteNoDeaths", 0);
        PlayerPrefs.SetInt("Level4CompleteNoDeaths", 0);
    }


    void SetAchievements()
    {
        //Check for tutorial complete
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            TutorialComplete = PlayerPrefs.GetInt("TutorialComplete");
        }
        else
        {
            PlayerPrefs.SetInt("TutorialComplete", TutorialComplete);
        }

        //Check for level 1 complete
        if (PlayerPrefs.HasKey("Level1Complete"))
        {
            Level1Complete = PlayerPrefs.GetInt("Level1Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level1Complete", Level1Complete);
        }

        //Check for level 2 complete
        if (PlayerPrefs.HasKey("Level2Complete"))
        {
            Level2Complete = PlayerPrefs.GetInt("Level2Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level2Complete", Level2Complete);
        }

        //Check for level 3 complete
        if (PlayerPrefs.HasKey("Level3Complete"))
        {
            Level3Complete = PlayerPrefs.GetInt("Level3Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level3Complete", Level3Complete);
        }

        //Check for level 4 complete
        if (PlayerPrefs.HasKey("Level4Complete"))
        {
            Level4Complete = PlayerPrefs.GetInt("Level4Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level4Complete", Level4Complete);
        }

        //Check for level 1 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level1CompleteNoDeaths"))
        {
            Level1CompleteNoDeaths = PlayerPrefs.GetInt("Level1CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level1CompleteNoDeaths", Level1CompleteNoDeaths);
        }

        //Check for level 2 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level2CompleteNoDeaths"))
        {
            Level2CompleteNoDeaths = PlayerPrefs.GetInt("Level2CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level2CompleteNoDeaths", Level2CompleteNoDeaths);
        }

        //Check for level 3 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level3CompleteNoDeaths"))
        {
            Level3CompleteNoDeaths = PlayerPrefs.GetInt("Level3CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level3CompleteNoDeaths", Level3CompleteNoDeaths);
        }

        //Check for level 4 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level4CompleteNoDeaths"))
        {
            Level4CompleteNoDeaths = PlayerPrefs.GetInt("Level4CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level4CompleteNoDeaths", Level4CompleteNoDeaths);
        }
    }
}
