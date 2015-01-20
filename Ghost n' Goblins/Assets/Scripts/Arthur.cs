using UnityEngine;
using System.Collections;


//Trying to work on a raycasting solution for collision based upon
//http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html

public class Arthur : MonoBehaviour {

	// Use this for initialization
	public static Vector3 arthurPos;

	public GameObject WeaponPrefab;

	public int health;
	public int weapon;
	public float sides;
	public int weaponCount;

	private PhysObj thisPhys;
	private GameObject arthurObject;
	private bool crouching;
	private bool jumping = false;
	private bool isHit = false;
	private float speed = 3f;
	private float jumpVel = 9f;
	private int weaponLimit = 2; //amount of weapon permitted on screen
	private float isHitTimer = 0;
	private bool isHitOnGround = false; // When hit, it does a special jump that does not show the invincibility amount
	private bool invincibleVisual;
	
	void Start() {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		arthurObject = this.gameObject;
		health = 2;
		crouching = false;
		weapon = 0;
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
			if (!isHitOnGround) {
				isHitOnGround = true;
				print ("Grounded but hit");
			}
			jumping = false;
		}

		if (!jumping)
			thisPhys.setVelocity (new Vector2(0f, thisPhys.getVelocity().y));



		if (Input.GetKey(KeyCode.LeftArrow) && !crouching && thisPhys.isGrounded) 
		{
			sides = -1f;
			thisPhys.addVelocity(-speed, 0f);
			Debug.Log ("movingleft");
		}
		if (Input.GetKey(KeyCode.RightArrow) && !crouching && thisPhys.isGrounded)
		{
			sides = 1f;
			thisPhys.addVelocity(speed, 0f);
			Debug.Log ("movingRight");
		}	
		if (Input.GetKey(KeyCode.DownArrow) && !jumping && thisPhys.isGrounded)
		{
			crouching = true;
			Debug.Log (crouching);
		}
		if (!jumping && Input.GetKeyDown(KeyCode.UpArrow))
		{
			jumping = true;
			thisPhys.addVelocity (jumpVel, 90);
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
			weaponObj.transform.position = new Vector2 (transform.position.x+sides, transform.position.y + 0.6f); 
		}
		/*if (crouching) {
			BoxCollider.transform.localScale.y = 0.5;
			BoxCollider.center = new Vector3(0, -0.25f, 0);
		}
		else {
			BoxCollider.transform.localScale.y = 1;
			BoxCollider.center = new Vector3(0, 0, 0);	
		}*/



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
			takeHit();

			//movement and invincibility

			//change state
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
			Destroy (this.gameObject);
		}
	}
}

