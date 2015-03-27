using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    public static string levelToLoad;
    public GameObject loadingText;
    public GameObject ProgressBar;

    int loadProgress = 0;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(DisplayLoadingScreen(levelToLoad));
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    IEnumerator DisplayLoadingScreen(string level)
    {
        Debug.Log(levelToLoad);
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
