﻿using UnityEngine;
using System.Collections;


//Trying to work on a raycasting solution for collision based upon
//http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html

public class Arthur : MonoBehaviour {

	// Use this for initialization
	public static Vector3 arthurPos;

	public GameObject WeaponPrefab;

	public int health;
	public WeaponType weapon;
	public float sides;
	public int weaponCount;

	private PhysObj thisPhys;
	private GameObject arthurObject;
	private bool crouching;
	private bool jumping;
	private float speed = 2f;
	private int weaponLimit = 2; //amount of weapon permitted on screen
	private Vector3 crouchState1 = new Vector3(1f, 0.5f, 1f);
	private Vector3 crouchState2 = new Vector3(0f, -0.25f, 0f);
	private Vector3 standState1 = new Vector3(1f, 1f, 1f);
	private Vector3 standState2 = new Vector3(0f, 0f, 0f);
	private float verticalWeaponSpawn;
	private BoxCollider boxCollider;

			
	
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

		if (jumping && thisPhys.isGrounded) {
			jumping = false;
		}

		if (!jumping)
			thisPhys.setVelocity (new Vector2(0f, thisPhys.getVelocity().y));



		if (Input.GetKey(KeyCode.LeftArrow) && !crouching && !jumping) 
		{
			sides = -1f;
			thisPhys.addVelocity(-speed, 0f);
			Debug.Log ("movingleft");
		}
		if (Input.GetKey(KeyCode.RightArrow) && !crouching && !jumping)
		{
			sides = 1f;
			thisPhys.addVelocity(speed, 0f);
			Debug.Log ("movingRight");
		}	
		if (Input.GetKey(KeyCode.DownArrow) && !jumping)
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
		if (!jumping && Input.GetKeyDown(KeyCode.UpArrow) && !crouching)
		{
			jumping = true;
			thisPhys.addVelocity (9, 90);
			crouching = false;
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
			boxCollider.transform.localScale = crouchState1;
			boxCollider.center = crouchState2;
		}
		else {
			boxCollider.transform.localScale = standState1;
			boxCollider.center = standState2;	
		}

		arthurPos = transform.position;

	}

	void OnTriggerEnter(Collider coll){
		//Find out what hit this basket
		GameObject collidedWith = coll.gameObject;
		/*if (collidedWith.tag == "Hostile") {
			health--;
			if (health == 0) {
				Destroy (this.gameObject);
			}

			//movement and invincibility

			//change state
		}*/
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
	}
}

