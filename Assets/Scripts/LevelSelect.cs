using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    public GameObject map;
    public GameObject level0;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;

    public Sprite levelComplete;
    public Sprite level0Unlocked;
    public Sprite level1Unlocked;
    public Sprite level2Unlocked;
    public Sprite level3Unlocked;
    public Sprite level4Unlocked;
    public Sprite level5Unlocked;

    // Use this for initialization
    void Start()
    {
        AchievementTracker.updateAchievements();
        if (AchievementTracker.Level_0Complete == 1)
        {
            if (AchievementTracker.Level_1Complete != 1 && TimeAndScore.UpdateMap)
            {
                //map.GetComponent<Animator>().Play("MapDraw1-2");
                TimeAndScore.UpdateMap = false;
            }
            else if (AchievementTracker.Level_1Complete != 1 && !TimeAndScore.UpdateMap)
            {
                //map.GetComponent<Animator>().enabled = false;
                //map.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Map1-2(Finish)");
            }
            level0.GetComponent<Image>().sprite = levelComplete;
            level0.GetComponent<Button>().interactable = true;
            level1.GetComponent<Image>().sprite = level1Unlocked;
            level1.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_1Complete == 1)
        {
            if (AchievementTracker.Level_2Complete != 1 && TimeAndScore.UpdateMap)
            {
                //map.GetComponent<Animator>().Play("MapDraw1-2");
                TimeAndScore.UpdateMap = false;
            }
            else if (AchievementTracker.Level_2Complete != 1 && !TimeAndScore.UpdateMap)
            {
                //map.GetComponent<Animator>().enabled = false;
                //map.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Map1-2(Finish)");
            }
            level1.GetComponent<Image>().sprite = levelComplete;
            level2.GetComponent<Image>().sprite = level2Unlocked;
            level2.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_2Complete == 1)
        {
            if (AchievementTracker.Level_3Complete != 1 && TimeAndScore.UpdateMap)
            {
                //map.GetComponent<Animator>().Play("MapDraw2-3");
                TimeAndScore.UpdateMap = false;
            }
            else if (AchievementTracker.Level_3Complete != 1 && !TimeAndScore.UpdateMap)
            {
                //map.GetComponent<Animator>().enabled = false;
                //map.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Map2-3(Finish)");
            }
            level2.GetComponent<Image>().sprite = levelComplete;
            level3.GetComponent<Image>().sprite = level3Unlocked;
            level3.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_3Complete == 1)
        {
            level3.GetComponent<Image>().sprite = levelComplete;
            level4.GetComponent<Image>().sprite = level4Unlocked;
            level4.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_4Complete == 1)
        {
            level4.GetComponent<Image>().sprite = levelComplete;
            level5.GetComponent<Image>().sprite = level5Unlocked;
            level5.GetComponent<Button>().interactable = true;
        }
        if (AchievementTracker.Level_5Complete == 1)
        {
            level5.GetComponent<Image>().sprite = levelComplete;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
