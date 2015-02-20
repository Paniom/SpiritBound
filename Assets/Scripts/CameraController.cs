using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public class Rotating : State<CameraController>
	{
		Quaternion start = Quaternion.identity;
		Quaternion end = Quaternion.identity;
		Vector3 sPos = Vector3.zero;
		Vector3 ePos = Vector3.zero;
		float count = 0;
		float totalTime = 1;
		float trueEnd = 0;

		public Rotating()
		{
			
		}

		public override void OnEnter (CameraController owner)
		{
			float s = Mathf.RoundToInt(owner.transform.rotation.eulerAngles.y);
			owner.activeRotation = owner.yRotations[owner.yRotations.Count-1];
			float e = owner.activeRotation;
			trueEnd = e;
			float[] vals = owner.getRotationAngles(s,e);
			s = vals[0];
			e = vals[1];
			count = 0;
			totalTime = Mathf.Abs(s-e)/90f;
			start = Quaternion.Euler(29.45f,s,0);
			end = Quaternion.Euler(29.45f,e,0);
			sPos = new Vector3(Mathf.Cos(s*Mathf.Deg2Rad)*owner.distFromPlayer.x + Mathf.Sin(s*Mathf.Deg2Rad)*owner.distFromPlayer.z,
			                   0,Mathf.Cos(s*Mathf.Deg2Rad)*owner.distFromPlayer.z - Mathf.Sin(s*Mathf.Deg2Rad)*owner.distFromPlayer.x);
			ePos = new Vector3(Mathf.Cos(e*Mathf.Deg2Rad)*owner.distFromPlayer.x + Mathf.Sin(e*Mathf.Deg2Rad)*owner.distFromPlayer.z,
			                   0,Mathf.Cos(e*Mathf.Deg2Rad)*owner.distFromPlayer.z - Mathf.Sin(e*Mathf.Deg2Rad)*owner.distFromPlayer.x);
		}

		public override void Process (CameraController owner)
		{
			Quaternion current = Quaternion.Slerp(start,end,count);
			Vector3 cPos = Vector3.Slerp(sPos,ePos,count);
			count = Mathf.Clamp01(count+Time.deltaTime/totalTime);
			if(count == 1)
			{
				owner.stateMachine.ChangeState(owner.states[1]);
			}
			cPos.y = owner.distFromPlayer.y;
			owner.transform.localRotation = current;
			owner.transform.localPosition = cPos;
		}

		public override void OnExit (CameraController owner)
		{
			if(count == 1)
			{
				owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.eulerAngles.x,trueEnd,owner.transform.rotation.eulerAngles.z);
			}
		}
	}

	public class Still : State<CameraController>
	{
		public Still()
		{

		}

	}

	public State<CameraController>[] states = new State<CameraController>[] {	new Rotating(),
																				new Still() };
	public StateMachine<CameraController> stateMachine = new StateMachine<CameraController>();


	private List<int> yRotations = new List<int>();
	private int activeRotation = 0;
	private Vector3 distFromPlayer = Vector3.zero;
	private bool _rotating = false;
	public bool rotating
	{
		get { return _rotating; }
		set { _rotating = value; }
	}

	// Use this for initialization
	void Start () {
		yRotations.Add(0);
		distFromPlayer = transform.localPosition;
		stateMachine.Configure(this, states[1]);
	}
	
	// Update is called once per frame
	void Update () {
		stateMachine.Update();
	}

	void RemoveRotation (int y) {
		yRotations.Remove(y);
		if(activeRotation == y)
		{
			if(rotating) {
				stateMachine.ChangeState(states[1]);
			}
			stateMachine.ChangeState(states[0]);
		}
	}

	void AddRotation (int y) {
		yRotations.Add(y);
		if(rotating) {
			stateMachine.ChangeState(states[1]);
		}
		stateMachine.ChangeState(states[0]);
	}

	public float[] getRotationAngles(float start, float end) {
		if(Mathf.Abs(start-end) <= 180) {
			return new float[] {start,end};
		}
		if(start < 180) {
			return new float[] {start,end-360};
		}
		return new float[] {start,end+360};
	}
}




















