using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialZones : MonoBehaviour 
{
    public enum TutorialType : int { Movement = 0, Jumps = 1, Bash = 2, SwitchSpirits = 3, 
                                    PowerDeplete = 4, Pickups = 5, DaySpiritPower = 6, NightSpiritPower = 7,Checkpoints = 8, FinalSay = 9, other = 10};
    public TutorialType tutorialType = TutorialType.other;
    private string[]TutorialMessage = new string[]{ "WASD Keys to move around",
                                    "Space to jump over obstacles and hazards",
                                    "The duskalo can bash through light obstacles like brush",
                                    "You are being helped by a day and night spirit, press 1, 2, or 3 to switch animals",
                                    "As you stay in spirit form, your power depletes - day spirit orange bar, night spirit dark blue bar",
                                    "Gem’s help refill your power bar, some are specific to a certain bar",
                                    "The day spirit can walk on steep ground, while the night spirit can walk on water",
                                    "The night spirit can jump high, the day spirit jumps far",
                                    "Ancient obelisks are checkpoints, you return to the last one you passed when you die",
                                    "Finishing faster, picking up coins, and dying less increase your score!",
                                    "Default tutorial message"};

    GameObject tutorialtext;

    // Use this for initialization
    void Start()
    {
        tutorialtext = Instantiate(Resources.Load("DeathText")) as GameObject;
        tutorialtext.transform.SetParent(GameObject.Find("UI Canvas").transform);
        tutorialtext.GetComponentInChildren<Text>().text = TutorialMessage[(int)tutorialType];
        tutorialtext.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        tutorialtext.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        tutorialtext.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        tutorialtext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialtext.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialtext.SetActive(false);
        }
    }
}
