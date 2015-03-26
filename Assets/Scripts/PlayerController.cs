using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject targetUI;

    public class Muskalo : State<PlayerController>
    {
        GameObject chargeEffect;
        float startingInteractTimer = 1.0f;
        float interactTimer = 1.0f;
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
            Physics.gravity = new Vector3(0,-owner.muskaloGravity,0);
            owner.muskaloTrail.SetActive(true);
            owner.stateMachine.setState("Muskalo");
            Color m = owner.muskaloUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.muskaloUI.GetComponent<Image>().color = m;
            owner.foxUI.GetComponent<RectTransform>().localScale = new Vector3(0.8f,0.8f,0.8f);
            owner.wolfUI.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            owner.muskaloUI.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
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

            if (owner.interacting)
            {
                if (interactTimer > 0)
                {
                    owner.rigidbody.velocity = new Vector3(0, owner.rigidbody.velocity.y, owner.speed * 5);
                    interactTimer -= Time.deltaTime;
                }
                else
                {
                    Destroy(chargeEffect);
                    owner.interacting = false;
                    interactTimer = startingInteractTimer;
                    owner.InteractComplete();
                }
            }
        }

        public override void OnExit(PlayerController owner)
        {
            owner.muskaloTrail.SetActive(false);
            Color m = owner.muskaloUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.muskaloUI.GetComponent<Image>().color = m;
            if (owner.interacting)
            {
                Destroy(chargeEffect);
                owner.interacting = false;
                interactTimer = startingInteractTimer;
                owner.InteractComplete();
            }
        }

        void move(PlayerController owner, params object[] args)
        {
            if (!owner.interacting)
            {
                if (owner.duskaloAnimator)
                {
                    Vector2 movement = new Vector2((float)args[0], (float)args[1]);
                    owner.duskaloAnimator.SetFloat("Speed", movement.magnitude);
                    owner.duskaloAnimator.SetBool("Grounded", owner.IsGrounded());
                }
                if (owner.IsGrounded())
                    owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed, owner.rigidbody.velocity.y, (float)args[1] * owner.speed);
                else
                {
                    owner.AirControlMovement(args);
                }
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = owner.muskaloJumpPower;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        //Lowers head and does a charge/bash foward
        void charge(PlayerController owner, params object[] args)
        {
            chargeEffect = Instantiate(Resources.Load("ChargeEffect"),owner.transform.position,owner.transform.rotation) as GameObject;
            chargeEffect.transform.parent = owner.transform;
            chargeEffect.transform.localPosition = new Vector3(-0.2f,0,1.5f);
            owner.interacting = true;
        }

        void pickup(PlayerController owner, params object[] args)
        {
            if ((string)args[0] == PickUpController.PowerUpType.Score.ToString())
            {
                Debug.Log("duskalo score pickup");
                TimeAndScore.score += 10;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.Coin.ToString())
            {
                TimeAndScore.coins += 1;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.Gem.ToString())
            {
                TimeAndScore.gems += 1;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.WolfPower.ToString())
            {
                if (owner.wolfPowerLevelUI.GetComponent<Slider>().value + 5 > 20)
                    owner.wolfPowerLevelUI.GetComponent<Slider>().value = 20;
                else
                    owner.wolfPowerLevelUI.GetComponent<Slider>().value += 5;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.FoxPower.ToString())
            {
                if (owner.foxPowerLevelUI.GetComponent<Slider>().value + 5 > 20)
                    owner.foxPowerLevelUI.GetComponent<Slider>().value = 20;
                else
                    owner.foxPowerLevelUI.GetComponent<Slider>().value += 5;
            }
        }
    }

    // Sun Spirit is fox
    public class Fox : State<PlayerController>
    {
        GameObject dashEffect;
        float startingInteractTimer = 1.0f;
        float interactTimer = 1.0f;
        public Fox()
        {
            HandlerList.Add(new Handler<PlayerController>("Move", move));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
            HandlerList.Add(new Handler<PlayerController>("Interact", dash));
            HandlerList.Add(new Handler<PlayerController>("PickUp", pickup));
        }

        public override void OnEnter(PlayerController owner)
        {
            owner.fox.SetActive(true);
            owner.wolf.SetActive(false);
            owner.muskalo.SetActive(false);
            Physics.gravity = new Vector3(0, -owner.foxGravity, 0);
            owner.foxTrail.SetActive(true);
            owner.stateMachine.setState("Fox");
            Color m = owner.foxUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.foxUI.GetComponent<Image>().color = m;
            owner.muskaloUI.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            owner.wolfUI.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            owner.foxUI.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
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
                owner.RotateRight();
            }
            if (owner.interacting)
            {
                if (interactTimer > 0)
                {
                    interactTimer -= Time.deltaTime;
                }
                else
                {
                    Destroy(dashEffect);
                    owner.interacting = false;
                    interactTimer = startingInteractTimer;
                    owner.InteractComplete();
                }
            }
        }

        public override void OnExit(PlayerController owner)
        {
            owner.foxTrail.SetActive(false);
            Color m = owner.foxUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.foxUI.GetComponent<Image>().color = m;
            if (owner.interacting)
            {
                Destroy(dashEffect);
                owner.interacting = false;
                interactTimer = startingInteractTimer;
                owner.InteractComplete();
            }
        }

        void move(PlayerController owner, params object[] args)
        {
            if (owner.foxAnimator)
            {
                Vector2 movement = new Vector2((float)args[0], (float)args[1]);
                owner.foxAnimator.SetFloat("Speed", movement.magnitude);
                owner.foxAnimator.SetBool("Grounded", owner.IsGrounded());
            }
            if (owner.IsGrounded())
                owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed, owner.rigidbody.velocity.y, (float)args[1] * owner.speed);
            else
            {
                owner.AirControlMovement(args);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = owner.foxJumpPower;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        //Does a quick dash(speed boost) forward
        void dash(PlayerController owner, params object[] args)
        {
            if (owner.foxPowerLevelUI.GetComponent<Slider>().value >= 5)
            {
                dashEffect = Instantiate(Resources.Load("FireDash"), owner.transform.position, owner.transform.rotation) as GameObject;
                dashEffect.transform.parent = owner.transform;
                dashEffect.transform.forward *= -1;
                dashEffect.transform.localPosition = new Vector3(0.0f, 1.0f, 2.0f);
                owner.foxPowerLevelUI.GetComponent<Slider>().value -= 2;
                owner.interacting = true;
                if (owner.IsGrounded())
                    owner.transform.position += new Vector3(0, 0.5f, 0);
                owner.rigidbody.AddForce(owner.fox.transform.forward * 15 + owner.fox.transform.up, ForceMode.Impulse);
                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/Dash"), owner.transform.position);
            }
            else
            {
                owner.InteractComplete();
            }
        }

        void pickup(PlayerController owner, params object[] args)
        {
            if ((string)args[0] == PickUpController.PowerUpType.Score.ToString())
            {
                Debug.Log("fox score pickup");
                TimeAndScore.score += 10;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.Coin.ToString())
            {
                TimeAndScore.coins += 1;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.Gem.ToString())
            {
                TimeAndScore.gems += 1;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.WolfPower.ToString())
            {
                if (owner.wolfPowerLevelUI.GetComponent<Slider>().value + 5 > 20)
                    owner.wolfPowerLevelUI.GetComponent<Slider>().value = 20;
                else
                    owner.wolfPowerLevelUI.GetComponent<Slider>().value += 5;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.FoxPower.ToString())
            {
                if (owner.foxPowerLevelUI.GetComponent<Slider>().value + 5 > 20)
                    owner.foxPowerLevelUI.GetComponent<Slider>().value = 20;
                else
                    owner.foxPowerLevelUI.GetComponent<Slider>().value += 5;
            }
        }
    }

    // Moon Spirit is wolf
    public class Wolf : State<PlayerController>
    {
        float startingInteractTimer = 2.25f;
        float interactTimer = 2.25f;
        public Wolf()
        {
            HandlerList.Add(new Handler<PlayerController>("Move", move));
            HandlerList.Add(new Handler<PlayerController>("Jump", jump));
            HandlerList.Add(new Handler<PlayerController>("Interact", precision));
            HandlerList.Add(new Handler<PlayerController>("PickUp", pickup));
        }

        public override void OnEnter(PlayerController owner)
        {
            owner.fox.SetActive(false);
            owner.wolf.SetActive(true);
            owner.muskalo.SetActive(false);
            Physics.gravity = new Vector3(0, -owner.wolfGravity, 0);
            owner.wolfTrail.SetActive(true);
            owner.stateMachine.setState("Wolf");
            Color m = owner.wolfUI.GetComponent<Image>().color;
            m.a = 1.0f;
            owner.wolfUI.GetComponent<Image>().color = m;
            owner.muskaloUI.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            owner.foxUI.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            owner.wolfUI.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
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
                owner.RotateLeft();
            }

            if (owner.interacting)
            {
                if (interactTimer > 0)
                {
                    interactTimer -= Time.deltaTime;
                }
                else
                {
                    owner.interacting = false;
                    interactTimer = startingInteractTimer;
                    Time.timeScale = 1f;
                    owner.InteractComplete();
                }
            }
        }

        public override void OnExit(PlayerController owner)
        {
            Time.timeScale = 1f;
            owner.wolfTrail.SetActive(false);
            Color m = owner.wolfUI.GetComponent<Image>().color;
            m.a = 0.15f;
            owner.wolfUI.GetComponent<Image>().color = m;
            if (owner.interacting)
            {
                owner.interacting = false;
                interactTimer = startingInteractTimer;
                Time.timeScale = 1f;
                owner.InteractComplete();
            }
        }

        void move(PlayerController owner, params object[] args)
        {
            if (owner.wolfAnimator)
            {
                Vector2 movement = new Vector2((float)args[0], (float)args[1]);
                owner.wolfAnimator.SetFloat("Speed", movement.magnitude);
                owner.wolfAnimator.SetBool("Grounded", owner.IsGrounded());
            }
            if (owner.IsGrounded())
                owner.rigidbody.velocity = new Vector3((float)args[0] * owner.speed * (1 / Time.timeScale), owner.rigidbody.velocity.y, (float)args[1] * owner.speed * (1 / Time.timeScale));
            else
            {
                owner.AirControlMovement(args);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = owner.wolfJumpPower;
            owner.GetComponent<Rigidbody>().velocity = vel;
        }

        //Slow down time, player still moves with the same speed
        void precision(PlayerController owner, params object[] args)
        {
            if (owner.wolfPowerLevelUI.GetComponent<Slider>().value >= 5)
            {
                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SlowMotion"), owner.transform.position);
                owner.wolfPowerLevelUI.GetComponent<Slider>().value -= 2;
                owner.interacting = true;
                Time.timeScale = 0.5f;
            }
            else
            {
                owner.InteractComplete();
            }
            
        }

        void pickup(PlayerController owner, params object[] args)
        {
            if ((string)args[0] == PickUpController.PowerUpType.Score.ToString())
            {
                Debug.Log("wolf score pickup");
                TimeAndScore.score += 10;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.Coin.ToString())
            {
                TimeAndScore.coins += 1;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.Gem.ToString())
            {
                TimeAndScore.gems += 1;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.WolfPower.ToString())
            {
                if (owner.wolfPowerLevelUI.GetComponent<Slider>().value + 5 > 20)
                    owner.wolfPowerLevelUI.GetComponent<Slider>().value = 20;
                else
                    owner.wolfPowerLevelUI.GetComponent<Slider>().value += 5;
            }
            else if ((string)args[0] == PickUpController.PowerUpType.FoxPower.ToString())
            {
                if (owner.foxPowerLevelUI.GetComponent<Slider>().value + 5 > 20)
                    owner.foxPowerLevelUI.GetComponent<Slider>().value = 20;
                else
                    owner.foxPowerLevelUI.GetComponent<Slider>().value += 5;
            }
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
            Physics.gravity = new Vector3(0, -owner.muskaloGravity, 0);
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
                owner.AirControlMovement(args);
            }
        }

        void jump(PlayerController owner, params object[] args)
        {
            Vector3 vel = owner.GetComponent<Rigidbody>().velocity;
            vel.y = owner.muskaloJumpPower;
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
	//private float camDirection = 0;
	private float setRotation = 0;
	public void newRotation(float r) {
		setRotation = r;
	}
	public float getRotation() {
		return setRotation;
	}

    float foxSpiritStartPowerLevel = 20;
    float wolfSpiritStartPowerLevel = 20;

    public GameObject foxTrail;
    public GameObject wolfTrail;
    public GameObject muskaloTrail;

    public bool grounded; // Determine if the player is on the ground
    float distToGround; // Distance from player collider to ground

    public bool interacting = false;

    [Tooltip("How fast the player will move")]
    public float speed = 1; //How fast the player will move

    [Tooltip("The force of gravity on the muskalo")]
    public float muskaloGravity = 15; //force of gravity on the muskalo

    [Tooltip("The force of gravity on the fox")]
    public float foxGravity = 10; //force of gravity on the fox

    [Tooltip("The force of gravity on the wolf")]
    public float wolfGravity = 40; //force of gravity on the wolf

    [Tooltip("The jump power of the muskalo")]
    public float muskaloJumpPower = 5; //jump power of the muskalo

    [Tooltip("The jump power of the fox")]
    public float foxJumpPower = 5; //jump power of the fox

    [Tooltip("The jump power of the wolf")]
    public float wolfJumpPower = 20; //jump power of the wolf

    public Animator duskaloAnimator;
    public Animator foxAnimator;
    public Animator wolfAnimator;

    //Vector3 startGravity = new Vector3(0,-10f,0); //Variable to tell what the starting gravity was

    // Use this for initialization
    void Start()
    {
        distToGround = collider.bounds.extents.y; // set distance from player collider to ground
        stateMachine.Configure(this, states[0]);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
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
		if(direction.y > 0)
		{
			float y = direction.y;
			direction = direction*1f/2f;
			direction.y = y;
		}
		else
		{
			direction = direction*1f/4f;
		}
		float rotation = setRotation;
		if(setRotation == -1) {
			rotation = transform.rotation.eulerAngles.y;
		}
		stateMachine.messageReciever("Move",new object[] {Mathf.Cos(rotation*Mathf.Deg2Rad)*direction.x + 
			Mathf.Sin(rotation*Mathf.Deg2Rad)*direction.y,Mathf.Cos(rotation*Mathf.Deg2Rad)*direction.y - 
			Mathf.Sin(rotation*Mathf.Deg2Rad)*direction.x});// straight
    }

    void Interact()
    {
        interacting = true;
        stateMachine.messageReciever("Interact", null);
    }

    void PickUp(string powerUpType)
    {
        stateMachine.messageReciever("PickUp", powerUpType);
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

    void AirControlMovement(params object[] args)
    {
        float airControl = 0.4f;
        float airSpeed = speed;
        Vector3 airMove = new Vector3((float)args[0] * airSpeed * (1 / Time.timeScale), rigidbody.velocity.y, (float)args[1] * airSpeed) * (1 / Time.timeScale);
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, airMove, Time.deltaTime * airControl);
    }

	public void InteractComplete()
	{
        this.SendMessage("interactFinished", SendMessageOptions.DontRequireReceiver);
	}

    void SwitchToStandby()
    {
        stateMachine.ChangeState(states[3]);
    }
}
