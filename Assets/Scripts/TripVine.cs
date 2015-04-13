using UnityEngine;
using System.Collections;

public class TripVine : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnCollisionEnter(Collision other)
    {
        
    }

    void DestroyThisVine()
    {
        Destroy(gameObject);
    }
}
