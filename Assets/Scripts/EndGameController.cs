using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TimeAndScore.GameOver = true;
            TimeAndScore.win = true;
            other.GetComponent<PlayerController>().speed = 0;
            other.GetComponent<PlayerController>().enabled = false;
        }
    }
}
