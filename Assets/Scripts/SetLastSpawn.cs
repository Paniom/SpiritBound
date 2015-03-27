using UnityEngine;
using System.Collections;

public class SetLastSpawn : MonoBehaviour {

    public static float checkpointTime = 300.0f;
	public int playerRot = 0;
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
            if (this.gameObject.transform.parent.gameObject.GetComponentInChildren<ParticleSystem>() != null && name != "Start_Zone" && name != "FinishZone")
            {
                this.gameObject.transform.parent.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
            checkpointTime = TimeAndScore.timeRemaining;
            killPlayer.lastSpawn = this.gameObject;
        }
    }
}
