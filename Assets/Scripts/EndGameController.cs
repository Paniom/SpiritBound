using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameController : MonoBehaviour {

    bool done = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!done)
            {
                done = true;
                TimeAndScore.GameOver = true;
                TimeAndScore.win = true;
                GameObject.Find("ScorePanel").SetActive(false);
                GameObject.Find("UIParent").SetActive(false);
                GameObject.Find("PowerBar").SetActive(false);
                GameObject.Find("TimeRemainingSlider").SetActive(false);
                GameObject.Find("PauseButtonBack").SetActive(false);
                other.GetComponent<PlayerController>().speed = 0;
                other.GetComponent<PlayerController>().enabled = false;
            }
        }
    }
}
