using UnityEngine;
using System.Collections;

public class BrushController : MonoBehaviour {

    float count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (count > 30)
        {
            //Hint to player to use the muskalo's charge to destroy brush
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        count += Time.deltaTime;
        GameObject other = collision.collider.gameObject;
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().stateMachine.getState() == "Muskalo" && other.GetComponent<PlayerController>().interacting)
            {
                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/BreakBrush"), transform.position);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        count += Time.deltaTime;
        GameObject other = collision.collider.gameObject;
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().stateMachine.getState() == "Muskalo" && other.GetComponent<PlayerController>().interacting)
            {
                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/BreakBrush"), transform.position);
                Destroy(gameObject);
            }
        }
    }
}
