using UnityEngine;
using System.Collections;

public class StayWithTarget : MonoBehaviour {

    public GameObject Target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    transform.position = new Vector3(Target.transform.position.x,Target.transform.position.y,Target.transform.position.z);
	}
}
