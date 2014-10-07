using UnityEngine;
using System.Collections;

public class EnemyCollide : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "CHARACTER FOOT"){
			Enemy_Tyrant_Zombie.enemyHit = true;
        }
		if(other.gameObject.name != "Plane"){
			print ("enemy trigger: " + other.gameObject.name);
		}
	}
}
