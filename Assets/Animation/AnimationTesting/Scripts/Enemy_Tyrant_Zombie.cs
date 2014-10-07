using UnityEngine;
using System.Collections;

public class Enemy_Tyrant_Zombie : MonoBehaviour {

	// Enemy
	Animator enemyAnim;
	int enemyHP = 20;
	public static bool enemyHit;
	public GameObject enemyArmature;
	bool dieing;

	// Use this for initialization
	void Start () {
		enemyAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//////ENEMY////////
		if(Input.GetKeyDown(KeyCode.H)){
			animation.Play ("attack01");
			
		}
		
		if(enemyHit){
			animation.Play ("damage01");
			enemyHP -= 10;
			enemyHit = false;
		}
		if(enemyHP <= 0){
			animation.Play ("death");
			enemyHP = 20;
		}
		
		if(animation.IsPlaying("death")){
			dieing = true;
			//enemy.animation.clip.frameRate = 1;
			//if(enemy.animation.GetClip(""+(enemy.animation.GetClipCount() - 1))){
			
			//}
		}
		// Idle when not animated
		if(animation.isPlaying == false){
			animation.Play ("idle");
			if(dieing == true){
				animation.Stop();
				collider.enabled = false;
				animation.enabled = false;
				enemyAnim.enabled = false;
				Destroy(gameObject);
			}
		}
	}
}
