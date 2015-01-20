﻿using UnityEngine;
using System.Collections;


//Trying to work on a raycasting solution for collision based upon
//http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html

public class Arthur : MonoBehaviour {

	// Use this for initialization

	public GameObject WeaponPrefab;

	public int health;
	public int weapon;
	public float sides;

	private PhysObj thisPhys;
	private GameObject arthurObject;
	private bool crouching;
	private bool jumping;
	private float speed = 2f;
	
	void Start() {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		arthurObject = this.gameObject;
		health = 2;
		crouching = false;
		weapon = 2;
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
			sides = 1f;
			thisPhys.addVelocity(-speed, 0f);
			Debug.Log ("movingleft");
		}
		if (Input.GetKey(KeyCode.RightArrow) && !crouching && !jumping)
		{
			sides = -1f;
			thisPhys.addVelocity(speed, 0f);
			Debug.Log ("movingRight");
		}	
		if (Input.GetKey(KeyCode.DownArrow))
		{
			crouching = true;
			Debug.Log (crouching);
		}
		if (!jumping && Input.GetKeyDown(KeyCode.UpArrow))
		{
			jumping = true;
			thisPhys.addVelocity (9, 90);
			crouching = false;
			Debug.Log (crouching);
		}
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//arthurObject.scale
			Debug.Log ("shootWeapon");
			GameObject weaponObj = Instantiate (WeaponPrefab) as GameObject;
			Weapon weaponComp = weaponObj.GetComponent<Weapon>();
			weaponComp.weapon = this.weapon;
			weaponComp.sides = this.sides;
			weaponObj.transform.position = new Vector2 (transform.position.x+sides, transform.position.y + 0.6f); 
		}




	}

	void OnCollisionEnter(Collision coll){
		//Find out what hit this basket
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Enemy") {
			health--;
			if (health == 0) {
				Destroy (this.gameObject);
			}

			//movement and invincibility

			//change state
		}
	}
}

