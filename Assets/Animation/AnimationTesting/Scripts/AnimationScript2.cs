using UnityEngine;
using System.Collections;

public class AnimationScript2 : MonoBehaviour {
	
	// Player
	public GameObject player;
	public Animator playerAnim;
	int health = 100;
	int stamina = 100;
	public static bool isHit;
	float forwardSpeed = 1.5f;
	float backwardSpeed = 1.0f;
	float rotateSpeed = 4.0f;
	float h;
	float v;
	int idleTime;
	bool dieing;

	// Enemy
	public GameObject enemy;
	Animator enemyAnim;
	int enemyHP = 20;
	public static bool enemyHit;
	public GameObject enemyArmature;

	// Use this for initialization
	void Start () {
		// Shortcuts
		playerAnim = player.GetComponent<Animator> ();
		enemyAnim = enemy.GetComponent<Animator> ();
	}
	
	void OnGUI(){
		GUILayout.Label ("Health: " + health + "\n" + 
		                 "Stamina: " + stamina + "\n" + 
		                 "speed: " + playerAnim.GetFloat("Speed") + "\n" + 
		                 "isAttacking: " + playerAnim.GetBool("isAttacking") + "\n" + 
		                 "isSprintHeld: " + playerAnim.GetBool("isSprintHeld") + "\n" + 
		                 "isJumpPressed: " + playerAnim.GetBool("isJumpPressed") + "\n" + 
		                 "isDamaged: " + playerAnim.GetBool("isDamaged") + "\n");
	}
	
	void FixedUpdate(){
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		playerAnim.SetFloat("Speed", v);
		playerAnim.speed = 1.5f;	
		//print (h + "," + v);
		Vector3 velocity = new Vector3(0, 0, v);
		velocity = player.transform.TransformDirection(velocity);
		
		if (v > 0.1) {
			velocity *= forwardSpeed;
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}
		player.transform.localPosition += velocity * Time.fixedDeltaTime;
		player.transform.Rotate(0, h * rotateSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		// Stamina regen
			idleTime++;
			if(stamina < 100){
				stamina++;
			}
			else{
				playerAnim.SetInteger("Stamina", 100);
			}
			if(idleTime > 1000){
				idleTime = 0;
			}

		// Player hit and damaged
		if(isHit && dieing == false){
			playerAnim.SetBool("isDamaged", true);
			isHit = false;
			health -= 10;
			playerAnim.SetInteger("Health", health);
		}
		else{
			playerAnim.SetBool("isDamaged", false);
		}




		// Player jump
		if(Input.GetButton("Jump")){
			playerAnim.SetBool("isJumpPressed", true);
		}
		if(Input.GetButtonDown("Jump")){
			playerAnim.SetBool("isJumpPressed", true);
			stamina -= 50;
			playerAnim.SetInteger("Stamina", stamina);
		}
		if(Input.GetButtonUp("Jump")) {
			playerAnim.SetBool("isJumpPressed", false);
		}

		// Player attack
		if(Input.GetButton("Fire1")){
			playerAnim.SetBool("isAttacking", true);
		}
		if(Input.GetButtonDown("Fire1")){
			stamina -= 50;
			playerAnim.SetInteger("Stamina", 50);
		}
		if(Input.GetButtonUp("Fire1")) {
			playerAnim.SetBool("isAttacking", false);
		}
		// Player run
		if(Input.GetKey(KeyCode.LeftShift)){
			playerAnim.SetBool("isSprintHeld", true);			
		}
		else {
			playerAnim.SetBool("isSprintHeld", false);
		}

		
		//////ENEMY////////
		if(Input.GetKeyDown(KeyCode.H)){
			enemy.animation.Play ("attack01");

		}

		if(enemyHit){
			enemy.animation.Play ("damage01");
			enemyHP -= 10;
			enemyHit = false;
		}
		if(enemyHP <= 0){
			enemy.animation.Play ("death");
			enemyHP = 20;
		}

		if(enemy.animation.IsPlaying("death")){
			dieing = true;
			//enemy.animation.clip.frameRate = 1;
			//if(enemy.animation.GetClip(""+(enemy.animation.GetClipCount() - 1))){
				
			//}
		}
		// Idle when not animated
		if(enemy.animation.isPlaying == false){
			enemy.animation.Play ("idle");
			if(dieing == true){
				enemy.animation.Stop();
				enemy.collider.enabled = false;
				//Destroy(enemyArmature);
				enemy.animation.enabled = false;
				enemyAnim.enabled = false;
			}
		}
	}
}



