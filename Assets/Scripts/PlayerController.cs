using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	protected float gravitySpeed = -.1f;

	enum Direction
	{
		Left = -1,
		Right = 1,
		//change for different jump height
		Up = 1,
		Stopped = 0
	}

	public class Muskalo : State<PlayerController>{
		Direction moving = Direction.Stopped;
		bool keyRecieved = false;
		
		public Muskalo()
		{
			HandlerList.Add( new Handler<PlayerController>("Left",moveLeft) );
			HandlerList.Add( new Handler<PlayerController>("Stop",stopMoving) );
			HandlerList.Add( new Handler<PlayerController>("Right",moveRight) );
			HandlerList.Add ( new Handler<PlayerController>("Jump",jump) );
		}
		
		public override void OnEnter (PlayerController owner)
		{
			owner.STATE = "Muskalo";
		}
		
		public override void Process (PlayerController owner)
		{
			owner.transform.localPosition += new Vector3((float)moving,0,0);
			if(!keyRecieved)
			{
				moving = Direction.Stopped;
			}
			
			keyRecieved = false;
		}
		
		void moveLeft(PlayerController owner, params object[] args)
		{
			if(moving == Direction.Stopped)
			{
				keyRecieved = true;
				moving = Direction.Left;
			}
			else if(moving == Direction.Left)
			{
				keyRecieved = true;
			}
		}
		
		void moveRight(PlayerController owner, params object[] args)
		{
			if(moving == Direction.Stopped)
			{
				moving = Direction.Right;
				keyRecieved = true;
			}
			else if(moving == Direction.Right)
			{
				keyRecieved = true;
			}
		}
		
		void jump(PlayerController owner, params object[] args)
		{
			owner.rigidbody.AddForce(new Vector3(0,1500,0));
		}
		void stopMoving(PlayerController owner, params object[] args)
		{
			if(moving != Direction.Stopped)
			{
				moving = Direction.Stopped;
			}
		}
	}

	// Moon Spirit is wolf
	public class Wolf : State<PlayerController>{
		Direction moving = Direction.Stopped;
		bool keyRecieved = false;
		
		public Wolf()
		{
			HandlerList.Add( new Handler<PlayerController>("Left",moveLeft) );
			HandlerList.Add( new Handler<PlayerController>("Stop",stopMoving) );
			HandlerList.Add( new Handler<PlayerController>("Right",moveRight) );
			HandlerList.Add ( new Handler<PlayerController>("Jump",jump) );
		}
		
		public override void OnEnter (PlayerController owner)
		{
			owner.STATE = "Muskalo";
		}
		
		public override void Process (PlayerController owner)
		{
			owner.transform.localPosition += new Vector3((float)moving,0,0);
			if(!keyRecieved)
			{
				moving = Direction.Stopped;
			}
			
			keyRecieved = false;
		}
		
		void moveLeft(PlayerController owner, params object[] args)
		{
			if(moving == Direction.Stopped)
			{
				keyRecieved = true;
				moving = Direction.Left;
			}
			else if(moving == Direction.Left)
			{
				keyRecieved = true;
			}
		}
		
		void moveRight(PlayerController owner, params object[] args)
		{
			if(moving == Direction.Stopped)
			{
				moving = Direction.Right;
				keyRecieved = true;
			}
			else if(moving == Direction.Right)
			{
				keyRecieved = true;
			}
		}
		
		void jump(PlayerController owner, params object[] args)
		{
			owner.rigidbody.AddForce(new Vector3(0,1500,0));
		}
		void stopMoving(PlayerController owner, params object[] args)
		{
			if(moving != Direction.Stopped)
			{
				moving = Direction.Stopped;
			}
		}
	}

	private string STATE = "";
	public int maxFallDistance = 50;
	public State<PlayerController>[] states = new State<PlayerController>[] {	new Muskalo(),
																				new Wolf()};
	public StateMachine<PlayerController> stateMachine = new StateMachine<PlayerController>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
