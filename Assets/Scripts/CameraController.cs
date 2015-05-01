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
		float totalTimeInv = 1;
		float trueEnd = 0;

		public Rotating()
		{
			
		}

		public override void OnEnter (CameraController owner)
		{
			owner.stateMachine.setState("Rotating");
			float s = Mathf.RoundToInt(owner.transform.rotation.eulerAngles.y);
			owner.target.GetComponent<PlayerController>().newRotation(-1);
			owner.activeRotation = owner.yRotations[owner.yRotations.Count-1];
			float e = owner.activeRotation;
			trueEnd = e;
			float[] vals = owner.getRotationAngles(s,e);
			s = vals[0];
			e = vals[1];
			count = 0;
			totalTimeInv = 180f/Mathf.Abs(s-e);
			start = Quaternion.Euler(owner.pitch,s,0);
			end = Quaternion.Euler(owner.pitch,e,0);
			sPos = new Vector3(Mathf.Cos(s*Mathf.Deg2Rad)*(owner.distFromPlayer.x) + Mathf.Sin(s*Mathf.Deg2Rad)*(owner.distFromPlayer.z),
			                   0,Mathf.Cos(s*Mathf.Deg2Rad)*(owner.distFromPlayer.z) - Mathf.Sin(s*Mathf.Deg2Rad)*(owner.distFromPlayer.x));
			ePos = new Vector3(Mathf.Cos(e*Mathf.Deg2Rad)*(owner.distFromPlayer.x) + Mathf.Sin(e*Mathf.Deg2Rad)*(owner.distFromPlayer.z),
			                   0,Mathf.Cos(e*Mathf.Deg2Rad)*(owner.distFromPlayer.z) - Mathf.Sin(e*Mathf.Deg2Rad)*(owner.distFromPlayer.x));
			//owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.eulerAngles.x,trueEnd,owner.transform.rotation.eulerAngles.z);
			//owner.stateMachine.ChangeState(owner.states[2]);
		}

		public override void Process (CameraController owner)
		{
			Quaternion current = Quaternion.Slerp(start,end,count);
			Vector3 cPos = Vector3.Slerp(sPos,ePos,count);
			count = Mathf.Clamp01(count+Time.deltaTime*totalTimeInv);
			if(count == 1)
			{
				Debug.Log("E");
				owner.stateMachine.ChangeState(owner.states[2]);
			}
			cPos.y = owner.distFromPlayer.y;
			float yRot = current.eulerAngles.y;
			Vector3 targRot = owner.target.transform.rotation.eulerAngles;
			owner.target.transform.rotation = Quaternion.Euler (targRot.x, yRot, targRot.z);
			owner.transform.rotation = current;
			owner.transform.position = cPos+owner.target.transform.position;
		}

		public override void OnExit (CameraController owner)
		{
			Debug.Log("E");
			if(count == 1)
			{
				Debug.Log("EXIT");
				owner.target.GetComponent<PlayerController>().newRotation(trueEnd);
				owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.eulerAngles.x,trueEnd,owner.transform.rotation.eulerAngles.z);
			}
		}
	}

	public class PathFollow : State<CameraController>
	{
		Quaternion start = Quaternion.identity;
		Quaternion end = Quaternion.identity;
		Vector3 sPos = Vector3.zero;
		Vector3 ePos = Vector3.zero;
		float count = 0;
		float otherCount = 0;
		float totalTimeInv = 1;
		float trueEnd = 0;
		Vector3 playerPos = Vector3.zero;
		
		public PathFollow()
		{
			
		}
		
		public override void OnEnter (CameraController owner)
		{
			owner.target.GetComponent<PlayerController>().useRotation = true;
			owner.stateMachine.setState("PathFollow");
			float s = owner.target.GetComponent<PlayerController>().getRotation();
			owner.activeRotation = owner.yRotations[owner.yRotations.Count-1];
			float e = owner.activeRotation;
			trueEnd = e;
			float[] vals = owner.getRotationAngles(s,e);
			s = vals[0];
			e = vals[1];
			count = 0;
			otherCount = 0;
			totalTimeInv = 180f/Mathf.Abs(s-e);
			start = Quaternion.Euler(owner.pitch,s,0);
			end = Quaternion.Euler(owner.pitch,e,0);
			sPos = owner.transform.position;
			playerPos = owner.target.transform.position;
			ePos = playerPos + new Vector3(Mathf.Cos(e*Mathf.Deg2Rad)*(owner.distFromPlayer.x) + Mathf.Sin(e*Mathf.Deg2Rad)*(owner.distFromPlayer.z),
			                   0,Mathf.Cos(e*Mathf.Deg2Rad)*(owner.distFromPlayer.z) - Mathf.Sin(e*Mathf.Deg2Rad)*(owner.distFromPlayer.x));

			//owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.eulerAngles.x,trueEnd,owner.transform.rotation.eulerAngles.z);
			//owner.stateMachine.ChangeState(owner.states[2]);
		}
		
		public override void Process (CameraController owner)
		{
			Vector3 dif = playerPos - owner.target.transform.position;
			float t = otherCount * totalTimeInv;
			t = Mathf.Sin(t * Mathf.PI * 0.5f);
			Quaternion current = Quaternion.Slerp(start,end,count);
			Vector3 cPos = Vector3.Slerp(sPos,ePos,count);
			count = Mathf.Clamp01(count+Time.deltaTime*totalTimeInv);
			if(count == 1)
			{
				owner.stateMachine.ChangeState(owner.states[2]);
			}
			float yRot = current.eulerAngles.y;
			if(yRot != yRot)
			{
				yRot = 0;
			}
			Vector3 targRot = owner.target.transform.rotation.eulerAngles;
			if(targRot.x != targRot.x)
			{
				targRot.x = 0;
			}
			if(targRot.z != targRot.z)
			{
				targRot.z = 0;
			}
			owner.target.transform.rotation = Quaternion.Euler (targRot.x, yRot, targRot.z);
			//cPos.y = owner.distFromPlayer.y;
			//owner.transform.rotation = current;
			//owner.transform.position = cPos;
		}
		
		public override void OnExit (CameraController owner)
		{
			if(count == 1)
			{
				owner.target.GetComponent<PlayerController>().newRotation(trueEnd);
				owner.target.GetComponent<PlayerController>().useRotation = false;
				//owner.transform.rotation = Quaternion.Euler(owner.transform.rotation.eulerAngles.x,trueEnd,owner.transform.rotation.eulerAngles.z);
			}
		}
	}

	public class Still : State<CameraController>
	{
		public Still()
		{

		}

		public override void OnEnter(CameraController owner)
		{
			owner.stateMachine.setState("Still");
		}
		public override void Process(CameraController owner)
		{
			owner.stateMachine.ChangeState(owner.states[2]);
		}
	}

	public class Swaying : State<CameraController>
	{
		float sway = 0;
	    
		Vector3 DesiredOffset = new Vector3(0.0f, 3.5f, -4.0f);
	    //Vector3 LookAtOffset = new Vector3(0.0f, 3.1f, 0.0f);
	 
	    private Vector3 desiredPosition = Vector3.zero;
	    private Vector3 cameraVelocity = Vector3.zero;

		public Swaying()
		{

		}

		public override void OnEnter (CameraController owner)
		{
			owner.stateMachine.setState("Swaying");
			float e = Mathf.RoundToInt(owner.transform.rotation.eulerAngles.y);
			float yRot = e;
			Vector3 targRot = owner.target.transform.rotation.eulerAngles;
			owner.target.transform.rotation = Quaternion.Euler (targRot.x, yRot, targRot.z);
			DesiredOffset = new Vector3(-owner.distFromPlayer.z,owner.distFromPlayer.y,owner.distFromPlayer.x);
			//LookAtOffset = new Vector3(0, owner.distFromPlayer.y-.4f, 0);*/
			print("dist = " + DesiredOffset.ToString());
		}

		public override void Process (CameraController owner)
		{
			Vector3 stretch = owner.SpringCamera.transform.position - desiredPosition;
			Vector3 force = -owner.stiffness * stretch - owner.damping * cameraVelocity;
	 
			Vector3 acceleration = force / owner.mass;
			
	        cameraVelocity += acceleration * Time.deltaTime;
	 
	        owner.SpringCamera.transform.position += cameraVelocity * Time.deltaTime;
			Vector3 s = owner.SpringCamera.transform.rotation.eulerAngles;
			owner.SpringCamera.transform.LookAt(owner.target.position);
			Vector3 e = owner.SpringCamera.transform.rotation.eulerAngles;
			DesiredOffset = new Vector3(-owner.distFromPlayer.z,owner.distFromPlayer.y,owner.distFromPlayer.x);
			float[] vals = owner.getRotationAngles(s.y,e.y);
			e = (e-s)/5+s;
			float valy = (vals[1]-vals[0])/10+vals[0];
			e.x = s.x;
			e.y = valy;
			owner.SpringCamera.transform.rotation = Quaternion.Euler(e);
			Vector3 rot = owner.target.rotation.eulerAngles;
	 
	        Matrix4x4 CamMat = new Matrix4x4();
	        CamMat.SetRow(0, new Vector4(-owner.target.forward.x, -owner.target.forward.y, -owner.target.forward.z));
	        CamMat.SetRow(1, new Vector4(owner.target.up.x, owner.target.up.y, owner.target.up.z));
	        Vector3 modRight = Vector3.Cross(CamMat.GetRow(1), CamMat.GetRow(0));
	        CamMat.SetRow(2, new Vector4(modRight.x, modRight.y, modRight.z));
	 
	        desiredPosition = owner.target.position + owner.TransformNormal(DesiredOffset, CamMat);
	 
	        //SpringCamera.projectionMatrix = Matrix4x4.Perspective(SpringCamera.fieldOfView, SpringCamera.aspect, SpringCamera.near, SpringCamera.far);
	 
		}
	}

	public State<CameraController>[] states = new State<CameraController>[] {	new PathFollow(),
																				new Still(),
																				new Swaying() };

	public StateMachine<CameraController> stateMachine = new StateMachine<CameraController>();

	public Transform target;
	private Camera SpringCamera;
	public float stiffness = 1000f;
	public float damping = 200f;
	public float mass = 50f;
	private List<int> yRotations = new List<int>();
	private List<Vector2> dimensions = new List<Vector2>();
	private int activeRotation = 0;
	private Vector3 distFromPlayer = Vector3.zero;
	private bool _rotating = false;
	public bool rotating
	{
		get { return _rotating; }
		set { _rotating = value; }
	}
	private float pitch = 0;

	// Use this for initialization
	void Start () {
		SpringCamera = Camera.main;
		pitch = SpringCamera.transform.rotation.eulerAngles.x;
		yRotations.Add(0);
		distFromPlayer = transform.position-target.position;
		print("dist = " + distFromPlayer.ToString());
		stateMachine.Configure(this, states[2]);
	}
	
	// Update is called once per frame
	void Update () {
		stateMachine.Update();
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
	
	public Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
    {
        Vector3 transformNormal = new Vector3();
        Vector3 axisX = new Vector3(matrix.m00, matrix.m01, matrix.m02);
        Vector3 axisY = new Vector3(matrix.m10, matrix.m11, matrix.m12);
        Vector3 axisZ = new Vector3(matrix.m20, matrix.m21, matrix.m22);
        transformNormal.x = Vector3.Dot(normal, axisX);
        transformNormal.y = Vector3.Dot(normal, axisY);
        transformNormal.z = Vector3.Dot(normal, axisZ);
 
        return transformNormal;
 
    }

	public void setPosition(Vector3 pos,Quaternion rot)
	{
		transform.position = pos;
		transform.rotation = rot;
		stateMachine.ChangeState(states[1]);
	}
}