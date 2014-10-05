using UnityEngine;
using System.Collections;

public class AnimationScript1 : MonoBehaviour {
	
	// Player
	public GameObject player;
	Animator playerAnim;
	bool playerIsPlaying;
	int health = 100;
	int stamina = 100;
	public static bool isHit;
	int idleCount;
	bool playerIsIdle;
	bool playerIsWalking;
	bool playerIsWalkingBackwards;
	bool shiftHeld;
	bool playerJump;
	bool playerSlide;
	bool playerRun;
	bool playerIsRunning;
	bool playerIsJumping;
	bool playerIsSliding;
	bool playerIsMoving;
	public float forwardSpeed = 1.5f;
	public float backwardSpeed = 1.0f;
	public float rotateSpeed = 4.0f;
	float h;
	float v;
	
	// Enemy
	public GameObject enemy;
	Animator enemyAnim;
	
	// Use this for initialization
	void Start () {
		// Shortcuts
		enemyAnim = enemy.GetComponent<Animator> ();
		playerAnim = player.GetComponent<Animator> ();
		playerIsPlaying = player.animation.isPlaying;
	}
	
	void PlayingAnimations(){
		// Not idle
		if(!playerIsPlaying.Equals("Idle")){
			playerIsIdle = false;
		}
		// Idle
		if(playerIsPlaying.Equals("Idle")){
			playerIsIdle = true;
		}
		
		// Idle animations
		if(playerIsIdle){
			idleCount++;
		}
		else{
			idleCount = 0;
		}
		if(idleCount > 0 && idleCount < 800){
			//playerAnim.Play("Idle");
		}
		if(idleCount > 800 && idleCount < 1200){
			playerAnim.Play("Idle2");
		}
		if(idleCount > 1200 && idleCount < 1600){
			playerAnim.Play("Idle3");
		}
		if(idleCount > 1600 && idleCount < 2500){
			playerAnim.Play("Idle4");
		}
		
		// Resting
		if(playerIsPlaying.Equals("Idle") && stamina < 100){
			playerAnim.Play("Exaust");
		}
		
		
		// Walk
		if(v > 0.1){
			playerIsWalking = true;
			playerAnim.Play("Walk2");
			playerIsWalkingBackwards = false;
		}
		else{
			playerIsMoving = false;
		}
		
		// Backward
		if (v < -0.1) {
			playerIsWalkingBackwards = true;
			playerAnim.Play("Walk1");
		}
		else if (v == 0){
			playerIsWalkingBackwards = false;
			playerIsIdle = true;
		}
		
		if(playerIsMoving && Input.GetKeyDown (KeyCode.Space) && shiftHeld){
			playerSlide = true;
		}
		if(playerIsMoving && Input.GetKeyDown (KeyCode.Space)){
			playerJump = true;
		}
		if(playerIsMoving && shiftHeld){
			playerRun = true;
		}
		
		if(player.animation.IsPlaying("Run1")){
			playerIsRunning = true;
		}
		else
			playerIsRunning = false;
		
		
		// Run
		if(playerIsRunning){
			//playerIsRunning = true;
			playerAnim.Play("Run1");
			stamina--;
			stamina--;
		}
		//else 
		//playerIsRunning = false;
		
		// Jump
		if(playerJump){
			playerAnim.Play("Jump1");
			stamina -= 20;
			if(!player.animation.IsPlaying("Jump1")){
				playerIsJumping = false;
			}
		}
		// Slide
		if(playerSlide){
			playerAnim.Play("Slide1");
			stamina -= 40;
			//player.transform.localPosition += velocity * Time.fixedDeltaTime;
			if(!player.animation.IsPlaying("Slide1")){
				playerIsSliding = false;
			}
		}
		
		///////PLAYER///////
		if (Input.GetKeyDown (KeyCode.LeftShift))
			shiftHeld = true;
		else
			shiftHeld = false;
		
		
		/*
		// Forward
		if (v > 0.1) {
			playerIsWalkingBackwards = false;
			if (Input.GetKeyDown (KeyCode.Space)){
				if (shiftHeld){
					playerIsSliding = true;
				}
				else{
					playerIsJumping = true;
				}
			}
			// Forward
			if (shiftHeld) {
				playerIsRunning = true;
				playerIsWalking = false;
			}
			else{
				playerIsRunning = false;
				playerIsWalking = true;
			}
		}
		*/
	}
	
	void OnGUI(){
		GUILayout.Label ("Health: " + health + "\n" + 
		                 "Stamina: " + stamina + "\n" +
		                 "Idle: " + playerIsIdle + "\n" + 
		                 "Jumping: " + playerIsJumping + "\n" + 
		                 "Running: " + playerIsRunning + "\n" + 
		                 "Sliding: " + playerIsSliding + "\n" + 
		                 "Walking: " + playerIsWalking + "\n" + 
		                 "Backwards: " + playerIsWalkingBackwards);
	}
	
	void FixedUpdate(){
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		playerAnim.SetFloat("Speed", v);
		playerAnim.speed = 1.5f;	
		//print (h + "," + v);
		Vector3 velocity = new Vector3(0, 0, v);
		velocity = player.transform.TransformDirection(velocity);
		
		if (v > 0.1 && shiftHeld) {
			velocity *= (forwardSpeed * 2);
		}
		else if (v > 0.1) {
			velocity *= forwardSpeed;
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}
		player.transform.localPosition += velocity * Time.fixedDeltaTime;
		player.transform.Rotate(0, h * rotateSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//PlayingAnimations ();
		
		// When player is hit by enemy
		if(isHit){
			PlayerDamaged();
		}
		// Stamina regen
		if(playerIsIdle){
			if(stamina < 100){
				stamina++;
			}
		}
		
		//////ENEMY////////
		if(Input.GetKeyDown(KeyCode.H)){
			enemy.animation.Play ("attack01");
		}
		
		// Idle when not animated
		if(enemy.animation.isPlaying == false){
			enemy.animation.Play ("idle");
		}
	}
	
	void PlayerDamaged(){
		// Lose health
		health -= 10;
		
		// If not dead play damaged
		// else play damaged2
		if(health > 0)
			player.GetComponent<Animator>().Play ("Damaged1");
		else
			player.GetComponent<Animator>().Play ("Damaged2");
		
		
		// display health
		print (health);
		
		// hit/damage finished
		isHit = false;
	}
}