using UnityEngine;
using System.Collections;

public class WallWalk : MonoBehaviour {

    public static bool onWall;
    float xRot =1 ;
    float zRot = 1;
    Vector3 startRot;
    bool done = false;

	// Use this for initialization
	void Start () 
    {
        
        if (transform.root.gameObject.name == "RightSideWall")
        {
            zRot = transform.root.localEulerAngles.z;
            xRot = transform.root.localEulerAngles.x;
        }
        else if (transform.root.gameObject.name == "LeftSideWall")
        {
            zRot = transform.root.localEulerAngles.z;
            xRot = transform.root.localEulerAngles.x;
        }
        Debug.Log("xRot : " + xRot + "   , zRot : " + zRot);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        
        //startRot = other.transform.localEulerAngles;
        if (other.tag == "Player" && !done)
        {
            done = true;
            PlayerController pc = other.GetComponent<PlayerController>();
            switch (pc.stateMachine.getState())
            {
                case "Muskalo":
                    {
                        startRot = other.transform.FindChild("Muskalo_Still").localEulerAngles;
                        Debug.Log(startRot);
                        other.transform.FindChild("Muskalo_Still").localEulerAngles = new Vector3(zRot, other.transform.FindChild("Muskalo_Still").localEulerAngles.y, xRot);
                        break;
                    }
                case "Fox":
                    {
                        other.transform.FindChild("FoxSpirit_Still").Rotate(-30, 0, 0);
                        break;
                    }
                case "Wolf":
                    {
                        other.transform.FindChild("WolfSpirit_Still").Rotate(-30, 0, 0);
                        break;
                    }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        done = false;
        if (other.tag == "Player")
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            switch (pc.stateMachine.getState())
            {
                case "Muskalo":
                    {
                        Debug.Log("exited : should reset rotation");
                        other.transform.FindChild("Muskalo_Still").localEulerAngles = startRot;
                        pc.speed = 3;
                        break;
                    }
                case "Fox":
                    {
                        other.transform.FindChild("FoxSpirit_Still").Rotate(30, 0, 0);
                        pc.speed = 3;
                        break;
                    }
                case "Wolf":
                    {
                        other.transform.FindChild("WolfSpirit_Still").Rotate(30, 0, 0);
                        pc.speed = 3;
                        break;
                    }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            switch (pc.stateMachine.getState())
            {
                case "Muskalo":
                    {
                        if (pc.speed > 0)
                            pc.speed -= Time.deltaTime;
                        else if (pc.speed < 0)
                            pc.speed = 0;
                        break;
                    }
                case "Fox":
                    {

                        break;
                    }
                case "Wolf":
                    {
                        if (pc.speed > 0)
                            pc.speed -= Time.deltaTime;
                        else if (pc.speed < 0)
                            pc.speed = 0;
                        break;
                    }
            }
        }
    }
}
