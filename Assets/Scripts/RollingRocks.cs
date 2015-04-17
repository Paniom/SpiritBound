using UnityEngine;
using System.Collections;

public class RollingRocks : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GetComponent<Animator>().Play("Rolling_2", this.gameObject.layer, Random.Range(0f, 1.958f));
	}
	
}
