using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GetComponent<Rigidbody>().velocity = new Vector3(4,0,0);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Blocker")
        {
            GetComponent<Rigidbody>().velocity *= -1;
        }
    }
}
