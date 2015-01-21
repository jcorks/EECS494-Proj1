﻿using UnityEngine;
using System.Collections;


//Trying to work on a raycasting solution for collision based upon
//http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html

public class Arthur : MonoBehaviour {

	// Use this for initialization
	public static Vector3 arthurPos;
	public static bool isDead = false;

	public GameObject WeaponPrefab;

	public int health;
	public WeaponType weapon;
	public float sides;
	public int weaponCount;

	private PhysObj thisPhys;
	private GameObject arthurObject;
	private bool crouching;
	private bool jumping = false;
	private bool isHit = false;
	private bool wall = false;
	private float speed = 3f;
	private float jumpVel = 13f;
	private int weaponLimit = 2; //amount of weapon permitted on screen

	private float isHitTimer = 0;
	private bool isHitOnGround = false; // When hit, it does a special jump that does not show the invincibility amount
	private bool invincibleVisual;
	private char hitSide;
	private bool onLadder = false;
	private bool upLadder = false;

	private Vector3 crouchState1 = new Vector3(1f, 0.75f, 1f);
	private Vector3 crouchState2 = new Vector3(0f, -0.125f, 0f);
	private Vector3 standState1 = new Vector3(1f, 1f, 1f);
	private Vector3 standState2 = new Vector3(0f, 0f, 0f);
	private float verticalWeaponSpawn;
	private BoxCollider boxCollider;

	private bool isDying = false;


	void Awake() {
		isDead = false;
	}
	
	void Start() {

		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		boxCollider = this.gameObject.collider as BoxCollider;
		arthurObject = this.gameObject;
		health = 2;
		crouching = false;
		weapon = WeaponType.LANCE;
		sides = 1f;
	}

	void changeSide(bool sides) {
		if (sides) {
			Vector3 theScale = transform.localScale;
			theScale.z *= -1;
			transform.localScale = theScale;
		}
	}

	void Update () {


		if (!jumping && Input.GetKey(KeyCode.UpArrow) && !crouching)
		{
			jumping = true;
			print ("Jump begin!");			
			thisPhys.addVelocity (jumpVel, 90);
			crouching = false;
		}
		if (jumping && thisPhys.isGrounded) {
			if (!isHitOnGround) {
				isHitOnGround = true;
				print ("Grounded but hit");
			}
			print ("No longer jumping");
			jumping = false;
		}



		if (!jumping)
			thisPhys.setVelocity (new Vector2(0f, thisPhys.getVelocity().y));
		
		if (isDying) return;

		if (Input.GetKey(KeyCode.LeftArrow) && !crouching && thisPhys.isGrounded && !jumping && hitSide != '1') 
		{
			sides = -1f;
			thisPhys.addVelocity(-speed, 0f);
		}
		if (Input.GetKey(KeyCode.LeftArrow) && !crouching && jumping) 
		{
			sides = -1f;
		}

		if (Input.GetKey(KeyCode.RightArrow) && !crouching && thisPhys.isGrounded && !jumping && hitSide != 'r')
		{
			sides = 1f;
			thisPhys.addVelocity(speed, 0f);
		}	
		if (Input.GetKey(KeyCode.RightArrow) && !crouching && jumping) 
		{
			sides = 1f;
		}


		if (Input.GetKey(KeyCode.DownArrow) && !jumping && thisPhys.isGrounded)
		{
			crouching = true;
			verticalWeaponSpawn = 0.2f;
			Debug.Log (crouching);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow) && !jumping)
		{
			crouching = false;
			verticalWeaponSpawn = 0.5f;
			Debug.Log (crouching);
		}

		if (Input.GetKeyDown (KeyCode.Space) && weaponCount < weaponLimit) 
		{
			//arthurObject.scale
			weaponCount++;
			Debug.Log ("weapon on " + weaponCount);
			GameObject weaponObj = Instantiate (WeaponPrefab) as GameObject;
			Weapon weaponComp = weaponObj.GetComponent<Weapon>();
			weaponComp.thisArthur = this.GetComponent<Arthur>();
			Debug.Log (weaponComp.thisArthur.weaponCount);

			weaponComp.weapon = this.weapon;
			weaponComp.sides = this.sides;
			weaponObj.transform.position = new Vector2 (transform.position.x+sides, transform.position.y + verticalWeaponSpawn); 


		}
		if (crouching) {
			boxCollider.center = crouchState2;
			boxCollider.size = crouchState1;
		}
		else {
			boxCollider.center = standState2;	
			boxCollider.size = standState1;
		}



		arthurPos = transform.position;


	}

	void FixedUpdate() {
		if (isHitTimer < 0) {
			isHit = false;
			GetComponent<MeshRenderer>().renderer.enabled = true;
		} else {
			
			isHitTimer -= Time.deltaTime;
			if (isHitOnGround) {
				drawInvincibleVisual();
			}
		}
	}


	void drawInvincibleVisual() {
		if (isDying) return;
		if (invincibleVisual) {
			GetComponent<MeshRenderer>().renderer.enabled = false;
		} else {
			GetComponent<MeshRenderer>().renderer.enabled = true;
		}
		invincibleVisual = !invincibleVisual;
	}

	void OnTriggerEnter(Collider coll){
		//Find out what hit this basket
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Hostile" && !isHit) {
			if (collidedWith.GetComponent<Enemy>().ready)
				takeHit();
		}


		if (collidedWith.tag == "Item") {
			Debug.Log("item received");
			ItemType received = collidedWith.GetComponent<Items>().get();
			Destroy(collidedWith);
			if (received == ItemType.LANCE)
				weapon = WeaponType.LANCE;
			if (received == ItemType.KNIFE)
				weapon = WeaponType.KNIFE;
			if (received == ItemType.FIREBALL)
				weapon = WeaponType.FIREBALL;

		}
		if (collidedWith.tag == "Wall") {
			Debug.Log ("Wallhit");
			if (collidedWith.transform.position.x  > this.transform.position.x)
				hitSide = 'r';
			else
				hitSide = 'l';
			Debug.Log (hitSide);

		}
		if (collidedWith.tag == "Ladder") {
			Debug.Log ("near ladder");
			onLadder = true;
		}
	}

	void OnTriggerExit(Collider coll){
			GameObject collidedWith = coll.gameObject;
			if (collidedWith.tag == "Wall") {
					Debug.Log ("WallOff");
					hitSide = 'n';
			}
			if (collidedWith.tag == "Ladder") {
				Debug.Log ("LadderOff");
				onLadder = false;
			}
	}


	// take a hit
	void takeHit() {
		print ("Ouch!");
		isHit = true;
		isHitTimer = 3.0f;
		isHitOnGround = false;



		Vector3 hitVel = new Vector3 (sides*speed, jumpVel, 0);
		jumping = true;

		thisPhys.setVelocity (hitVel);
		health--;
		if (health == 0) {
			isDying = true;
			GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 255);
			Invoke ("die", 3);
		}
	}

	void die() {
		isDead = true;
		Destroy (this.gameObject);


	}
}

