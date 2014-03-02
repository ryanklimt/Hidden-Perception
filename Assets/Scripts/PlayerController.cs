using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : Entity {

	public GameObject throwObj;
	
	// Player Handling
	public float gravity = 20;
	public float walkSpeed = 8;
	public float runSpeed = 12;
	public float acceleration = 30;
	public float jumpHeight = 12;

	private bool facingRight;

	// System
	private float animationSpeed;
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private float moveDirX;
	
	// States
	private bool jumping;
	private bool throwing;


	// Components
	private PlayerPhysics playerPhysics;
	private Animator animator;
	private GameManager manager;
	

	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator>();
		manager = Camera.main.GetComponent<GameManager>();
		animator.SetLayerWeight(1,1);
		throwing = false;
		facingRight = true;
	}
	
	void Update () {

		// Reset acceleration upon collision
		if (playerPhysics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}

		// Throwing Logic
		if (Input.GetButton("Throw")) {
			throwing = true;
		}

		if (throwing) {
			throwing = false;
			Vector3 mousePos = Input.mousePosition;
			mousePos.z =- (transform.position.z - Camera.main.transform.position.z - 20);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
			var targetDelta = (worldPos - transform.position);

			float tmpSpeed = 0;

			if(facingRight) tmpSpeed = 0.5f;
			if(!facingRight) tmpSpeed = -0.5f;

			GameObject projectile = Instantiate(throwObj, new Vector3(transform.position.x + tmpSpeed, transform.position.y + 2.9f, transform.position.z), transform.rotation) as GameObject;
			projectile.rigidbody.AddForce(targetDelta * 100);
		}

		for(int i=0; i < GameObject.FindGameObjectsWithTag("ThrowBall").Length; i++) {
			if(GameObject.FindGameObjectsWithTag("ThrowBall")[i].transform.position.y < -50) {
				Destroy(GameObject.FindGameObjectsWithTag("ThrowBall")[i]);
			}
		}
		
		// If player is touching the ground
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			// Jump logic
			if (jumping) {
				jumping = false;
				animator.SetBool("Jumping",false);
			}

		}
		
		// Jump Input
		if (Input.GetButtonDown("Jump")) {
			if (playerPhysics.grounded) {
				amountToMove.y = jumpHeight;
				jumping = true;
				animator.SetBool("Jumping",true);
			}
		}

		// Set animator parameters
		animationSpeed = IncrementTowards(animationSpeed,Mathf.Abs(targetSpeed),acceleration);
		animator.SetFloat("Speed",animationSpeed);
		
		// Input
		moveDirX = Input.GetAxisRaw("Horizontal");
		float speed = (Input.GetButton("Run"))?runSpeed:walkSpeed;
		targetSpeed = moveDirX * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
			
		// Face Direction
		if (moveDirX !=0) {
			transform.eulerAngles = (moveDirX>0)?Vector3.up * 180:Vector3.zero;
			if(moveDirX>0)facingRight = true;
			if(moveDirX<0)facingRight = false;
		}
		
		// Set amount to move
		amountToMove.x = currentSpeed;
		
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime, moveDirX);
	
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Checkpoint") {
			manager.SetCheckpoint(c.transform.position);
		}
		if (c.tag == "Finish") {
			manager.EndLevel();
		}
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}
