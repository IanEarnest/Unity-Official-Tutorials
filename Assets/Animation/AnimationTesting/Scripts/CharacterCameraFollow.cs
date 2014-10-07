using UnityEngine;
using System.Collections;

public class CharacterCameraFollow : MonoBehaviour {

	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPosition = player.transform.position;
		//playerPosition.x = 0;
		playerPosition.y = 0.79f;
		playerPosition.z -= 2;

		transform.position = playerPosition;
	}
}
