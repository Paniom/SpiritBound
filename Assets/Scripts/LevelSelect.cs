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

    public GameObject LevelDetails;
    public GameObject levelDetails_name;
    public GameObject levelDetails_stats;

    public void ShowLevelDetails(string level)
    {
        LevelDetails.SetActive(true);
        switch (level)
        {
            case "Level1":
                {
                    levelDetails_name.GetComponent<Text>().text = "Level 1 ";
                    if(AchievementTracker.Level_0Complete == 1)
                        levelDetails_stats.GetComponent<Text>().text = "Fastest time: " + AchievementTracker.level1FastestTime+ "\n" + "High Score: " + AchievementTracker.level1HighScore;
                    else
                        levelDetails_stats.GetComponent<Text>().text = "Locked";
                    break;
                }
            case "Level2":
                {
                    levelDetails_name.GetComponent<Text>().text = "Level 2 ";
                    if (AchievementTracker.Level_1Complete == 1)
                        levelDetails_stats.GetComponent<Text>().text = "Fastest time: " + AchievementTracker.level2FastestTime + "\n" + "High Score: " + AchievementTracker.level2HighScore; 
                    else
                        levelDetails_stats.GetComponent<Text>().text = "Locked";
                    break;
                }
            case "Level3":
                {
                    levelDetails_name.GetComponent<Text>().text = "Level 3 ";
                    if(AchievementTracker.Level_2Complete == 1)
                        levelDetails_stats.GetComponent<Text>().text = "Fastest time: " + AchievementTracker.level3FastestTime + "\n" + "High Score: " + AchievementTracker.level3HighScore; 
                    else
                        levelDetails_stats.GetComponent<Text>().text = "Locked";
                    break;
                }
            case "Level4":
                {
                    levelDetails_name.GetComponent<Text>().text = "Level 4 ";
                    levelDetails_stats.GetComponent<Text>().text = "Locked : Coming Soon";
                    //levelDetails_stats.GetComponent<Text>().text = "Fastest time: " + AchievementTracker.level4FastestTime + "\n" + "High Score: " + AchievementTracker.level4HighScore; 
                    break;
                }
            case "Level5":
                {
                    levelDetails_name.GetComponent<Text>().text = "Level 5 ";
                    levelDetails_stats.GetComponent<Text>().text = "Locked : Coming Soon";
                    //levelDetails_stats.GetComponent<Text>().text = "Fastest time: " + AchievementTracker.level5FastestTime + "\n" + "High Score: " + AchievementTracker.level5HighScore; 
                    break;
                }
        }
    }

    public void HideLevelDetails()
    {
        LevelDetails.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        //LevelDetails = Instantiate(Resources.Load("LevelDetails")) as GameObject;
        //LevelDetails.transform.SetParent(GameObject.Find("Canvas").transform);
        //LevelDetails.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
        //LevelDetails.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        //LevelDetails.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        LevelDetails.SetActive(false);

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
            //level4.GetComponent<Image>().sprite = level4Unlocked;
            //level4.GetComponent<Button>().interactable = true;
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
