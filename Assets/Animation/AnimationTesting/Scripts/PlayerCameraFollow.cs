using UnityEngine;
using System.Collections;

public class PlayerCameraFollow : MonoBehaviour {

	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPosition = player.transform.position;
		//playerPosition.x = 0;
		playerPosition.y = 2.20f;
		playerPosition.z -= 2;

		transform.position = playerPosition;
	}
}
