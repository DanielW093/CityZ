using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

	[SyncVar] public float playerSpeed = 7.0f;
	[SyncVar] public float jumpSpeed = 7.0f;
	const float gravity = 4f;

	Rigidbody controller;

	// Use this for initialization
	void Awake () {
		//Spawn player in appropriate position
	}

	void Start(){
		controller = gameObject.GetComponent<Rigidbody> ();
	}
	
	void Update () {
		//Player Movement
		if (isLocalPlayer) {
			Vector3 moveVec = Vector3.zero;
			if (Input.GetAxis ("Horizontal") != 0) {
				moveVec += transform.right * Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime;
			}
			if (Input.GetAxis ("Vertical") != 0) {
				moveVec += transform.forward * Input.GetAxis ("Vertical") * playerSpeed * Time.deltaTime;
			}
			//if(Input.GetAxis ("Jump") != 0 && controller.)
			//{
			//	Debug.Log("Jump");
			//	moveVec += transform.up * Input.GetAxis ("Jump") * jumpSpeed;
			//}

			controller.AddRelativeForce(moveVec);
		}
	}
}
