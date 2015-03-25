using UnityEngine;
using System.Collections;

public class killPlayer : MonoBehaviour {

    public static GameObject lastSpawn;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TimeAndScore.numberOfDeaths++;
            TimeAndScore.timeRemaining = SetLastSpawn.checkpointTime;
            SetLastSpawn.checkpointTime = 0.0f;
            other.transform.position = lastSpawn.transform.position;
        }
    }
}
