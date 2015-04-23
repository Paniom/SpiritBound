using UnityEngine;
using System.Collections;

public class AchievementTracker : MonoBehaviour
{
    public static int Level_0Complete = 0;
    public static int Level_1Complete = 0;
    public static int Level_2Complete = 0;
    public static int Level_3Complete = 0;
    public static int Level_4Complete = 0;
    public static int Level_5Complete = 0;

    public static int Level_1CompleteNoDeaths = 0;
    public static int Level_2CompleteNoDeaths = 0;
    public static int Level_3CompleteNoDeaths = 0;
    public static int Level_4CompleteNoDeaths = 0;
    public static int Level_5CompleteNoDeaths = 0;

    // Use this for initialization
    void Start()
    {
        SetAchievements();
    }
    
    public static void updateAchievements()
    {
        //Check for tutorial complete
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            Level_0Complete = PlayerPrefs.GetInt("TutorialComplete");
        }
        else
        {
            PlayerPrefs.SetInt("TutorialComplete", Level_0Complete);
        }

        //Check for Level_ 1 complete
        if (PlayerPrefs.HasKey("Level_1Complete"))
        {
            Level_1Complete = PlayerPrefs.GetInt("Level_1Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_1Complete", Level_1Complete);
        }

        //Check for Level_ 2 complete
        if (PlayerPrefs.HasKey("Level_2Complete"))
        {
            Level_2Complete = PlayerPrefs.GetInt("Level_2Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_2Complete", Level_2Complete);
        }

        //Check for Level_ 3 complete
        if (PlayerPrefs.HasKey("Level_3Complete"))
        {
            Level_3Complete = PlayerPrefs.GetInt("Level_3Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_3Complete", Level_3Complete);
        }

        //Check for Level_ 4 complete
        if (PlayerPrefs.HasKey("Level_4Complete"))
        {
            Level_4Complete = PlayerPrefs.GetInt("Level_4Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_4Complete", Level_4Complete);
        }

        //Check for Level_ 5 complete
        if (PlayerPrefs.HasKey("Level_5Complete"))
        {
            Level_5Complete = PlayerPrefs.GetInt("Level_5Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_5Complete", Level_5Complete);
        }

        //Check for Level_ 1 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_1CompleteNoDeaths"))
        {
            Level_1CompleteNoDeaths = PlayerPrefs.GetInt("Level_1CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_1CompleteNoDeaths", Level_1CompleteNoDeaths);
        }

        //Check for Level_ 2 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_2CompleteNoDeaths"))
        {
            Level_2CompleteNoDeaths = PlayerPrefs.GetInt("Level_2CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_2CompleteNoDeaths", Level_2CompleteNoDeaths);
        }

        //Check for Level_ 3 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_3CompleteNoDeaths"))
        {
            Level_3CompleteNoDeaths = PlayerPrefs.GetInt("Level_3CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_3CompleteNoDeaths", Level_3CompleteNoDeaths);
        }

        //Check for Level_ 4 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_4CompleteNoDeaths"))
        {
            Level_4CompleteNoDeaths = PlayerPrefs.GetInt("Level_4CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_4CompleteNoDeaths", Level_4CompleteNoDeaths);
        }

        //Check for Level_ 5 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_5CompleteNoDeaths"))
        {
            Level_5CompleteNoDeaths = PlayerPrefs.GetInt("Level_5CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_5CompleteNoDeaths", Level_5CompleteNoDeaths);
        }
    }


    void SetAchievements()
    {
        //Check for tutorial complete
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            Level_0Complete = PlayerPrefs.GetInt("TutorialComplete");
        }
        else
        {
            PlayerPrefs.SetInt("TutorialComplete", Level_0Complete);
        }

        //Check for Level_ 1 complete
        if (PlayerPrefs.HasKey("Level_1Complete"))
        {
            Level_1Complete = PlayerPrefs.GetInt("Level_1Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_1Complete", Level_1Complete);
        }

        //Check for Level_ 2 complete
        if (PlayerPrefs.HasKey("Level_2Complete"))
        {
            Level_2Complete = PlayerPrefs.GetInt("Level_2Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_2Complete", Level_2Complete);
        }

        //Check for Level_ 3 complete
        if (PlayerPrefs.HasKey("Level_3Complete"))
        {
            Level_3Complete = PlayerPrefs.GetInt("Level_3Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_3Complete", Level_3Complete);
        }

        //Check for Level_ 4 complete
        if (PlayerPrefs.HasKey("Level_4Complete"))
        {
            Level_4Complete = PlayerPrefs.GetInt("Level_4Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_4Complete", Level_4Complete);
        }

        //Check for Level_ 5 complete
        if (PlayerPrefs.HasKey("Level_5Complete"))
        {
            Level_5Complete = PlayerPrefs.GetInt("Level_5Complete");
        }
        else
        {
            PlayerPrefs.SetInt("Level_5Complete", Level_5Complete);
        }

        //Check for Level_ 1 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_1CompleteNoDeaths"))
        {
            Level_1CompleteNoDeaths = PlayerPrefs.GetInt("Level_1CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_1CompleteNoDeaths", Level_1CompleteNoDeaths);
        }

        //Check for Level_ 2 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_2CompleteNoDeaths"))
        {
            Level_2CompleteNoDeaths = PlayerPrefs.GetInt("Level_2CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_2CompleteNoDeaths", Level_2CompleteNoDeaths);
        }

        //Check for Level_ 3 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_3CompleteNoDeaths"))
        {
            Level_3CompleteNoDeaths = PlayerPrefs.GetInt("Level_3CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_3CompleteNoDeaths", Level_3CompleteNoDeaths);
        }

        //Check for Level_ 4 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_4CompleteNoDeaths"))
        {
            Level_4CompleteNoDeaths = PlayerPrefs.GetInt("Level_4CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_4CompleteNoDeaths", Level_4CompleteNoDeaths);
        }

        //Check for Level_ 5 CompleteNoDeaths
        if (PlayerPrefs.HasKey("Level_5CompleteNoDeaths"))
        {
            Level_5CompleteNoDeaths = PlayerPrefs.GetInt("Level_5CompleteNoDeaths");
        }
        else
        {
            PlayerPrefs.SetInt("Level_5CompleteNoDeaths", Level_5CompleteNoDeaths);
        }
    }
}
