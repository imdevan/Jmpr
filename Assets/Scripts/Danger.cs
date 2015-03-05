//-----------------------------------------
//   Danger.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour 
{
    public float speedDiff;
    public float destroySeconds;
    public float destroyScore;
	public GameObject explosion;
	public float gravity = 5.0f;
	public GameObject player;

    // Use this for initialization
    void Start()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
        // destroy object after x seconds
        //Destroy(this.gameObject, destroySeconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // add forward force to the object
		//if(player.GetComponent<PlayerController>().startedMoving)
       		//rigidbody.velocity = transform.up * -(speedDiff + GameController.gameSpeed);

    }

    public void Destruction()
    {
        // increase the game score by 1
        GameController.gameScore += destroyScore;

        // play destruction sound
        GetComponent<AudioSource>().Play();

        // spawn explosion effect
        Instantiate(explosion, transform.position, transform.rotation);

        // disable render and collider
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Bullet")
		{
			Destruction();
		}
		if (col.gameObject.tag == "Ground")
		{
			Destruction();
		}
	}
}
