using UnityEngine;
using System.Collections;

public class WallWalk : MonoBehaviour
{
    public static bool onWall;
    float xRot = 1;
    float zRot = 1;
    Vector3 startRot;

    PhysicMaterial wallWalk;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        onWall = false;
        player = GameObject.FindGameObjectWithTag("Player");
        wallWalk = Resources.Load<PhysicMaterial>("WallWalk");
    }

    void Update()
    {
        if (onWall)
        {
            Debug.Log("player is on the wall");
            switch (player.GetComponent<PlayerController>().stateMachine.getState())
            {
                case "Muskalo":
                    {
                        player.GetComponent<PlayerController>().OnWall = true;
                        if (player.GetComponent<InputController>().ySpeed - Time.deltaTime * 0.1f > 0)
                            player.GetComponent<InputController>().ySpeed -= Time.deltaTime * 0.1f;
                        else if (player.GetComponent<InputController>().ySpeed - Time.deltaTime * 0.1f < 0 && player.GetComponent<InputController>().ySpeed != 0)
                            player.GetComponent<InputController>().ySpeed = 0;
                        //startRot = player.transform.FindChild("Muskalo_Still").localEulerAngles;
                        //Transform other = player.transform.FindChild("Muskalo_Still");
                        //other.up = Vector3.Normalize(other.position - other.position);
                        //other.localEulerAngles = new Vector3(-zRot, other.localEulerAngles.y, other.localEulerAngles.z);
                        break;
                    }
                case "Fox":
                    {
                        player.GetComponent<PlayerController>().OnWall = false;
                        //startRot = player.transform.FindChild("foxSpirit_Still").localEulerAngles;
                        //Transform other = player.transform.FindChild("foxSpirit_Still");
                        //other.up = Vector3.Normalize(other.position - other.position);
                        //other.localEulerAngles = new Vector3(-zRot, other.localEulerAngles.y, other.localEulerAngles.z);
                        break;
                    }
                case "Wolf":
                    {
                        player.GetComponent<PlayerController>().OnWall = true;
                        if (player.GetComponent<InputController>().ySpeed - Time.deltaTime * 0.5f > 0)
                            player.GetComponent<InputController>().ySpeed -= Time.deltaTime * 0.5f;
                        else if (player.GetComponent<InputController>().ySpeed - Time.deltaTime * 0.5f < 0 && player.GetComponent<InputController>().ySpeed != 0)
                            player.GetComponent<InputController>().ySpeed = 0;
                        //startRot = player.transform.FindChild("WolfSpirit_Still").localEulerAngles;
                        //Transform other = player.transform.FindChild("WolfSpirit_Still");
                        //other.up = Vector3.Normalize(other.position - other.position);
                        //other.localEulerAngles = new Vector3(-zRot, other.localEulerAngles.y, other.localEulerAngles.z);
                        break;
                    }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //startRot = other.transform.localEulerAngles;
        if (other.gameObject.tag == "Player")
        {
            onWall = true;
            Debug.Log("Wall collision enter with player");
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            switch (pc.stateMachine.getState())
            {
                case "Muskalo":
                    {
                        wallWalk.staticFriction = 0;
                        wallWalk.dynamicFriction = 0;
                        wallWalk.frictionCombine = PhysicMaterialCombine.Minimum;
                        //startRot = other.gameObject.transform.FindChild("Muskalo_Still").localEulerAngles;
                        //Transform otherChild = other.gameObject.transform.FindChild("Muskalo_Still");
                        //otherChild.up = Vector3.Normalize(otherChild.position - other.transform.position);
                        //otherChild.localEulerAngles = new Vector3(-zRot, otherChild.localEulerAngles.y, otherChild.localEulerAngles.z);
                        break;
                    }
                case "Fox":
                    {
                        wallWalk.staticFriction = 1;
                        wallWalk.dynamicFriction = 1;
                        wallWalk.frictionCombine = PhysicMaterialCombine.Maximum;
                        //startRot = other.gameObject.transform.FindChild("foxSpirit_Still").localEulerAngles;
                        //Transform otherChild = other.gameObject.transform.FindChild("foxSpirit_Still");
                        //otherChild.up = Vector3.Normalize(otherChild.position - other.transform.position);
                        //otherChild.localEulerAngles = new Vector3(-zRot, otherChild.localEulerAngles.y, otherChild.localEulerAngles.z);
                        break;
                    }
                case "Wolf":
                    {
                        wallWalk.staticFriction = 0;
                        wallWalk.dynamicFriction = 0;
                        wallWalk.frictionCombine = PhysicMaterialCombine.Minimum;
                        //startRot = other.gameObject.transform.FindChild("WolfSpirit_Still").localEulerAngles;
                        //Transform otherChild = other.gameObject.transform.FindChild("WolfSpirit_Still");
                        //otherChild.up = Vector3.Normalize(otherChild.position - other.transform.position);
                        //otherChild.localEulerAngles = new Vector3(-zRot, otherChild.localEulerAngles.y, otherChild.localEulerAngles.z);
                        break;
                    }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onWall = false;
            player.GetComponent<PlayerController>().OnWall = false;
            Debug.Log("Wall collision exit with player");
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            switch (pc.stateMachine.getState())
            {
                case "Muskalo":
                    {
                        player.GetComponent<InputController>().ySpeed = 0;
                        pc.speed = 3;
                        break;
                    }
                case "Fox":
                    {
                        pc.speed = 3;
                        break;
                    }
                case "Wolf":
                    {
                        player.GetComponent<InputController>().ySpeed = 0;
                        pc.speed = 3;
                        break;
                    }
            }
        }
    }
}
