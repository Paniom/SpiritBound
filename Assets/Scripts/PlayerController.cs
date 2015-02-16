using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public class Muskalo : State<PlayerController>{

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
            Color m = owner.muskaloUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.muskaloUI.GetComponent<Image>().color = m;
		}
		
		public override void Process (PlayerController owner)
		{
            if (owner.foxPowerLevelUI.GetComponent<Slider>().value < owner.foxSpiritStartPowerLevel) // check if fox power level needs to decrease
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value += Time.deltaTime * 0.5f; //increase fox power level
                owner.foxSpiritPowerLevel = owner.foxPowerLevelUI.GetComponent<Slider>().value;
            }
            else if (owner.foxPowerLevelUI.GetComponent<Slider>().value > owner.foxSpiritStartPowerLevel)
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value = owner.foxSpiritStartPowerLevel;
                owner.foxSpiritPowerLevel = owner.foxPowerLevelUI.GetComponent<Slider>().value;
            }

            if (owner.wolfPowerLevelUI.GetComponent<Slider>().value < owner.wolfSpiritStartPowerLevel) // check if wolf power level needs to decrease
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value += Time.deltaTime * 0.5f; //increase wolf power level
                owner.wolfSpiritPowerLevel = owner.wolfPowerLevelUI.GetComponent<Slider>().value;
            }
            else if (owner.wolfPowerLevelUI.GetComponent<Slider>().value > owner.wolfSpiritStartPowerLevel)
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value = owner.wolfSpiritStartPowerLevel;
                owner.wolfSpiritPowerLevel = owner.wolfPowerLevelUI.GetComponent<Slider>().value;
            }
		}

        public override void OnExit(PlayerController owner)
        {
            Color m = owner.muskaloUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.muskaloUI.GetComponent<Image>().color = m;
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

    // Sun Spirit is fox
    public class Fox : State<PlayerController>
    {

        public Fox()
        {
            HandlerList.Add(new Handler<PlayerController>("Left", moveLeft));
            HandlerList.Add(new Handler<PlayerController>("Stop", stopMoving));
            HandlerList.Add(new Handler<PlayerController>("Right", moveRight));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
        }

        public override void OnEnter(PlayerController owner)
        {
            owner.stateMachine.setState("Fox");
            Color m = owner.foxUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.foxUI.GetComponent<Image>().color = m;
        }

        public override void Process(PlayerController owner)
        {
            if (owner.foxPowerLevelUI.GetComponent<Slider>().value > 0) // check if fox power level needs to decrease
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value -= Time.deltaTime; //decrease fox power level
                owner.foxSpiritPowerLevel = owner.foxPowerLevelUI.GetComponent<Slider>().value;
            }
            else
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value = 0;
                owner.foxSpiritPowerLevel = owner.foxPowerLevelUI.GetComponent<Slider>().value;
                owner.stateMachine.ChangeState(owner.states[0]); //switch state to muskalo
            }
        }

        public override void OnExit(PlayerController owner)
        {
            Color m = owner.foxUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.foxUI.GetComponent<Image>().color = m;
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
            Color m = owner.wolfUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.wolfUI.GetComponent<Image>().color = m;
		}
		
		public override void Process (PlayerController owner)
		{
            if (owner.wolfPowerLevelUI.GetComponent<Slider>().value > 0) // check if wolf power level needs to decrease
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value -= Time.deltaTime; //decrease wolf power level
                owner.wolfSpiritPowerLevel = owner.wolfPowerLevelUI.GetComponent<Slider>().value;
            }
            else
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value = 0;
                owner.wolfSpiritPowerLevel = owner.wolfPowerLevelUI.GetComponent<Slider>().value;
                owner.stateMachine.ChangeState(owner.states[0]); //switch state to muskalo
            }
		}

        public override void OnExit(PlayerController owner)
        {
            Color m = owner.wolfUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.wolfUI.GetComponent<Image>().color = m;
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
																				new Wolf(),
                                                                                new Fox()};
	public StateMachine<PlayerController> stateMachine = new StateMachine<PlayerController>();

    public GameObject muskaloUI;
    public GameObject wolfUI;
    public GameObject foxUI;
    public GameObject foxPowerLevelUI;
    public GameObject wolfPowerLevelUI;
    public float wolfSpiritPowerLevel = 20;
    public float foxSpiritPowerLevel = 20;

    float foxSpiritStartPowerLevel;
    float wolfSpiritStartPowerLevel;

	// Use this for initialization
	void Start () {
        foxSpiritStartPowerLevel = foxSpiritPowerLevel;
        wolfSpiritStartPowerLevel = wolfSpiritPowerLevel;
        stateMachine.Configure(this, states[0]);
	}
	
	// Update is called once per frame
	void Update () {
        stateMachine.Update();
        if (Input.GetKeyUp(KeyCode.Q))
            Left();
        else if (Input.GetKeyUp(KeyCode.E))
            Right();
	}

	// unused method
	void Left(){
		stateMachine.messageReciever("Left",null);
        if(stateMachine.getState() == "Wolf")
            stateMachine.ChangeState(states[0]);
        else if (stateMachine.getState() == "Fox")
            stateMachine.ChangeState(states[1]);
        else if (stateMachine.getState() == "Muskalo")
            stateMachine.ChangeState(states[2]);
	}

	// unused method
	void Right(){
		stateMachine.messageReciever("Right",null);
        if (stateMachine.getState() == "Wolf")
            stateMachine.ChangeState(states[2]);
        else if (stateMachine.getState() == "Fox")
            stateMachine.ChangeState(states[0]);
        else if (stateMachine.getState() == "Muskalo")
            stateMachine.ChangeState(states[1]);
	}


   /***************************
	******** New Calls ********
	***************************/
	void RotateLeft(){
		if(stateMachine.getState() == "Wolf")
			stateMachine.ChangeState(states[0]);
		else if (stateMachine.getState() == "Fox")
			stateMachine.ChangeState(states[1]);
		else if (stateMachine.getState() == "Muskalo")
			stateMachine.ChangeState(states[2]);
	}

	void RotateRight(){
		if (stateMachine.getState() == "Wolf")
			stateMachine.ChangeState(states[2]);
		else if (stateMachine.getState() == "Fox")
			stateMachine.ChangeState(states[0]);
		else if (stateMachine.getState() == "Muskalo")
			stateMachine.ChangeState(states[1]);
	}

	void Move(Vector2 direction){

	}

	void Interact(){

	}

	void Jump(){

	}
}
