using UnityEngine;
using System.Collections;

public class TripVine : MonoBehaviour 
{

    bool done = false;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !done)
        {
            GetComponent<Animator>().SetBool("reset", false);
            done = true;
            //SetLastSpawn.PiecesToReset.Add(gameObject);
            GetComponent<Animator>().SetBool("playerStanding", true);
        }
    }

    void DestroyThisVine()
    {
        Destroy(gameObject);
    }
}
