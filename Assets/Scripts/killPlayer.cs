﻿using UnityEngine;
using System.Collections;

public class killPlayer : MonoBehaviour {

    public static GameObject lastSpawn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = lastSpawn.transform.position;
        }
    }
}
