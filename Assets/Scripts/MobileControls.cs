using UnityEngine;
using System.Collections;

public class MobileControls : MonoBehaviour {

	bool left = false;
	bool right = false;
	bool down = false;
	bool up = false;
	bool yPress = false;
	public InputController playerInput;
	float downTimer = 1.3f;
	float upTimer = 1f;
	float ySpeed = 0;
	float stopTimer = 0;
	float stopTime = 1.4f;
	float speedChange = .35f;
	float maxSpeed = 1.4f;
	bool backPress = false;

	Vector2 direction = Vector2.zero;

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
		gameObject.SetActive(false);
		#endif
		downTimer = playerInput.stopTime;
		maxSpeed = playerInput.maxSpeed;
		speedChange = playerInput.speedChange;
		stopTime = playerInput.stopTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (direction.x == 0) {
			if ( left ) {
				direction.x = -1;
			}
			else if ( right ) {
				direction.x = 1;
			}
		}
		if (direction.x > 0) {
			if ( right ) {
				direction.x = 1;
			}
			else if ( left ) {
				direction.x = -1;
			}
			else {
				direction.x = 0;
			}
		}
		if (direction.x < 0) {
			if ( left ) {
				direction.x = -1;
			}
			else if ( right ) {
				direction.x = 1;
			}
			else {
				direction.x = 0;
			}
		}
		if(down)
		{
			if(stopTimer > stopTime)
			{
				ySpeed = 0;
			}
			stopTimer += Time.deltaTime;
		}
		direction.y = ySpeed;
		direction = direction*6;
		playerInput.target.SendMessage("Move", direction, SendMessageOptions.DontRequireReceiver);
	}

	public void LeftPressed() {
		left = true;
	}

	public void LeftReleased() {
		left = false;
	}

	public void RightPressed() {
		right = true;
	}

	public void RightReleased() {
		right = false;
	}

	public void Jump() {
		playerInput.target.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
	}

	public void UpPressed() {
		up = true;
		ySpeed = Mathf.Clamp(ySpeed + speedChange, 0, maxSpeed);
	}

	public void UpReleased() {
		up = false;
	}

	public void DownPressed() {
		down = true;
		ySpeed = Mathf.Clamp(ySpeed - speedChange, 0, maxSpeed);
		stopTimer = 0;
	}

	public void DownReleased() {
		down = false;
	}
}
