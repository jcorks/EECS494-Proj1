using UnityEngine;
using System.Collections;

public class Unicorn : MonoBehaviour {

	const float EXPIRED = -9999f;
	public GameObject FireballPrefab;

	public Sprite Stand;
	public Sprite MoveL;
	public Sprite MoveR;
	public Sprite Shoot;
	public Sprite Jump;


	Enemy thisE;
	PhysObj thisPhys;

	float initWaitTime 		= 3f;
	float chanceToGoFast 	= .005f;
	float chanceToChangeDir = .013f;
	float chanceToJumpOnDmg = .35f;
	float chanceToFire      = .01f;
	float fastDuration 		= 4f; // durtion for fastness in seconds
	float shotDuration   	= .6f;
	float shotSpeed         = 4f;
	Vector3 shotOrigin		= new Vector3(0, .1f, 0f);


	float timeSinceStarted	= 0f;
	float timeFastLeft      = EXPIRED;
	float timeShotLeft      = EXPIRED;

	float speed				= 2f;
	float fastSpeed 		= 6f;
	float jumpVelocity		= 15f;

	bool started = false;
	float oldSpeed;


	// Use this for initialization
	void Start () {
		thisE = GetComponent<Enemy> ();
		thisPhys = GetComponent<PhysObj> ();
	}
	
	// Update is called once per frame
	void Update () {

		thisE.ready = GetComponentInChildren<SpriteRenderer>().isVisible;
		initWait ();

		timeSinceStarted += Time.deltaTime;
		timeFastLeft -= Time.deltaTime;
		timeShotLeft -= Time.deltaTime;

		if (!thisE.ready || !started || timeShotLeft > 0)
						return;




		// reset speed if fastness expired
		if (timeFastLeft < 0 && timeFastLeft > EXPIRED+1) {
			thisPhys.setVelocity(
				new Vector2(speed, thisPhys.getVelocity().y)
			);
			timeFastLeft = EXPIRED - 10;
			print ("SPEED EXPIRED :(");
		} 

		// reset fire shot
		if (timeShotLeft < 0 && timeShotLeft > EXPIRED+1) {
			thisPhys.setVelocity(
				new Vector2(oldSpeed, thisPhys.getVelocity().y)
			);
			print ("Can act again");
			timeShotLeft = EXPIRED - 10;
			thisPhys.setVelocity (new Vector2 (oldSpeed, 0));
		}


		if (thisPhys.isGrounded && Random.value <= chanceToFire && timeFastLeft < 0) {
			print ("FIRE!!!!");
			GetComponentInChildren<SpriteRenderer>().sprite = Shoot;
			fireProjectile();
		}

		// recover speed from jump if not firing
		if (Mathf.Abs (thisPhys.getVelocity ().x) < .001 && thisPhys.isGrounded
		    && timeShotLeft < 0) {
			thisPhys.setVelocity(
				new Vector2(-speed, thisPhys.getVelocity().y)
			);
		}



	}

	void  fireProjectile() {
		oldSpeed = thisPhys.getVelocity ().x;
		thisPhys.setVelocity (new Vector2(0, 0));
		timeShotLeft = shotDuration;

		// instantiate fireball
		GameObject fb = (GameObject)Instantiate (FireballPrefab);

		float mult = 1;
		if (Arthur.arthurPos.x < transform.position.x) mult = -1f;
		fb.GetComponent<PhysObj> ().setVelocity (new Vector2 (mult * shotSpeed, 0));
		fb.transform.position = transform.position + shotOrigin;


	}

	// initial waiting time that unicorn does when first encountering Arthur
	void initWait() {
		if (thisE.ready && !started) {
			if (timeSinceStarted >initWaitTime) {
				started = true;
				print ("READY!!!!!!!!!");
				thisPhys.setVelocity(
					new Vector2(-speed, thisPhys.getVelocity().y)
				);

			}
		}

	}


	void FixedUpdate() {
		if (!started || timeShotLeft > 0)
			return;

		if (Random.value <= chanceToChangeDir) {
			thisPhys.setVelocity(
				new Vector2(-thisPhys.getVelocity().x, thisPhys.getVelocity().y)
			);
		}

		// Go fast 
		if (Random.value <= chanceToGoFast) {
			timeFastLeft = fastDuration;
			thisPhys.setVelocity(
				new Vector2(fastSpeed, thisPhys.getVelocity().y)
			);
			print ("SPEEDBOOST!");
		}


		// Go toward player if offscreen 
		if (!thisE.ready) {
			float mult = -1;
			if (transform.position.x < Arthur.arthurPos.x)
				mult = 1f;
			thisPhys.setVelocity(
				new Vector2(Mathf.Abs(thisPhys.getVelocity().x) * mult, thisPhys.getVelocity().y)
			);
		}



	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon") {
			if (Random.value <= chanceToJumpOnDmg) {
				thisPhys.setVelocity (new Vector2(0, jumpVelocity));
			}
		}
	}

	void OnDestroy() {
		Manager.beatLevel = true;
	}


}
