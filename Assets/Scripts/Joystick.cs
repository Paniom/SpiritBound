using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {
	
	public GameObject top;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void position(Vector2 v){
		if(v.sqrMagnitude > 40000){
			Vector2 vn = v.normalized;
			top.transform.localPosition = new Vector3(vn.x*200/85,-1,-vn.y*200/85);
		}
		else{
			top.transform.localPosition = new Vector3(v.x/85,-1,-v.y/85);
		}
	}
}