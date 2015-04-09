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
        if (GetComponent<Animator>().GetBool("playerStanding") == true)
        {

        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ContactPoint cp = other.contacts[0];
            Debug.Log(cp.point);
            if (cp.point.y > collider.bounds.extents.y)
            {
                GetComponent<Animator>().SetBool("playerStanding", true);
            }
        }
    }

    void DestroyThisRock()
    {
        Debug.Log("this is called");
        Destroy(gameObject);
    }

    void RemoveTheCollider()
    {
        collider.enabled = false;
    }

}
