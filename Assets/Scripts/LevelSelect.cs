using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    public GameObject map;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    // Use this for initialization
    void Start()
    {
        AchievementTracker.updateAchievements();
        if (AchievementTracker.Level_1Complete == 1)
        {
            if (AchievementTracker.Level_2Complete != 1 && TimeAndScore.UpdateMap)
            {
                map.GetComponent<Animator>().Play("MapDraw1-2");
                TimeAndScore.UpdateMap = false;
            }
            else if (AchievementTracker.Level_2Complete != 1 && !TimeAndScore.UpdateMap)
            {
                map.GetComponent<Animator>().enabled = false;
                map.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Map1-2(Finish)");
            }
            level2.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_2Complete == 1)
        {
            if (AchievementTracker.Level_3Complete != 1 && TimeAndScore.UpdateMap)
            {
                map.GetComponent<Animator>().Play("MapDraw2-3");
                TimeAndScore.UpdateMap = false;
            }
            else if (AchievementTracker.Level_3Complete != 1 && !TimeAndScore.UpdateMap)
            {
                map.GetComponent<Animator>().enabled = false;
                map.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Map2-3(Finish)");
            }
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
    void Update()
    {

    }
}
