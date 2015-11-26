using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float playerAcc = 1.0f;

	CharacterController controller;

	// Use this for initialization
	void Awake () {
		//Spawn player in appropriate position
	}

	void Start(){
		controller = gameObject.GetComponent<CharacterController> ();
	}
	
	void FixedUpdate () {

		Vector3 moveVec = new Vector3 (0f, 0f, 0f);

		if(Input.GetAxis ("Horizontal") != 0)
		{
			moveVec += transform.right * Input.GetAxis ("Horizontal") * playerAcc;
		}
		if (Input.GetAxis ("Vertical") != 0) 
		{
			moveVec += transform.forward * Input.GetAxis ("Vertical") * playerAcc;
		}

		controller.Move (moveVec);
		Debug.Log (controller.velocity);
	}
}
