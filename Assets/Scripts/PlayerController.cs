using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject targetUI;

    public class Muskalo : State<PlayerController>
    {
        public Muskalo()
        {
            HandlerList.Add(new Handler<PlayerController>("Move", move));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
            HandlerList.Add(new Handler<PlayerController>("Interact", charge));
            HandlerList.Add(new Handler<PlayerController>("PickUp", pickup));
        }

        public override void OnEnter(PlayerController owner)
        {
            owner.fox.SetActive(false);
            owner.wolf.SetActive(false);
            owner.muskalo.SetActive(true);
            Physics.gravity = owner.startGravity*1.5f;
            owner.muskaloTrail.SetActive(true);
            owner.stateMachine.setState("Muskalo");
            Color m = owner.muskaloUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.muskaloUI.GetComponent<Image>().color = m;
            Debug.Log(owner.stateMachine.getState());
        }

        public override void Process(PlayerController owner)
        {
            if (owner.foxPowerLevelUI.GetComponent<Slider>().value < owner.foxSpiritStartPowerLevel) // check if fox power level needs to increase
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value += Time.deltaTime * 0.5f; //increase fox power level
            }
            else if (owner.foxPowerLevelUI.GetComponent<Slider>().value > owner.foxSpiritStartPowerLevel) //check if fox power level went over starting power level
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value = owner.foxSpiritStartPowerLevel; // reset value to starting power level
            }

            if (owner.wolfPowerLevelUI.GetComponent<Slider>().value < owner.wolfSpiritStartPowerLevel) // check if wolf power level needs to increase
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value += Time.deltaTime * 0.5f; //increase wolf power level
            }
            else if (owner.wolfPowerLevelUI.GetComponent<Slider>().value > owner.wolfSpiritStartPowerLevel)//check if wolf power level went over starting power level
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value = owner.wolfSpiritStartPowerLevel;// reset value to starting power level
            }
        }

        public override void OnExit(PlayerController owner)
        {
            owner.muskaloTrail.SetActive(false);
            Color m = owner.muskaloUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.muskaloUI.GetComponent<Image>().color = m;
        }

        void move(PlayerController owner, params object[] args)
        {
            if (owner.IsGrounded())
                owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed, owner.rigidbody.velocity.y, (float)args[1] * owner.speed);
            else
            {
                float airControl = 0.2f;
                float airSpeed = owner.speed;
                Vector3 airMove = new Vector3((float)args[0] * airSpeed, owner.rigidbody.velocity.y, (float)args[1] * airSpeed);
                owner.rigidbody.velocity = Vector3.Lerp(owner.rigidbody.velocity, airMove, Time.deltaTime * airControl);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = 5;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        //Lowers head and does a charge/bash foward
        void charge(PlayerController owner, params object[] args)
        {

        }

        void pickup(PlayerController owner, params object[] args)
        {
            TimeAndScore.score += 10;
        }
    }

    // Sun Spirit is fox
    public class Fox : State<PlayerController>
    {
        public Fox()
        {
            HandlerList.Add(new Handler<PlayerController>("Move", move));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
            HandlerList.Add(new Handler<PlayerController>("Interact", dash));
        }

        public override void OnEnter(PlayerController owner)
        {
            owner.fox.SetActive(true);
            owner.wolf.SetActive(false);
            owner.muskalo.SetActive(false);
            Physics.gravity = owner.startGravity*0.5f;
            owner.foxTrail.SetActive(true);
            owner.stateMachine.setState("Fox");
            Color m = owner.foxUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.foxUI.GetComponent<Image>().color = m;
            Debug.Log(owner.stateMachine.getState());
        }

        public override void Process(PlayerController owner)
        {
            if (owner.foxPowerLevelUI.GetComponent<Slider>().value > 0) // check if fox power level needs to decrease
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value -= Time.deltaTime; //decrease fox power level
            }
            else
            {
                owner.foxPowerLevelUI.GetComponent<Slider>().value = 0;
                owner.stateMachine.ChangeState(owner.states[0]); //switch state to muskalo
                owner.targetUI.GetComponent<UIController>().stateMachine.ChangeState(owner.targetUI.GetComponent<UIController>().states[0]);
                owner.targetUI.GetComponent<UIController>()._isLerping = false;
                owner.targetUI.GetComponent<UIController>().SpiritUIParent.transform.rotation = Quaternion.identity;
            }
        }

        public override void OnExit(PlayerController owner)
        {
            owner.foxTrail.SetActive(false);
            Color m = owner.foxUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.foxUI.GetComponent<Image>().color = m;
        }

        void move(PlayerController owner, params object[] args)
        {
            if (owner.IsGrounded())
                owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed, owner.rigidbody.velocity.y, (float)args[1] * owner.speed);
            else
            {
                float airControl = 0.2f;
                float airSpeed = owner.speed;
                Vector3 airMove = new Vector3((float)args[0] * airSpeed, owner.rigidbody.velocity.y, (float)args[1] * airSpeed);
                owner.rigidbody.velocity = Vector3.Lerp(owner.rigidbody.velocity, airMove, Time.deltaTime * airControl);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = 5;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        //Does a quick dash(speed boost) forward
        void dash(PlayerController owner, params object[] args)
        {
            owner.rigidbody.AddForce(owner.transform.forward*10);
        }

        void pickup(PlayerController owner, params object[] args)
        {
            owner.foxPowerLevelUI.GetComponent<Slider>().value += 5;
        }
    }

    // Moon Spirit is wolf
    public class Wolf : State<PlayerController>
    {
        public Wolf()
        {
            HandlerList.Add(new Handler<PlayerController>("Move", move));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
            HandlerList.Add(new Handler<PlayerController>("Interact", precision));
        }

        public override void OnEnter(PlayerController owner)
        {
            owner.fox.SetActive(false);
            owner.wolf.SetActive(true);
            owner.muskalo.SetActive(false);
            Physics.gravity = owner.startGravity * 4;
            owner.wolfTrail.SetActive(true);
            owner.stateMachine.setState("Wolf");
            Color m = owner.wolfUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.wolfUI.GetComponent<Image>().color = m;
            Debug.Log(owner.stateMachine.getState());
        }

        public override void Process(PlayerController owner)
        {
            if (owner.wolfPowerLevelUI.GetComponent<Slider>().value > 0) // check if wolf power level needs to decrease
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value -= Time.deltaTime; //decrease wolf power level
            }
            else
            {
                owner.wolfPowerLevelUI.GetComponent<Slider>().value = 0;
                owner.stateMachine.ChangeState(owner.states[0]); //switch state to muskalo
                owner.targetUI.GetComponent<UIController>().stateMachine.ChangeState(owner.targetUI.GetComponent<UIController>().states[0]);
                owner.targetUI.GetComponent<UIController>()._isLerping = false;
                owner.targetUI.GetComponent<UIController>().SpiritUIParent.transform.rotation = Quaternion.identity;
            }
        }

        public override void OnExit(PlayerController owner)
        {
            owner.wolfTrail.SetActive(false);
            Color m = owner.wolfUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.wolfUI.GetComponent<Image>().color = m;
        }

        void move(PlayerController owner, params object[] args)
        {
            if (owner.IsGrounded())
                owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed, owner.rigidbody.velocity.y, (float)args[1] * owner.speed);
            else
            {
                float airControl = 0.5f;
                float airSpeed = owner.speed;
                Vector3 airMove = new Vector3((float)args[0] * airSpeed, owner.rigidbody.velocity.y, (float)args[1] * airSpeed);
                owner.rigidbody.velocity = Vector3.Lerp(owner.rigidbody.velocity, airMove, Time.deltaTime * airControl);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = 20;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        //Slow down time, player still moves with the same speed
        void precision(PlayerController owner, params object[] args)
        {
            
        }

        void pickup(PlayerController owner, params object[] args)
        {
            owner.wolfPowerLevelUI.GetComponent<Slider>().value += 5;
        }
    }

    public class Standby : State<PlayerController>
    {
        public Standby()
        {
            HandlerList.Add(new Handler<PlayerController>("Move", move));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
            HandlerList.Add(new Handler<PlayerController>("PickUp", pickup));
        }

        public override void OnEnter(PlayerController owner)
        {
            Physics.gravity = owner.startGravity * 1.5f;
            Debug.Log(owner.stateMachine.getState());
        }

        public override void Process(PlayerController owner)
        {
            
        }

        public override void OnExit(PlayerController owner)
        {

        }

        void move(PlayerController owner, params object[] args)
        {
            if (owner.IsGrounded())
                owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed, owner.rigidbody.velocity.y, (float)args[1] * owner.speed);
            else
            {
                float airControl = 0.2f;
                float airSpeed = owner.speed;
                Vector3 airMove = new Vector3((float)args[0] * airSpeed, owner.rigidbody.velocity.y, (float)args[1] * airSpeed);
                owner.rigidbody.velocity = Vector3.Lerp(owner.rigidbody.velocity, airMove, Time.deltaTime * airControl);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = 5;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        void pickup(PlayerController owner, params object[] args)
        {

        }
    }

    public State<PlayerController>[] states = new State<PlayerController>[] {	new Muskalo(),
																				new Wolf(),
                                                                                new Fox(),
                                                                                new Standby()};
    public StateMachine<PlayerController> stateMachine = new StateMachine<PlayerController>();

    public GameObject muskaloUI;
    public GameObject wolfUI;
    public GameObject foxUI;
    public GameObject foxPowerLevelUI;
    public GameObject wolfPowerLevelUI;

    public GameObject fox;
    public GameObject wolf;
    public GameObject muskalo;

	public GameObject followingCamera;
	private float camDirection = 0;

    float foxSpiritStartPowerLevel = 20;
    float wolfSpiritStartPowerLevel = 20;

    public GameObject foxTrail;
    public GameObject wolfTrail;
    public GameObject muskaloTrail;

    bool grounded; // Determine if the player is on the ground
    float distToGround; // Distance from player collider to ground
    public float speed = 1; //How fast the player will move

    Vector3 startGravity = new Vector3(0,-9.8f,0); //Variable to tell what the starting gravity was

    // Use this for initialization
    void Start()
    {
        distToGround = collider.bounds.extents.y; // set distance from player collider to ground
        stateMachine.Configure(this, states[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(stateMachine.getState());
        stateMachine.Update();
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			camDirection = 90;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			camDirection = 180;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			camDirection = 270;
		}
		if(Input.GetKeyDown(KeyCode.Alpha0)) {
			camDirection = 0;
		}
    }


    /***************************
     ******** New Calls ********
     ***************************/

    void RotateRight()
    {
        if (targetUI.GetComponent<UIController>()._isLerping == false)
            targetUI.SendMessage("ChangeState", 2, SendMessageOptions.DontRequireReceiver);
    }

    void RotateLeft()
    {
        if (targetUI.GetComponent<UIController>()._isLerping == false)
            targetUI.SendMessage("ChangeState", 1, SendMessageOptions.DontRequireReceiver);
    }

    void ChangeRight()
    {
		gameObject.SendMessage("rotateFinished",SendMessageOptions.DontRequireReceiver);
        if (stateMachine.getState() == "Wolf")
        {
            stateMachine.ChangeState(states[2]);
        }
        else if (stateMachine.getState() == "Fox")
        {
            stateMachine.ChangeState(states[0]);
        }
        else if (stateMachine.getState() == "Muskalo")
        {
            stateMachine.ChangeState(states[1]);
        }
    }

    void ChangeLeft()
    {
		gameObject.SendMessage("rotateFinished",SendMessageOptions.DontRequireReceiver);
        if (stateMachine.getState() == "Wolf")
        {
            stateMachine.ChangeState(states[0]);
        }
        else if (stateMachine.getState() == "Fox")
        {
            stateMachine.ChangeState(states[1]);
        }
        else if (stateMachine.getState() == "Muskalo")
        {
            stateMachine.ChangeState(states[2]);
        }
    }

    void Move(Vector2 direction)
    {
		camDirection = followingCamera.transform.localRotation.eulerAngles.y;
		stateMachine.messageReciever("Move",new object[] {Mathf.Cos(camDirection*Mathf.Deg2Rad)*direction.x + 
			Mathf.Sin(camDirection*Mathf.Deg2Rad)*direction.y,Mathf.Cos(camDirection*Mathf.Deg2Rad)*direction.y - 
			Mathf.Sin(camDirection*Mathf.Deg2Rad)*direction.x});// straight
    }

    void Interact()
    {
        stateMachine.messageReciever("Interact", null);
    }

    void PickUp()
    {
        stateMachine.messageReciever("PickUp", null);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f); // Raycast to determine if player is on the ground and they can jump
    }

    /* If the player is grounded they will jump*/
    void Jump()
    {
        if (IsGrounded())
        {
            stateMachine.messageReciever("Jump",null);
        }
    }

    void AirControl()
    {
        float airControl = 0.2f;
        float airSpeed = speed;
        //Vector3 airMove = new Vector3((float)args[0] * airSpeed, owner.rigidbody.velocity.y, (float)args[1] * airSpeed);
    }

	public void interactFinished()
	{
		gameObject.SendMessage("interactFinished",SendMessageOptions.DontRequireReceiver);
	}

    void SwitchToStandby()
    {
        stateMachine.ChangeState(states[3]);
    }
}
