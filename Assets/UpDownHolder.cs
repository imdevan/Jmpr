using UnityEngine;
using System.Collections;

public class UpDownHolder : MonoBehaviour {
	
	CharacterController controller;
	
	void Start(){
	}
	
	void FixedUpdate(){ 
	}
	
	void OnTriggerEnter(Collider col){
		
		transform.parent.GetComponent<UpDownPlatform> ().player = col.GetComponent<CharacterController> ();
		/// col.transform.parent = transform;
		// Try this: http://answers.unity3d.com/questions/8207/charactercontroller-falls-through-or-slips-off-mov.html
	}
	
	void OnTriggerExit(Collider col){
		transform.parent.GetComponent<UpDownPlatform> ().player = null;
//		col.transform.parent = null;
//		col.transform.SetParent (null);	 
	}
	
}
