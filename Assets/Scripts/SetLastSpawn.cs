using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetLastSpawn : MonoBehaviour {

    public static List<GameObject> PiecesToReset;
    public static float checkpointTime = 300.0f;
	public static Quaternion RespawnPlayerRot = Quaternion.identity;
    public static Vector3 RespawnPlayerPos = Vector3.zero;

    void Awake()
    {
        PiecesToReset = new List<GameObject>();
    }

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
            PiecesToReset.Clear();
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
