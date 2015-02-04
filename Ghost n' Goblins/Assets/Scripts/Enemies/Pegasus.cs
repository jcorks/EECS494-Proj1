using UnityEngine;
using System.Collections;

public class Pegasus : MonoBehaviour {
	
	const float EXPIRED = -9999f;
	public GameObject FireballPrefab;
	public GameObject rockPrefab;
	public float ArenaXMin;
	public float ArenaXMax;
	public GameObject platform;
	
	Enemy thisE;
	PhysObj thisPhys;
	SpriteRenderer spr;

	
	float initWaitTime 		= 3f;
	float chanceToGoFast 	= .006f;
	float chanceToChangeDir = .002f;
	float chanceToFire      = .005f;
	float fastDuration 		= 4f; // durtion for fastness in seconds
	float shotDuration   	= 1f;
	float freezeDuration    = .1f;
	float shotSpeed         = 6f;
	float minimumFireTime   = 4.5f; 
	int   numRocks          = 7;
	Vector3 shotOrigin		= new Vector3(0, .1f, 0f);
	
	
	float timeSinceStarted	= 0f;
	float timeFastLeft      = EXPIRED;
	float timeShotLeft      = EXPIRED;
	float timeShotSince     = 6f;
	float timeFrozen        = 0f;
	Vector2 frozenVel		= new Vector2(0, 0);
	int   rocksLaunched     = 0;
	
	float speed				= 2f;
	float fastSpeed 		= 3f;
	float jumpVelocity		= 20f;


	
	bool started = false;
	float oldSpeed;
	

	// Use this for initialization
	void Start () {
		spr =  GetComponentInChildren<SpriteRenderer>();
		thisE = GetComponent<Enemy> ();
		thisPhys = GetComponent<PhysObj> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		thisE.ready = GetComponent<MeshRenderer>().isVisible || started;
		initWait ();
		
		timeSinceStarted += Time.deltaTime;
		timeFastLeft -= Time.deltaTime;
		timeShotLeft -= Time.deltaTime;


		
		if (!thisE.ready || !started || timeShotLeft > 0)
			return;


		timeShotSince -= Time.deltaTime;


		if (thisPhys.isGrounded && (Random.value <= chanceToFire || timeShotSince <0)) {
			print ("FIRE!!!!");
			fireProjectile();
			timeShotSince=minimumFireTime;
			return;
		}



		if (timeFrozen > 0) {
			thisPhys.setVelocity(new Vector2(0,0));
			timeFrozen-=Time.deltaTime;
			if (	timeFrozen < 0) {
				thisPhys.setVelocity (frozenVel);
			}
			return;

		}
		
		// reset speed if fastness expired
		if (timeFastLeft < 0 && timeFastLeft > EXPIRED+1) {
			thisPhys.setVelocity(
				new Vector2(speed, thisPhys.getVelocity().y)
				);
			timeFastLeft = EXPIRED - 10;
			//print ("SPEED EXPIRED :(");
		} 
		
		// reset fire shot
		if (timeShotLeft < 0 && timeShotLeft > EXPIRED+1) {
			thisPhys.setVelocity(
				new Vector2(oldSpeed, thisPhys.getVelocity().y)
				);
			//print ("Can act again");
			timeShotLeft = EXPIRED - 10;
			thisPhys.setVelocity (new Vector2 (oldSpeed, 0));
		}
		
		

		
		// recover speed from jump if not firing
		if (Mathf.Abs (thisPhys.getVelocity ().x) < .001 && thisPhys.isGrounded
		    && timeShotLeft < 0) {
			thisPhys.setVelocity(
				new Vector2(getDir()*speed, thisPhys.getVelocity().y)
				);
		}
		
		
		
	}
	
	void  fireProjectile() {
		oldSpeed = thisPhys.getVelocity ().x;
		thisPhys.setVelocity (new Vector2(0, 6));
		timeShotLeft = shotDuration;

		rocksLaunched = 0;


		if (thisE.health < 10) {
			GameObject fb = (GameObject)Instantiate (FireballPrefab);



			fb.GetComponent<PhysObj> ().setVelocity (new Vector2 (getDir () * shotSpeed, 0));
			fb.transform.position = transform.position + shotOrigin;
		}
		
	}
	
	// initial waiting time that unicorn does when first encountering Arthur
	void initWait() {
		if (thisE.ready && !started) {
			if (timeSinceStarted >initWaitTime) {
				started = true;
				//print ("READY!!!!!!!!!");
				thisPhys.setVelocity(
					new Vector2(getDir()*speed, thisPhys.getVelocity().y)
					);
				
			}
		}
		
	}
	
	
	void FixedUpdate() {
		if (!started)
			return;

		if (timeShotLeft > 0) {
			if (!thisPhys.isGrounded) return;
			Manager.shakeCamera (.1f);
			if (Random.value < .2 && rocksLaunched++ < numRocks) {
				GameObject n = (GameObject)Instantiate(rockPrefab);
				n.transform.position = new Vector3(Arthur.arthurPos.x + Random.value*12 - 6, Arthur.arthurPos.y+6f	, 0);
			}
			return;
		}
		
		if (Random.value <= chanceToChangeDir || !GetComponent<MeshRenderer>().isVisible) {
			thisPhys.setVelocity(
				new Vector2(getDir ()*Mathf.Abs (thisPhys.getVelocity().x), thisPhys.getVelocity().y)
				);
		}
		
		// GOTTA Go fast 
		if (Random.value <= chanceToGoFast) {
			timeFastLeft = fastDuration;
			thisPhys.setVelocity(

				new Vector2(getDir () *fastSpeed, thisPhys.getVelocity().y)
				);

			print (thisPhys.getVelocity().ToString());
		}
		

		
		
		
	}
	
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon") {
			if (thisE.health % 10 == 0 && thisE.health < 30) {
				thisPhys.setVelocity (new Vector2(0, jumpVelocity));
			} else {
				if (thisPhys.isGrounded && timeShotLeft < 0) {
					thisPhys.setVelocity (new Vector2(0, 10));
					timeFrozen = freezeDuration;
					frozenVel = thisPhys.getVelocity ();	
				}
			}



			if (thisE.health == 20) {
				GameObject n = (GameObject) Instantiate (platform);
				n.transform.position = FinalBossManager.basePos + new Vector3(0, 4.5f, 0);
				n = (GameObject) Instantiate (platform);
				n.transform.position = FinalBossManager.basePos + new Vector3(0, 6.5f, 0);

			}
		}
	}
	
	void OnDestroy() {
		Manager.beatLevel = true;
	}

	float getDir() {
				return (Arthur.arthurPos.x < transform.position.x ? -1f:1f);
	}
	
	
}
