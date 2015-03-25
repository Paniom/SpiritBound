using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {
	
	public GameObject top;
	private RectTransform rect;
	
	// Use this for initialization
	void Start () {
		rect = top.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void position(Vector2 v){

		if(v.sqrMagnitude > 22500){
			Vector2 vn = v.normalized;
			rect.localPosition = new Vector3(vn.x*150f/85f,vn.y*150f/85f,0);
		}
		else{
			rect.localPosition = new Vector3(v.x/85,v.y/85,0);
		}
	}
}