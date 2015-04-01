using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public GameObject level2;
    public GameObject level3;


	// Use this for initialization
	void Start () 
    {
        AchievementTracker.updateAchievements();
        if (AchievementTracker.Level_1Complete == 1)
        {
            level2.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_2Complete == 1)
        {
            level3.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_3Complete == 1)
        {
            //level4.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_4Complete == 1)
        {
            //level5.GetComponent<Button>().interactable = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
