//-----------------------------------------
//   PlayerController.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float inputHorizontal;
    private float inputVertical;
    private float seconds;
	
	// Variables to help with Jump function
	public float speed = 2f;
	public float jumpSpeed = 10.0f; 
	public float gravity = 5.0f;
	public float playerHorizontalSpeed = 2.5f;

	private Vector3 moveDirection = Vector3.zero; 
	private Vector3 rotateDirection = Vector3.zero;
	private float vSpeed = 0; 
	private float inputTimer = 0f;

	// Variables to handle player movement states 
	private bool jumping = false;
	private bool walking = false;
	private bool cbWasPreviouslyTriggered = false;


	// 'Character'
	private CharacterController controller;

	// Google Cardboard Interface
	private CardboardHead head;


    void Start ()
    {
		controller = GetComponent<CharacterController> ();
		head = Camera.main.GetComponent<StereoController>().Head;
    }

    void FixedUpdate ()
    {
        // grabs input in update loop for best accuracy

		// Handling jump function
		
		moveDirection = transform.forward * Input.GetAxis("Vertical") * speed; 

		if (controller.isGrounded) { 
			vSpeed = 0; // a grounded character has zero vert speed unless... 

			// Jump or Walk
			// Start jump counter on "space" or cardboard trigger
			//if (Input.GetKeyDown ("space") || Cardboard.SDK.CardboardTriggered) { 
			//if(Input.GetKeyDown("space")){

			#if UNITY_EDITOR
			if(Input.GetMouseButtonDown(0)){
			#else
			if(Cardboard.SDK.CardboardTriggered){
			#endif
				if(inputTimer == 0f)
					startInputTimer();
			} 

			// If the player holds space or the cardboard trigger for longer than 0.5 seconds
			// walk
			//if(Input.GetKey ("space") || Cardboard.SDK.CardboardTriggered){	
			//			if(Input.GetKey("space")){	
				
			#if UNITY_EDITOR
			if(Input.GetMouseButton(0)){
			#else
			if(Cardboard.SDK.CardboardTriggered){
			#endif

				inputTimer += Time.deltaTime;
				if(inputTimer > 0.25f) 
					setPlayerState("walk");
			}
			
			// If the player holds space or the cardboard trigger for less than 0.5 seconds
			// jump
			//if(Input.GetKeyUp("space") || !Cardboard.SDK.CardboardTriggered){
					//			if(Input.GetKeyUp("space")){
			#if UNITY_EDITOR
			if(Input.GetMouseButton(0)){
			#else
			if(!Cardboard.SDK.CardboardTriggered){
			#endif
				if(inputTimer > 0.01f && inputTimer < 0.25f && cbWasPreviouslyTriggered)
					setPlayerState("jump");
			}

			// Player is idle
			if(!Input.GetMouseButton(0) && !Cardboard.SDK.CardboardTriggered && inputTimer > 0.5f){
				setPlayerState("idle");
			}

//			rotateIfNeeded();

		} 

		// Potentially use this to detect camrea rotation
		// Cardboard.SDK.camera.rigidbody.rotation


		// Move the player in the air if they are jumping
		if(jumping || walking) {
						moveDirection.x = head.Gaze.direction.x * speed * playerHorizontalSpeed;
						moveDirection.z = head.Gaze.direction.z * speed * playerHorizontalSpeed;	
			inputTimer += Time.deltaTime;
		}

		// Apply gravity 
					vSpeed -= gravity * Time.deltaTime* speed ; 
		moveDirection.y = vSpeed; 
		
		// include vertical speed 
		// Move the controller 
		controller.Move(moveDirection * Time.deltaTime); 

		if(transform.position.y < -100)
			Application.LoadLevel (Application.loadedLevelName);

    }

	void setPlayerState(string s){
		switch (s) {
		case "walk":
			walking = true;
			break;
		case "jump":
			jumping = true;
			vSpeed = jumpSpeed;
			break;
		case "idle":
			jumping = false;
			walking = false;
			inputTimer = 0;
			cbWasPreviouslyTriggered = false;
			break;
		}
	}


	void startInputTimer(){
		inputTimer = 0;
		cbWasPreviouslyTriggered = true;
	}

	// Rotate the player if the action is called for
	void rotateIfNeedded(){
		
		// Handle rotation while testing with Unity
		// Negative is to the right
		if (Input.GetKey ("-")) {
			rotateDirection.z -= -1f;
		} else if (Input.GetKey ("=")) {
			rotateDirection.z += -1f;
		} else if (Input.GetKeyDown ("0")) { 
			rotateDirection.z = 0f;
		}
		
		// Turn left
		if (rotateDirection.z > 35f) {
			rotateDirection.y = -1f;
		}
		// Turn right
		else if (rotateDirection.z < -35f) {
			rotateDirection.y = 1f;
		} 
		// Stop turning
		else {
			rotateDirection.y = 0f;
		}
	}


}