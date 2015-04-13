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
                        transform.root.collider.material.staticFriction = 0;
                        transform.root.collider.material.dynamicFriction = 0;
                        transform.root.collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
                        startRot = other.transform.FindChild("Muskalo_Still").localEulerAngles;
                        other.transform.FindChild("Muskalo_Still").localEulerAngles = new Vector3(-zRot, other.transform.FindChild("Muskalo_Still").localEulerAngles.y, xRot);
                        break;
                    }
                case "Fox":
                    {
                        transform.root.collider.material.staticFriction = 1;
                        transform.root.collider.material.dynamicFriction = 0;
                        transform.root.collider.material.frictionCombine = PhysicMaterialCombine.Maximum;
                        startRot = other.transform.FindChild("foxSpirit_Still").localEulerAngles;
                        other.transform.FindChild("foxSpirit_Still").localEulerAngles = new Vector3(-zRot, other.transform.FindChild("foxSpirit_Still").localEulerAngles.y, xRot);
                        break;
                    }
                case "Wolf":
                    {
                        transform.root.collider.material.staticFriction = 0;
                        transform.root.collider.material.dynamicFriction = 0;
                        transform.root.collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
                        startRot = other.transform.FindChild("WolfSpirit_Still").localEulerAngles;
                        other.transform.FindChild("WolfSpirit_Still").localEulerAngles = new Vector3(-zRot, other.transform.FindChild("WolfSpirit_Still").localEulerAngles.y, xRot);
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
                        other.transform.FindChild("Muskalo_Still").localEulerAngles = startRot;
                        pc.speed = 3;
                        break;
                    }
                case "Fox":
                    {
                        other.transform.FindChild("foxSpirit_Still").localEulerAngles = startRot;
                        pc.speed = 3;
                        break;
                    }
                case "Wolf":
                    {
                        other.transform.FindChild("WolfSpirit_Still").localEulerAngles = startRot;
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
