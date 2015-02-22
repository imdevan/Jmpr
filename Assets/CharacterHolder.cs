using UnityEngine;
using System.Collections;

public class CharacterHolder : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		transform.parent.GetComponent<ForwardBackPlatform> ().player = col.GetComponent<CharacterController> ();
	}
	
	void OnTriggerExit(Collider col){
		transform.parent.GetComponent<ForwardBackPlatform> ().player = null;
	}


}
