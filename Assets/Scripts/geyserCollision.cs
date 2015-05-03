using UnityEngine;
using System.Collections;

public class geyserCollision : MonoBehaviour {

    public bool erupting;
    float eruptDelay;
    float eruptTime;

	// Use this for initialization
	void Start () 
    {
        eruptDelay = 2f;
        eruptTime = Random.Range(1f, 5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.GetComponent<Animator>().SetBool("erupting", erupting);
        if (erupting)
        {
            if(!collider.enabled)
                collider.enabled = true;
            if (!GetComponent<ParticleSystem>().isPlaying)
                GetComponent<ParticleSystem>().Play();
            if (eruptTime > 0)
            {
                eruptTime -= Time.deltaTime;
            }
            else
            {
                erupting = false;
                eruptTime = Random.Range(1f, 5f);
            }
        }
        else
        {
            if (GetComponent<ParticleSystem>().isPlaying)
                GetComponent<ParticleSystem>().Stop();
            if(collider.enabled)
                collider.enabled = false;
            if (eruptDelay > 0)
            {
                eruptDelay -= Time.deltaTime;
            }
            else
            {
                GetComponent<AudioSource>().Play();
                erupting = true;
                eruptDelay = 2f;
            }
        }
	}
}
