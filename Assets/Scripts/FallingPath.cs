using UnityEngine;
using System.Collections;

public class FallingPath : MonoBehaviour 
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
        if (other.gameObject.tag == "Player")
        {
			GetComponent<Animator>().SetBool("playerStanding", true);
        }
    }

    void DestroyThisRock()
    {
        Destroy(gameObject);
    }

    void RemoveTheCollider()
    {
        collider.enabled = false;
    }

}
