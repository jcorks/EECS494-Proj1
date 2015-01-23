using UnityEngine;
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
	private WeaponType priorWeapon;

	private bool crouching = false;
	private bool jumping = false;
	private bool isHit = false;
	private bool wall = false;
	private float speed = 2.2f;
	private float jumpVel = 10f;
	private int weaponLimit = 2; //amount of weapon permitted on screen
	private Color origColor;

	private float isHitTimer = 0;
	private bool isHitOnGround = false; // When hit, it does a special jump that does not show the invincibility amount
	private bool invincibleVisual;
	private char hitSide;
	private bool onLadder = false;
	private bool onLadderTop = false;
	private bool upLadder = false;
	private bool stepUp = false;

	private Vector3 crouchState1 = new Vector3(1f, 0.75f, 1f);
	private Vector3 crouchState2 = new Vector3(0f, -0.125f, 0f);
	private Vector3 standState1 = new Vector3(1f, 1f, 1f);
	private Vector3 standState2 = new Vector3(0f, 0f, 0f);
	private float verticalWeaponSpawn = 0.5f;
	private BoxCollider boxCollider;

	private bool isDying = false;


	void Awake() {
		isDead = false;
		origColor = GetComponent<MeshRenderer> ().material.color;
	}
	
	void Start() {

		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		boxCollider = this.gameObject.collider as BoxCollider;
		arthurObject = this.gameObject;
		health = 2;
		crouching = false;
		weapon = priorWeapon;
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
		if (upLadder) {
			climbUp ();
			return;
		}

		if (!jumping && Input.GetKey(KeyCode.Z) && !crouching)
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


		//If I am pressing up while on the latter
		if (Input.GetKey(KeyCode.UpArrow) && !crouching && thisPhys.isGrounded && !jumping && onLadder) 
		{
			Debug.Log("going up");
			thisPhys.isGrounded = false;
			upLadder = true;
			stepUp = true;
		}

		//If I press down while on top of the ladder
		if (Input.GetKey(KeyCode.DownArrow) && onLadderTop) 
		{
			onLadderTop = false;
			onLadder = true;
			Debug.Log("going up");
			thisPhys.isGrounded = false;
			upLadder = true;
			stepUp = true;
			Vector3 temp = transform.position;
			temp.y =  transform.position.y-1f;
			transform.position = temp;
		}

		
		if (Input.GetKey(KeyCode.LeftArrow) && !crouching && thisPhys.isGrounded && !jumping && hitSide != 'l') 
		{
			sides = -1f;
			thisPhys.addVelocity(-speed, 0f);
		}
		if (Input.GetKey(KeyCode.LeftArrow) && !crouching && jumping & !upLadder) 
		{
			sides = -1f;
		}

		if (Input.GetKey(KeyCode.RightArrow) && !crouching && thisPhys.isGrounded && !jumping && hitSide != 'r')
		{
			sides = 1f;
			thisPhys.addVelocity(speed, 0f);
		}	
		if (Input.GetKey(KeyCode.RightArrow) && !crouching && jumping && !upLadder) 
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

		if (Input.GetKeyDown (KeyCode.X) && weaponCount < weaponLimit) 
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
			GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255, 255);
			boxCollider.center = crouchState2;
			boxCollider.size = crouchState1;
		}
		else {
			GetComponent<MeshRenderer>().material.color = origColor;
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
			if (collidedWith.transform.position.x  < this.transform.position.x)
				hitSide = 'l';
			Debug.Log (hitSide);

		}
		if (collidedWith.tag == "Ladder") {
			Debug.Log ("near ladder");
			onLadder = true;
		}
		if (collidedWith.tag == "LadderTop") {
			Debug.Log ("near ladderTop");
			onLadderTop = true;
		}
	}

	void OnTriggerStay(Collider coll){
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Ladder" && upLadder && stepUp) {
			Debug.Log ("LadderOn");
			stepUp = false;
			transform.position = new Vector3(collidedWith.transform.position.x,
			   transform.position.y+0.1f,collidedWith.transform.position.z);
		}
		if (collidedWith.tag == "LadderTop" && upLadder && stepUp) {
			Debug.Log ("LadderOn");
			stepUp = false;
			transform.position = new Vector3(collidedWith.transform.position.x,
			                                 transform.position.y+0.1f,collidedWith.transform.position.z);
		}

		if (collidedWith.tag == "Hostile" && !isHit) {
			if (collidedWith.GetComponent<Enemy>().ready)
				takeHit();
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
			upLadder = false;
		}
	}

	void climbUp() {
		thisPhys.setVelocity (new Vector2(0f, 0.5f));
		if (Input.GetKey (KeyCode.UpArrow)) {
			Debug.Log ("up");
			thisPhys.addVelocity (speed, 90f);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			Debug.Log (thisPhys.isGrounded);
			if (thisPhys.isGrounded) {
				Debug.Log ("grounded");
				upLadder = false;
			}
			Debug.Log ("down");
			thisPhys.addVelocity (-speed, 90f);
		}

	}

	// take a hit
	void takeHit() {
		print ("Ouch!");
		isHit = true;
		isHitTimer = 3.0f;
		isHitOnGround = false;



		Vector3 hitVel = new Vector3 (-sides*speed, jumpVel, 0);
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
		priorWeapon = weapon;

	}
}

