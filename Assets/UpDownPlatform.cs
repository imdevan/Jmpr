using UnityEngine;
using System.Collections;

public class UpDownPlatform : MonoBehaviour {
	
	public CharacterController player; 
	
	public float boundHeight = 0.25f;
	public float speed;
	public bool reversed = false;
	
	private Vector3 platformMoveDirection;
	private float dir;
	private Vector3 pos1;
	private Vector3 pos2;
	
	
	// Use this for initialization
	void Start () {
		pos1 = new Vector3 (transform.position.x,
		                    transform.position.y - boundHeight,
		                    transform.position.z);
		pos2 = new Vector3 (transform.position.x,
		                    transform.position.y + boundHeight,
		                    transform.position.z);
		
		if (reversed)
			dir = -speed;
		else
			dir = speed;
	}
	
	// FixedUpdate is called at a fixed interval, instead of per frame
	void FixedUpdate () {

		MoveTo (dir);
		
		if (transform.position.y <= pos1.y) {
			dir *= -1;
		}
		else if (transform.position.y >= pos2.y){
			dir *= -1;
		}
	}
	
	void MoveTo(float spd){
		
		Vector3 delta = new Vector3(0.0f, spd  * Time.deltaTime, 0.0f);
		transform.position += delta;
		
		// Move player
		if (player != null) {
			player.transform.position += delta;
		}
	}
}
