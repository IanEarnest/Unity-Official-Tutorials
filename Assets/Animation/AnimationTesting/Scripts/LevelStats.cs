using UnityEngine;
using System.Collections;

public class LevelStats : MonoBehaviour {

	void OnGUI(){
		GUILayout.Label ("Health: " + Player_Unitychan.playerHealth + "\n" + 
		                 "Stamina: " + Player_Unitychan.playerStamina);

		GUI.Box(new Rect(0, 30, 500, 20), "Health");
		GUI.Box(new Rect(0, 30, Player_Unitychan.playerHealth/2, 20), Player_Unitychan.playerHealth.ToString());

		GUI.Box(new Rect(0, 50, 500, 20), "Stamina");
		GUI.Box(new Rect(0, 50, Player_Unitychan.playerStamina/2, 20), Player_Unitychan.playerStamina.ToString());

		if(Player_Unitychan.playerHealth <= 0){
			GUI.Box(new Rect(0, 100, Screen.width, 300), "YOU DIED");
		}
	}
}
