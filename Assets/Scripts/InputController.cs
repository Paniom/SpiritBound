using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputController : MonoBehaviour {
	
	enum InputType{
		Keyboard,
		Mobile
	}

	InputType inputDevice = InputType.Keyboard; // used for knowing input being used.
	public GameObject target; // used to send messages to for controls to be used
	private Vector2 direction = Vector2.zero; // used to control the movement passed through

	private bool canRotate = true;
	private bool interactActive = false;
	private float interactDelay = 1.5f;
	private float interactTime = 0;

	public float maxSpeed = 1.4f;
	public float speedChange = .35f;
	public float ySpeed = 0;
	public float stopTime = 1.3f;
	private float stopTimer = 0;
	private bool yPress = false;
	private bool backPress = false;

	/*
	 * Messages used: 
	 * Move, Jump, Interact, RotateRight, RotateLeft
	 */
	
	// Use this for initialization
	void Start () {
		// used to find the input type used to play the game
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
		inputDevice = InputType.Keyboard;
		#elif UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
		inputDevice = InputType.Mobile;
		#endif
	}
	
	// Update is called once per frame
	void Update () {
        if (interactTime > 0)
        {
            interactTime -= Time.deltaTime;
        }
        else
        {
            interactTime = 0;
        }
		if(inputDevice == InputType.Keyboard) {
			if (direction.x == 0) {
				if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ) {
					direction.x = -1;
				}
				else if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ) {
					direction.x = 1;
				}
			}
			if (direction.x > 0) {
				if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ) {
					direction.x = 1;
				}
				else if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ) {
					direction.x = -1;
				}
				else {
					direction.x = 0;
				}
			}
			if (direction.x < 0) {
				if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ) {
					direction.x = -1;
				}
				else if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ) {
					direction.x = 1;
				}
				else {
					direction.x = 0;
				}
			}
			if( Input.GetKeyDown(KeyCode.W) && !yPress )
			{
				ySpeed = Mathf.Clamp(ySpeed + speedChange, 0, maxSpeed);
				yPress = true;
			}
			if( Input.GetKeyUp(KeyCode.W) )
			{
				yPress = false;
			}
			if( Input.GetKeyDown(KeyCode.S) && !backPress )
			{
				ySpeed = Mathf.Clamp(ySpeed - speedChange, 0, maxSpeed);
				stopTimer = 0;
				backPress = true;
			}
			if( Input.GetKey(KeyCode.S) )
			{
				if(stopTimer > stopTime)
				{
					ySpeed = 0;
				}
				stopTimer += Time.deltaTime;
			}
			if( Input.GetKeyUp(KeyCode.S) )
			{
				backPress = false;
			}
			direction.y = ySpeed;
			direction = direction*6;
			target.SendMessage("Move", direction, SendMessageOptions.DontRequireReceiver);
			if ( Input.GetKeyDown(KeyCode.Space) ) {
				target.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
			}
			if (canRotate) {
				if ( Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1) ) {
					target.SendMessage("ChangeToFox", SendMessageOptions.DontRequireReceiver);
					//canRotate = false;
				}
				else if ( Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2) ) {
					target.SendMessage("ChangeToMuskalo", SendMessageOptions.DontRequireReceiver);
					//canRotate = false;
				}
				else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3) )
                {
                    target.SendMessage("ChangeToWolf", SendMessageOptions.DontRequireReceiver);
                    //canRotate = false;
                }
			}
		}
	}

	// Resets the interact buttons to be used again
	// starts the delay on using the interact button
	void interactFinished() {
		interactActive = false;
		interactTime = interactDelay;
	}

	void rotateFinished() {
		canRotate = true;
	}
}
