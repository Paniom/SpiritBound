using UnityEngine;
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

	private int joystickID = -1; // used to keep track of which finger is controlling the joystick
	private int slideID = -1; // used to keep track of which finger is doing the swipe
	private Vector2 joystickPosition; // used for a moveable joystick
	private Vector2 slideStartPosition; // used for mapping a swipe
	private float endTime = 0; // used to force an end time on a swipe 
	private float jumpTime = 0; // used to compare short swipes to taps
	private float slideTime = 0; // used to show how long a press is going
	private Vector2 slideDirection; // used to calculate the swipes

	// used for showing a joystick
	public GameObject cam;
	public GameObject joystick;
	public GameObject joystickPrefab;
	
	private bool start; // used to set time the start of a swipe
	private float oWidth = 0.0f; // used to divide the screen to figure out 
	private float oHeight = 0.0f; // used to find the width

	/*
	 * Messages used: 
	 * Move, Jump, Interact, RotateRight, RotateLeft
	 */
	
	// Use this for initialization
	void Start () {
		oHeight = 2.0f*Camera.main.orthographicSize;
		oWidth = oHeight*Camera.main.aspect;
		// used to find the input type used to play the game
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
		inputDevice = InputType.Keyboard;
		#elif UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
		inputDevice = InputType.Mobile;
		#endif
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
			if (direction.y == 0) {
				if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ) {
					direction.y = 1;
				}
				else if ( Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ) {
					direction.y = -1;
				}
			}
			if (direction.y > 0) {
				if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ) {
					direction.y = 1;
				}
				else if ( Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ) {
					direction.y = -1;
				}
				else {
					direction.y = 0;
				}
			}
			if (direction.y < 0) {
				if ( Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ) {
					direction.y = -1;
				}
				else if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ) {
					direction.y = 1;
				}
				else {
					direction.y = 0;
				}
			}
			direction = direction*6;
			target.SendMessage("Move", direction, SendMessageOptions.DontRequireReceiver);
			if ( Input.GetKey(KeyCode.Space) ) {
				target.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
			}
			if(!interactActive && interactTime <= 0) {
				if ( Input.GetKey(KeyCode.E) ) {
					target.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
					interactActive = true;
				}
			}
			if (canRotate) {
				if ( Input.GetKey(KeyCode.Z) ) {
					target.SendMessage("RotateLeft", SendMessageOptions.DontRequireReceiver);
					canRotate = false;
				}
				else if ( Input.GetKey(KeyCode.X) ) {
					target.SendMessage("RotateRight", SendMessageOptions.DontRequireReceiver);
					canRotate = false;
				}
			}
		}
		if(inputDevice == InputType.Mobile) {
			for(int i = 0; i < Input.touchCount; i++){
				if(Input.touches[i].fingerId == slideID){
					if(Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary){
						slideDirection = Input.touches[i].position - slideStartPosition;
						if(start){
							endTime = Time.time + 1;
							jumpTime = Time.time + .2f;
							start = false;
						}
						slideTime = Time.time;
					}
					if(Input.touches[i].phase == TouchPhase.Ended || Input.touches[i].phase == TouchPhase.Canceled){
						slideID = -1;
						if(slideDirection.sqrMagnitude > 1000){
							setSwipe(slideDirection);
						}
						else {
							if(start || slideTime < jumpTime) {
								target.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
							}
						}
					}
					if(slideTime < endTime && slideDirection.sqrMagnitude > 40000){
						slideID = -1;
						setSwipe(slideDirection);
					}
					if(slideTime >= endTime){
						slideID = -1;
						if(slideDirection.sqrMagnitude > 1000){
							setSwipe(slideDirection);
						}
					}
				}
				else if(slideID == -1){
					if(Input.touches[i].phase == TouchPhase.Began){
						if(Input.touches[i].position.x > Screen.width/2){
							slideID = Input.touches[i].fingerId;
							slideStartPosition = Input.touches[i].position;
							start = true;
						}
					}
				}
				if(Input.touches[i].fingerId == joystickID){
					if(Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary){
						direction = Input.touches[i].position - joystickPosition;
						joystick.GetComponent<Joystick>().position(direction);
						if(direction.sqrMagnitude > 700){
							if(direction.sqrMagnitude > 22500){
								direction = direction.normalized;
							}
							else{
								float rat = direction.magnitude/150f;
								direction = direction.normalized * rat;
							}
							direction = direction*7;
						}
						else{
							direction = Vector2.zero;
						}
					}
					if(Input.touches[i].phase == TouchPhase.Ended || Input.touches[i].phase == TouchPhase.Canceled){
						joystickID = -1;
						Destroy(joystick);
					}
				}
				else if(joystickID == -1){
					if(Input.touches[i].phase == TouchPhase.Began){
						if(Input.touches[i].position.x < Screen.width/2){
							joystickID = Input.touches[i].fingerId;
							joystickPosition = Input.touches[i].position;
							if(joystick != null){
								Destroy(joystick);
							}
							oHeight = 2.0f*Camera.main.orthographicSize;
							oWidth = oHeight*Camera.main.aspect;
							joystick = Instantiate(joystickPrefab) as GameObject;
							RectTransform rect = joystick.GetComponent<RectTransform>();
							joystick.transform.parent = cam.transform;
							rect.localPosition = new Vector3((joystickPosition.x/(Screen.width)*oWidth)-oWidth/2.0f, -(oHeight/2.0f)+(joystickPosition.y/Screen.height)*oHeight, 0);
							rect.localRotation = Quaternion.Euler(new Vector3(0,0,0));
							rect.localScale = new Vector3(oWidth/10f,oWidth/10f,1);
						}
					}
				}
				target.SendMessage("Move", direction, SendMessageOptions.DontRequireReceiver);
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

	// Determines whether a swipe is vertical up the screen or horizontal and which direction
	// Sends the appropriate message to the target
	void setSwipe(Vector2 input) {
		float x = input.x;
		float y = input.y;
		if (canRotate && !interactActive) {
			if(Mathf.Abs(x) > y && Mathf.Abs(x) > 100) {
				if(x > 0) {
					target.SendMessage("RotateRight", SendMessageOptions.DontRequireReceiver);
					canRotate = false;
				}
				else {
					target.SendMessage("RotateLeft", SendMessageOptions.DontRequireReceiver);
					canRotate = false;
				}
			}
		}
		else if(y > 100) {
			if(!interactActive && interactTime <= 0) {
				target.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
				interactActive = true;
			}
		}
	}
}
