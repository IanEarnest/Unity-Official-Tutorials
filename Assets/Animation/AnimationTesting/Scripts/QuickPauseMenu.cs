using UnityEngine;
using System.Collections;

public class QuickPauseMenu : MonoBehaviour {

	bool isPaused;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape"))	isPaused = !isPaused;
		if(isPaused)	Time.timeScale = 0;
		else 	Time.timeScale = 1;
		
	}

	void OnGUI(){
		if(isPaused)
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 100, 250, 50), "Pause Menu");
	}
}
