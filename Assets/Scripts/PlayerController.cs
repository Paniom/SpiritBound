using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public class Muskalo : State<PlayerController>{
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
			owner.stateMachine.setState("Muskalo");
		}
		
		public override void Process (PlayerController owner)
		{

		}
		
		void moveLeft(PlayerController owner, params object[] args)
		{

		}
		
		void moveRight(PlayerController owner, params object[] args)
		{

		}
		
		void jump(PlayerController owner, params object[] args)
		{
			
		}
		void stopMoving(PlayerController owner, params object[] args)
		{

		}
	}

	// Moon Spirit is wolf
	public class Wolf : State<PlayerController>{

		public Wolf()
		{
			HandlerList.Add( new Handler<PlayerController>("Left",moveLeft) );
			HandlerList.Add( new Handler<PlayerController>("Stop",stopMoving) );
			HandlerList.Add( new Handler<PlayerController>("Right",moveRight) );
			HandlerList.Add ( new Handler<PlayerController>("Jump",jump) );
		}
		
		public override void OnEnter (PlayerController owner)
		{
			owner.stateMachine.setState("Wolf");
		}
		
		public override void Process (PlayerController owner)
		{

		}
		
		void moveLeft(PlayerController owner, params object[] args)
		{

		}
		
		void moveRight(PlayerController owner, params object[] args)
		{

		}
		
		void jump(PlayerController owner, params object[] args)
		{

		}
		void stopMoving(PlayerController owner, params object[] args)
		{

		}
	}

	public State<PlayerController>[] states = new State<PlayerController>[] {	new Muskalo(),
																				new Wolf()};
	public StateMachine<PlayerController> stateMachine = new StateMachine<PlayerController>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Left(){
		stateMachine.messageReciever("Left",null);
	}
	
	void Right(){
		stateMachine.messageReciever("Right",null);
	}
}
