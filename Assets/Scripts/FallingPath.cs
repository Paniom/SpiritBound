using UnityEngine;
using System.Collections;

public class FallingPath : MonoBehaviour 
{
    bool done = false;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !done)
        {
            GetComponent<Animator>().SetBool("reset", false);
            done = true;
            SetLastSpawn.PiecesToReset.Add(gameObject);
			GetComponent<Animator>().SetBool("playerStanding", true);
        }
    }

    void OnCollisionExit()
    {
        done = false;
        GetComponent<Animator>().SetBool("playerStanding", false);
    }

    void DestroyThisRock()
    {
        gameObject.SetActive(false);
    }

    void RemoveTheCollider()
    {
        collider.enabled = false;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        done = false;
        GetComponent<Animator>().SetBool("reset", true);
        GetComponent<Animator>().SetBool("playerStanding", false);
        collider.enabled = true;
    }

}
