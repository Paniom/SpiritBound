using UnityEngine;
using System.Collections;

public class BrushController : MonoBehaviour {

    float count = 0;
    public GameObject afterCollision;
    public Animator breakDown;
    float breakTimer = 0.1f;
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (breakDown.GetBool("Breaking"))
        {
            if (breakTimer < 0)
            {
                breakTimer = 0.1f;
                collider.enabled = false;
                //afterCollision.SetActive(true);
            }
            else
            {
                breakTimer -= Time.deltaTime;
            }
        }
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
                SetLastSpawn.PiecesToReset.Add(gameObject);
                if (breakDown.GetInteger("DoneBreaking") == 0 && breakDown.GetBool("Breaking") == false)
                {
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/BreakBrush"), transform.position);
                    breakDown.SetBool("Breaking", true);
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
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/BreakBrush"), transform.position);
                    breakDown.SetBool("Breaking", true);
                }
            }
        }
    }

    public void Reset()
    {
        breakDown.SetInteger("DoneBreaking",0);
        breakDown.SetBool("Breaking",false);
        breakTimer = 0.1f;
        collider.enabled = true;
        afterCollision.SetActive(false);
    }
}
