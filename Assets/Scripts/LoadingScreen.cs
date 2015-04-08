using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{

    public static string levelToLoad;
    public GameObject loadingText;
    public GameObject ProgressBar;
    public GameObject spinWheel;

    int loadProgress = 0;

    bool switchText = true;
    float delay = 0.5f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DisplayLoadingScreen(levelToLoad));
    }

    // Update is called once per frame
    void Update() 
    {
        spinWheel.transform.Rotate(0, 0, 1);
        if (delay <= 0)
        {
            delay = 0.5f;
            switchText = true;
        }
        else
        {
            delay -= Time.deltaTime;
        }
        if(switchText)
        {
            switch (loadingText.GetComponent<Text>().text)
                {
                    case "Loading...":
                        {
                            loadingText.GetComponent<Text>().text = "Loading.";
                            break;
                        }
                    case "Loading..":
                        {
                            loadingText.GetComponent<Text>().text = "Loading...";
                            break;
                        }
                    case "Loading.":
                        {
                            loadingText.GetComponent<Text>().text = "Loading..";
                            break;
                        }
                }
            switchText = false;
        }
	}

    IEnumerator DisplayLoadingScreen(string level)
    {
        ProgressBar.GetComponent<Image>().fillAmount = loadProgress;

        AsyncOperation async = Application.LoadLevelAsync(level);
        while (!async.isDone)
        {
            
            loadProgress = (int)async.progress;
            ProgressBar.GetComponent<Image>().fillAmount = loadProgress;
            yield return null;
        }
    }
}
