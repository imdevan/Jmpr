using UnityEngine;
using System.Collections;

public class GoToLvl3 : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		
		//Application.LoadLevel ("STH Level 2");
		if (col.gameObject.CompareTag ("Player")) {
			Application.LoadLevel ("Jmpr Level 3");
		}
	}
}
