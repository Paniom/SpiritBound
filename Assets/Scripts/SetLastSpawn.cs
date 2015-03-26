using UnityEngine;
using System.Collections;

public class SetLastSpawn : MonoBehaviour {

    public static float checkpointTime = 300.0f;
	// Use this for initialization
	void Start () {
        checkpointTime = TimeAndScore.timeRemaining;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && killPlayer.lastSpawn != this.gameObject)
        {
            checkpointTime = TimeAndScore.timeRemaining;
            killPlayer.lastSpawn = this.gameObject;
        }
    }
}
