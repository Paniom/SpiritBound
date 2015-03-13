using UnityEngine;
using System.Collections;

public class NewCameraRotation : MonoBehaviour {
	// Camera euler y rotation (0 - straight, 90 - right, 180 - backward, 270 - left)
	// straight is no prefab, try not to overlap too far.
	public int eulerYRot = 90;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
			other.GetComponent<PlayerController>().followingCamera.SendMessage("AddRotation",new object[] {eulerYRot,transform.position.x,transform.position.z},SendMessageOptions.DontRequireReceiver);
		}
	}
	void OnTriggerExit(Collider other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
			other.GetComponent<PlayerController>().followingCamera.SendMessage("RemoveRotation",eulerYRot,SendMessageOptions.DontRequireReceiver);
		}
	}
}
