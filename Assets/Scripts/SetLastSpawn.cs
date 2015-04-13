using UnityEngine;
using System.Collections;

public class SetLastSpawn : MonoBehaviour {

    public static float checkpointTime = 300.0f;
	public static Quaternion RespawnPlayerRot = Quaternion.identity;
    public static Vector3 RespawnPlayerPos = Vector3.zero;
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
            RespawnPlayerRot = other.transform.rotation;
            RespawnPlayerPos = other.transform.position;
            checkpointTime = TimeAndScore.timeRemaining;
            killPlayer.lastSpawn = this.gameObject;
        }
    }
}
