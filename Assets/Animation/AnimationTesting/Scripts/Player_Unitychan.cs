using UnityEngine;
using System.Collections;

public class Player_Unitychan : MonoBehaviour {

	// Player
	public static Animator playerAnim;
	public static float playerHealth = 1000;
	public static float playerStamina = 1000;
	float staminaRegen = 0.2f;
	public static bool isPlayerHit;
	float forwardSpeed = 1.5f;
	float backwardSpeed = 1.0f;
	float rotateSpeed = 4.0f;
	float horizontal;
	float vertical;
	int idleAnimTime;


	// Animations
	bool canAttack = true;
	bool canMove = true;
	int playerAnimHash;
	int attackHash = Animator.StringToHash ("Base Layer.Attack");
	int idleHash = Animator.StringToHash ("Base Layer.Idle");
	int walkHash = Animator.StringToHash ("Base Layer.Walk2");
	int walkLeftHash = Animator.StringToHash ("Base Layer.Walk3");
	int walkRightHash = Animator.StringToHash ("Base Layer.Walk4");
	int runHash = Animator.StringToHash ("Base Layer.Run1");
	int runLeftHash = Animator.StringToHash ("Base Layer.Run2");
	int runRightHash = Animator.StringToHash ("Base Layer.Run3");
	int runHandHash = Animator.StringToHash ("Base Layer.RunHand");
	int slideHash = Animator.StringToHash ("Base Layer.Slide");
	int damaged1Hash = Animator.StringToHash ("Base Layer.Damaged1");
	int damaged2Hash = Animator.StringToHash ("Base Layer.Damaged2");
	int exaustHash = Animator.StringToHash ("Base Layer.Exaust");
	int hopHash = Animator.StringToHash ("Base Layer.Hop");


	// Use this for initialization
	void Start () {
		playerAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerAnimHash = playerAnim.GetCurrentAnimatorStateInfo (0).nameHash;
		idleAnimTime++;

		// Stamina regen
		playerAnim.SetFloat("Stamina", playerStamina);
		if(playerStamina <= 0){
			playerAnim.SetBool("isSprintHeld", false);
			canAttack = false;
		}
		else{
			canAttack = true;
		}

		if(playerStamina < 1000){
			if(playerAnimHash == idleHash || 
			   playerAnimHash == walkHash || 
			   playerAnimHash == walkLeftHash || 
			   playerAnimHash == walkRightHash || 
			   playerAnimHash == exaustHash)
				playerStamina += staminaRegen;
		}
		if(idleAnimTime > 1000){
			idleAnimTime = 0;
		}
		
		// Player hit and damaged
		if(isPlayerHit){
			playerAnim.SetBool("isDamaged", true);
			isPlayerHit = false;
			playerHealth -= 150;
			playerAnim.SetFloat("Health", playerHealth);
		}
		else{
			playerAnim.SetBool("isDamaged", false);
		}




		// Checking animation playing
		if(playerAnimHash == attackHash){
			//print ("Player is attacking");
			GameObject.Find ("CHARACTER FOOT").GetComponent<BoxCollider>().enabled = true;
			canMove = false;
		}
		if(playerAnimHash == idleHash){
			//print ("Player is idle");
			canMove = true;
			playerAnim.SetBool("isAttacking", false);
			// Disable foot collider
			GameObject.Find ("CHARACTER FOOT").GetComponent<BoxCollider>().enabled = false;
		}
		if(playerAnimHash == damaged1Hash ||
		   playerAnimHash == damaged2Hash){
			canMove = false;
        }


		// Player jump
		if(Input.GetButton("Jump")){
			playerAnim.SetBool("isJumpPressed", true);
		}
		if(Input.GetButtonDown("Jump")){
			playerAnim.SetBool("isJumpPressed", true);
			playerStamina -= 50;
			playerAnim.SetFloat("Stamina", playerStamina);
		}
		if(Input.GetButtonUp("Jump")) {
			playerAnim.SetBool("isJumpPressed", false);
		}
		
		// Player attack
		if(Input.GetButtonDown("Fire1")){
			if(canAttack == true && 
			   playerAnim.GetBool("isAttacking") == false &&
			   playerAnimHash != attackHash){
				playerStamina -= 250;
				playerAnim.SetFloat("Stamina", 50);
				playerAnim.SetBool("isAttacking", true);
				canAttack = false;

			}
		}

		if(playerAnimHash == exaustHash){
			canMove = false;
		}

		// Player run
		// Left shift held for running
		if(Input.GetKey(KeyCode.LeftShift))
			playerAnim.SetBool("isSprintHeld", true);
		else    
			playerAnim.SetBool("isSprintHeld", false);

		
		// Player walking and running animations
		// Slide
		if(Input.GetButton("Fire1"))		playerAnim.SetBool("isSlidePressed", true);
		else						playerAnim.SetBool("isSlidePressed", false);
		// Run hand
		if(Input.GetButton("Fire2"))		playerAnim.SetBool("isRunHandPressed", true);
		else						playerAnim.SetBool("isRunHandPressed", false);

		// Walk left
		if(Input.GetKey(KeyCode.A) && !playerAnim.GetBool("isSprintHeld"))		
		   	playerAnim.SetBool("isLookLeft", true);
		else						
			playerAnim.SetBool("isLookLeft", false);
		// Walk right
		if(Input.GetKey(KeyCode.D) && !playerAnim.GetBool("isSprintHeld"))		
			playerAnim.SetBool("isLookRight", true);
		else						
			playerAnim.SetBool("isLookRight", false);

		// Run left
		if(Input.GetKey(KeyCode.A) && playerAnim.GetBool("isSprintHeld"))		
			playerAnim.SetBool("isRunLeft", true);
		else						
			playerAnim.SetBool("isRunLeft", false);
		// Run right
		if(Input.GetKey(KeyCode.D) && playerAnim.GetBool("isSprintHeld"))		
			playerAnim.SetBool("isRunRight", true);
		else						
			playerAnim.SetBool("isRunRight", false);




		// Sprinting moves player faster
		// Sprinting reduces stamina
		if(playerAnimHash == runHash ||
		   playerAnimHash == runLeftHash ||
		   playerAnimHash == runRightHash ||
		   playerAnimHash == runHandHash){
			//print ("player running");
			forwardSpeed = 4.5f;
			playerStamina -= staminaRegen;
		}
		else
			forwardSpeed = 1.5f;

		// Run and jump speed??
		if(playerAnimHash == hopHash ||
		   playerAnimHash == slideHash)
			forwardSpeed = 4.5f;
	}









	void FixedUpdate(){
		// Player movement
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		playerAnim.SetFloat("Speed", vertical);
		playerAnim.speed = 1.5f;	
		//print (h + "," + v);
		Vector3 velocity = new Vector3(0, 0, vertical);
		velocity = transform.TransformDirection(velocity);

		// Player speed
		if (vertical > 0.1) {
			velocity *= forwardSpeed;
		} else if (vertical < -0.1) {
			velocity *= backwardSpeed;
		}

		// player movement restrictions
		if(canMove){
			transform.localPosition += velocity * Time.fixedDeltaTime;
			transform.Rotate(0, horizontal * rotateSpeed, 0);
		}
	}
}
