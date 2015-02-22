using UnityEngine;
using System.Collections;

public class HoverY : MonoBehaviour {
	
	private Vector2 pos1;
	private Vector2 pos2;
	private float dir;

	public float boundsHeight = 0.02f;
	public float speed = 0.2f;
	public bool rotating = true;
	public float rotateSpeed = 2f;

	// Use this for initialization
	void Start () {
		pos1 = new Vector3 (transform.position.x,
		                    transform.position.y - boundsHeight,
		                    transform.position.z);
		pos2 = new Vector3 (transform.position.x,
		                    transform.position.y + boundsHeight,
		                    transform.position.z);
		dir = speed;
	}
	
	void Update() {

	}


	void FixedUpdate(){

		MoveTo (dir);
		
		if (transform.position.y <= pos1.y) {
			dir = speed;
		}
		if (transform.position.y >= pos2.y) {
			dir = -speed;
		}

		transform.Rotate (Vector3.one * rotateSpeed * Time.deltaTime);
	}


	void MoveTo(float speed){
		
		Vector3 delta = new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
		transform.position += delta;
	}


}
