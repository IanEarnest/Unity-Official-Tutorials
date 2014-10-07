using UnityEngine;
using System.Collections;

public class Player_Unitychan : MonoBehaviour {

	// Player
	public static Animator playerAnim;
	public static int playerHealth = 100;
	public static int playerStamina = 100;
	public static bool isPlayerHit;
	float forwardSpeed = 1.5f;
	float backwardSpeed = 1.0f;
	float rotateSpeed = 4.0f;
	float horizontal;
	float vertical;
	int idleAnimTime;


	// Use this for initialization
	void Start () {
		playerAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Stamina regen
		idleAnimTime++;
		if(playerStamina < 100){
			playerStamina++;
		}
		else{
			playerAnim.SetInteger("Stamina", 100);
		}
		if(idleAnimTime > 1000){
			idleAnimTime = 0;
		}
		
		// Player hit and damaged
		if(isPlayerHit){
			playerAnim.SetBool("isDamaged", true);
			isPlayerHit = false;
			playerHealth -= 10;
			playerAnim.SetInteger("Health", playerHealth);
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
			playerStamina -= 50;
			playerAnim.SetInteger("Stamina", playerStamina);
		}
		if(Input.GetButtonUp("Jump")) {
			playerAnim.SetBool("isJumpPressed", false);
		}
		
		// Player attack
		if(Input.GetButton("Fire1")){
			playerAnim.SetBool("isAttacking", true);
		}
		if(Input.GetButtonDown("Fire1")){
			playerStamina -= 50;
			playerAnim.SetInteger("Stamina", 50);
		}
		if(Input.GetButtonUp("Fire1")) {
			playerAnim.SetBool("isAttacking", false);
		}
		// Player run
		if(Input.GetKey(KeyCode.LeftShift)){
			playerAnim.SetBool("isSprintHeld", true);
			forwardSpeed = 4.5f;
		}
		else {
			playerAnim.SetBool("isSprintHeld", false);
			forwardSpeed = 1.5f;
		}

	}

	void FixedUpdate(){
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		playerAnim.SetFloat("Speed", vertical);
		playerAnim.speed = 1.5f;	
		//print (h + "," + v);
		Vector3 velocity = new Vector3(0, 0, vertical);
		velocity = transform.TransformDirection(velocity);
		
		if (vertical > 0.1) {
			velocity *= forwardSpeed;
		} else if (vertical < -0.1) {
			velocity *= backwardSpeed;
		}
		transform.localPosition += velocity * Time.fixedDeltaTime;
		transform.Rotate(0, horizontal * rotateSpeed, 0);
	}
}
