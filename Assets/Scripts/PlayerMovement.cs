using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof (CharacterController))]

public class PlayerMovement : NetworkBehaviour {

	[SyncVar] private int health = 100;

	private bool IsWalking;
	[SerializeField] private float WalkSpeed;
	[SerializeField] private float RunSpeed;
	[SerializeField] private float JumpSpeed;
	[SerializeField] private float StickToGroundForce;
	[SerializeField] private float GravityMultiplier;
	//private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
	//private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
	//private AudioClip m_LandSound;    

	private Camera PlayerCamera;
	private bool JumpPress;
	private Vector2 PlayerInput;
	private Vector3 MoveDir = Vector3.zero;
	private CharacterController PlayerController;
	private bool PreviouslyGrounded;
	private bool IsJumping;
	//private AudioSource m_AudioSource;

	private GameObject currentWeapon;

	// Use this for initialization
	void Start () {
		PlayerController = GetComponent<CharacterController>(); //Get player controller
		PlayerCamera = GetComponentInChildren<Camera>();

		if(!isLocalPlayer)
		{
			GetComponentInChildren <AudioListener>().enabled = false;
			PlayerCamera.enabled = false;
		}

		IsJumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer)
		{
			//Jumping Input
			if(JumpPress == false)
			{
				//TODO: FIND WAY TO DO THIS THAT ALLOWS IT TO BE REBOUND
				JumpPress = Input.GetKeyDown ("space");
			}
			//Landing
			if(!PreviouslyGrounded  && PlayerController.isGrounded)
			{
				MoveDir.y = 0f;
				IsJumping = false;
			}
			if(!PlayerController.isGrounded && !IsJumping && PreviouslyGrounded)
			{
				MoveDir.y = 0f;
			}
			PreviouslyGrounded = PlayerController.isGrounded;
		}
	}

	void FixedUpdate()
	{
		if (isLocalPlayer)
		{
			float speed;

			GetInput (out speed);
			
			Vector3 desiredMove = transform.forward*PlayerInput.y + transform.right * PlayerInput.x;
			
			MoveDir.x = desiredMove.x*speed;
			MoveDir.z = desiredMove.z*speed;
			
			//Jumping
			if(PlayerController.isGrounded)
			{
				MoveDir.y = -StickToGroundForce;
				
				if(JumpPress)
				{
					MoveDir.y = JumpSpeed;
					JumpPress = false;
					IsJumping = true;
				}
			}
			else{
				MoveDir += Physics.gravity * GravityMultiplier * Time.fixedDeltaTime;
			}
			
			PlayerController.Move (MoveDir*Time.fixedDeltaTime);
		}
	}

	void GetInput(out float speed)
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		if(Input.GetAxis ("Sprint") == 0)
			IsWalking = true;	
		else
			IsWalking  = false;
		
		if(IsWalking)
			speed = WalkSpeed;
		else
			speed = RunSpeed;

		//Limit speed
		PlayerInput = new Vector2(horizontal, vertical);

		if(PlayerInput.sqrMagnitude > 1)
		{
			PlayerInput.Normalize();
		}
	}

	public void ChangeHealth(int val)
	{
		health += val;
		Debug.Log (gameObject.name + " health: " + health);
	}
}
