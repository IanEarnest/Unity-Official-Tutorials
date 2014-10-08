using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {
		// find player
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Check distance to player
		float distance = Vector3.Distance(transform.position, player.transform.position);
		//print ("distance: " + distance);

		// close enough to attack
		// Attack when in range of player
		if(distance < 0.7){
			if(animation.IsPlaying("walk02") || animation.IsPlaying("idle"))
			animation.Play ("attack02");
			transform.LookAt(player.transform.position);
		}
		if(distance > 0.7 && distance < 1){
			if(animation.IsPlaying("walk02") || animation.IsPlaying("idle"))
			animation.Play ("attack01");
		}

		// Close enough to detect
		// Point to player
		// Walk to player
		if(distance > 1 && distance < 5){
			if(animation.IsPlaying("idle"))
				animation.Play ("walk02");
			transform.LookAt(player.transform.position);
			transform.Translate (0,0, 3*Time.deltaTime);

		}

		if(distance > 5 && distance < 8){
			animation.Play ("walk01");
			transform.LookAt(player.transform.position);
			transform.Translate (0,0, (1*Time.deltaTime)/3);
		}





		/*
			// Gets a vector that points from the player's position to the target's.
			var heading = target.position - player.position;

			var distance = heading.magnitude;
			var direction = heading / distance; // This is now the normalized direction.

			var heading = target.position - player.position;
			heading.y = 0;  // This is the overground heading.


			if (heading.sqrMagnitude < maxRange * maxRange) {
	   			// Target is within range.
			}
		 */
	}
}
