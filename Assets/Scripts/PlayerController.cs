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
    public bool invertYAxis;
    public float boundsWidth;
    public float boundsHeight;
    public float speed;
    public float tilt;
    public float bounce;
    public float bounceSpeed;

    public GameObject bullet;
    public float fireDistance;

    public GameObject explosion;

    private Vector3 defaultPos;
    private float inputHorizontal;
    private float inputVertical;
    private float seconds;
	
	// Variables to help with Jump function
	// public float speed = 6.0f;
	public float jumpSpeed =20.0f; 
	public float gravity = 5.0f;
	public float jumpHorizontalSpeed = 2.0f;

	private Vector3 moveDirection = Vector3.zero; 
	private Vector3 jumpStartDir = Vector3.zero;
	private float vSpeed = 0; 

	// 'Character'
	private CharacterController controller;

	// Google Cardboard Interface
	private CardboardHead head;

	// Particle system
	private ParticleSystem particleSys;

	// Boolean is true once player starts moving (the level begins)
	public bool startedMoving = false; 


    void Start ()
    {
        // fetch default position from our inspector
        defaultPos = transform.position;
		controller = GetComponent<CharacterController> ();
		head = Camera.main.GetComponent<StereoController>().Head;
		particleSys = GetComponent<ParticleSystem> ();
    }

    void Update ()
    {
        // grabs input in update loop for best accuracy
        PlayerInput();

            // fire button triggers bullets (space bar)
		if (Input.GetButtonDown("Jump") )
            {
                // handles bullet firing
                Fire(fireDistance);
            }

		// Handling jump function
		
		moveDirection = transform.forward * Input.GetAxis("Vertical") * speed; 
		
		if (controller.isGrounded) { 
			vSpeed = 0; // a grounded character has zero vert speed unless... 
		//	particleSys.Stop();
			if (Input.GetKeyDown ("tab")|| Cardboard.SDK.CardboardTriggered ) { 
				vSpeed = jumpSpeed; 
				jumpStartDir = head.Gaze.direction;

				if(!startedMoving)
					startedMoving = true;

			} 
		} 
		if(!controller.isGrounded) {
			//	moveDirection.x = jumpStartDir.x * jumpHorizontalSpeed;
			//	moveDirection.z = jumpStartDir.z * jumpHorizontalSpeed;
			moveDirection.x = head.Gaze.direction.x * jumpHorizontalSpeed;
			moveDirection.z = head.Gaze.direction.z * jumpHorizontalSpeed;
		}
		// Apply gravity 
		vSpeed -= gravity * Time.deltaTime; 
		moveDirection.y = vSpeed; 
		
		// include vertical speed 
		// Move the controller 
		controller.Move(moveDirection * speed * Time.deltaTime); 
		if(transform.position.y < 0)
			Application.LoadLevel (Application.loadedLevelName);
    }

	void FixedUpdate ()
	{
        // run movement function to handle rigidbody physics
        Movement();
	}

    void PlayerInput()
    {
        // fetch our input for movememnt
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (invertYAxis)
        {
            inputVertical *= -1;
        }
    }

    void Movement()
    {
        // update player velocity
        Vector3 input = new Vector3(inputHorizontal, inputVertical, 0.0f);
        // TOFIX rigidbody.velocity = input * speed;

        // create a hover effect using a sin wave
		// TOFIX  float bounceY = rigidbody.position.y + bounce * Mathf.Sin(bounceSpeed * Time.time);

        // apply hover effect, and clamp player within bounds
		// TOFIX rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, -boundsWidth, boundsWidth),
		// TOFIX                                          Mathf.Clamp(bounceY, -boundsHeight, boundsHeight),
		// TOFIX                                 rigidbody.position.z);

        // apply tilt effect to our rotation
		// TOFIX float tiltX = rigidbody.velocity.y * -tilt;
		// TOFIX float tiltZ = rigidbody.velocity.x * -tilt;
		// TOFIX rigidbody.rotation = Quaternion.Euler(tiltX, 0.0f, tiltZ);
    }

    void Fire (float distance)
    {
        // define left firing position, add distance to spawn ahead of player
        Vector3 fireFromLeft = new Vector3(transform.position.x - distance,
                                transform.position.y,
                                transform.position.z + distance);

        // define right firing position, add distance to spawn ahead of player
        Vector3 fireFromRight = new Vector3(transform.position.x + distance,
                                transform.position.y,
                                transform.position.z + distance);

        // spawn 2 bullets
        Instantiate(bullet, fireFromLeft, transform.rotation);
        Instantiate(bullet, fireFromRight, transform.rotation);
    }

    void Destruction()
    {
        // play destruction sound
        audio.Play();

        // spawn an explosion effect
        Instantiate(explosion, transform.position, transform.rotation);

        // change game state to game over
        GameController.gameState = GameStates.GameOver;
        GameController.changeState = true;
    }
	void OnControllerColliderHit (ControllerColliderHit hit ) {
		if ( hit.gameObject.CompareTag ( "Platform" ) ) {
			var normal = hit.normal;
			if (normal.y < 0) { // if the bottom side hit something 
				Debug.Log ("You Hit the floor");
			}
			if (normal.y > 0) { // if the top side hit something
				hit.gameObject.renderer.material.color = new Color(0.945f,0.768f, 0.059f);
			}    
		}
	}

    void OnTriggerEnter(Collider col)
    {
        // if player collides with a danger object...
        if (col.gameObject.tag == "Ground")
		{
			Application.LoadLevel (Application.loadedLevelName);
			// run destruction function
            // Destruction();
        }
    }
}