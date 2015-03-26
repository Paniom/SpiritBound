using UnityEngine;
using System.Collections;

public class BrushController : MonoBehaviour {

    float count = 0;
    public GameObject afterCollision;
    public Animator breakDown;
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
                if (breakDown.GetInteger("DoneBreaking") == 0 && breakDown.GetBool("Breaking") == false)
                {
                    breakDown.SetBool("Breaking", true);
                }
                else if (breakDown.GetInteger("DoneBreaking") == 1)
                {
                    collider.enabled = false;
                    afterCollision.SetActive(true);
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/BreakBrush"), transform.position);
                }
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
                if (breakDown.GetInteger("DoneBreaking") == 0 && breakDown.GetBool("Breaking") == false)
                {
                    breakDown.SetBool("Breaking", true);
                }
                else if (breakDown.GetInteger("DoneBreaking") == 1)
                {
                    collider.enabled = false;
                    afterCollision.SetActive(true);
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/BreakBrush"), transform.position);
                }
            }
        }
    }
}
