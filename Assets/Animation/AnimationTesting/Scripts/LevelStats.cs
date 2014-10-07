using UnityEngine;
using System.Collections;

public class LevelStats : MonoBehaviour {

	void OnGUI(){
		GUILayout.Label ("Health: " + Player_Unitychan.playerHealth + "\n" + 
		                 "Stamina: " + Player_Unitychan.playerStamina + "\n" + 
		                 "isAttacking: " + Player_Unitychan.playerAnim.GetBool("isAttacking") + "\n" + 
		                 "isSprintHeld: " + Player_Unitychan.playerAnim.GetBool("isSprintHeld") + "\n" + 
		                 "isJumpPressed: " + Player_Unitychan.playerAnim.GetBool("isJumpPressed") + "\n" + 
		                 "isDamaged: " + Player_Unitychan.playerAnim.GetBool("isDamaged") + "\n");
	}
}
