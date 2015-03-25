using UnityEngine;
using System.Collections;

public class SetLastSpawn : MonoBehaviour {

    public static float checkpointTime = 0.0f;
    public static float deathTime = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && killPlayer.lastSpawn != this.gameObject)
        {
            checkpointTime = Time.time;
            killPlayer.lastSpawn = this.gameObject;
        }
    }
}
