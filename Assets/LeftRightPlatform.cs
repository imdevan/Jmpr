using UnityEngine;
using System.Collections;

public class LeftRightPlatform : MonoBehaviour {
	
	public CharacterController player; 
	
	public float boundsWidth = 10f;
	public float speed = 8f;
	public bool reversed = false;
	
	private Vector3 pos1;
	private Vector3 pos2;
	
	private Vector3 platformMoveDirection;
	private float dir;
	
	
	
	// Use this for initialization
	void Start () {
		pos1 = new Vector3 (transform.position.x - boundsWidth,
		                    transform.position.y,
		                    transform.position.z);
		pos2 = new Vector3 (transform.position.x + boundsWidth,
		                    transform.position.y,
		                    transform.position.z);
		
		if (reversed)
			dir = -speed;
		else
			dir = speed;
	}
	
	// FixedUpdate is called at a fixed interval, instead of per frame
	void FixedUpdate () {
		MoveTo (dir);
		
		if (transform.position.x <= pos1.x) {
			dir *= -1;
		}
		else if (transform.position.x >= pos2.x){
			dir *= -1;
		}
	}
	
	void MoveTo(float spd){
		
		Vector3 delta = new Vector3(spd  * Time.deltaTime, 0.0f,  0.0f);
		transform.position += delta;
		
		// Move player
		if (player != null) {
			player.transform.position += delta;
		}
	}
}
