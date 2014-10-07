using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "ENEMY LEFT HAND" || 
		   other.gameObject.name == "ENEMY RIGHT HAND"){
			AnimationScript2.isHit = true;
		}
		if(other.gameObject.name == "tyrant_zombie"){
			//AnimationScript2.enemyHit = true;
        }
		if(other.gameObject.name != "Plane"){
			print ("trigger: " + other.gameObject.name);
		}
	}
}
