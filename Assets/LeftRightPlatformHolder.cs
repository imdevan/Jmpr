using UnityEngine;
using System.Collections;

public class LeftRightPlatformHolder : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		transform.parent.GetComponent<LeftRightPlatform> ().player = col.GetComponent<CharacterController> ();
	}
	
	void OnTriggerExit(Collider col){
		transform.parent.GetComponent<LeftRightPlatform> ().player = null;
	}
	
	
}
