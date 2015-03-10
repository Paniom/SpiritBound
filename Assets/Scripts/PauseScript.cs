using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public GameObject PausePanel;

    float startTime;
    bool startedUp;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (startTime + .01 < Time.time && startedUp == false)
        {
            PausePanel.SetActive(false);
            startedUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PausePanel.activeInHierarchy == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Pause()
    {
        if (!TimeAndScore.GameOver)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
