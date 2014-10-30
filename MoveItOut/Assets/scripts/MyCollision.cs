using UnityEngine;
using System;
using System.Collections;

public class MyCollision : MonoBehaviour {
	public MyCollision () {
	}
	
	void OnTriggerEnter (Collider myTrigger) {
		gameController.isCollision = true;
		Debug.Log("Collision!");
	}
}


