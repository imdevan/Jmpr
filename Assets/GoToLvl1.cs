using UnityEngine;
using System.Collections;

public class GoToLvl1 : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("Player")) {
			Application.LoadLevel ("Jmpr Level 1");
		}
	}
}
