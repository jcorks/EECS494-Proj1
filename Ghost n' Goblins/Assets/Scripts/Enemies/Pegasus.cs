using UnityEngine;
using System.Collections;

public class Pegasus : MonoBehaviour {
	
	const float EXPIRED = -9999f;
	public GameObject FireballPrefab;
	public GameObject rockPrefab;
	public float ArenaXMin;
	public float ArenaXMax;
	public GameObject platform;
	public GameObject movingPlatform;
	public GameObject item;
	
	Enemy thisE;
	PhysObj thisPhys;
	SpriteRenderer spr;
	public Sprite originalSpr;
	public Sprite invertedSpr;

	
	float initWaitTime 		= 3f;
	float chanceToGoFast 	= .006f;
	float chanceToChangeDir = .03f;
	float chanceToFire      = .004f;
	float fastDuration 		= 4f; // durtion for fastness in seconds
	float shotDuration   	= 1f;
	float freezeDuration    = .6f;
	float shotSpeed         = 6f;
	float minimumFireTime   = 3.5f; 
	int   numRocks          = 7;
	Vector3 shotOrigin		= new Vector3(0, .4f, 0f);
	bool dramaticWait = true;
	
	
	float timeSinceStarted	= 0f;
	float timeFastLeft      = EXPIRED;
	float timeShotLeft      = EXPIRED;
	float timeShotSince     = 6f;
	float timeFrozen        = -1f;
	Vector2 frozenVel		= new Vector2(0, 0);
	int   rocksLaunched     = 0;
	float chanceToRock		= .2f;
	
	float speed				= 2f;
	float fastSpeed 		= 3f;
	float jumpVelocity		= 24f;
	bool isDoingBigJump = false;
	bool shotsFired = false;


	
	bool started = false;
	float oldSpeed;
	

	// Use this for initialization

	void Awake() {
		dramaticWait = true;
		FinalBossManager.defeated = false;
	}
	void Start () {
		spr =  GetComponentInChildren<SpriteRenderer>();
		thisE = GetComponent<Enemy> ();
		thisPhys = GetComponent<PhysObj> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (dramaticWait) {
			if (Arthur.arthurPos.y > transform.position.y - .1f) {
				dramaticWait = false;
			}
			return;
		}

		if (timeFrozen > 0) {
			thisPhys.setVelocity(new Vector2(0,thisPhys.getVelocity().y));
			thisE.ignoreProjectiles = true;
			if (spr.sprite == originalSpr) {
				spr.sprite = invertedSpr;
			} else {
				spr.sprite = originalSpr;
			}
			timeFrozen-=Time.deltaTime;
			if (	timeFrozen < 0) {
				thisE.ignoreProjectiles = false;
				spr.sprite = originalSpr;

			}
		}

		if (isDoingBigJump) {
			if (thisPhys.getVelocity().y < 0) {
				isDoingBigJump = false;
				thisPhys.ignoreGround = false;
			}
		}
		
		thisE.ready = (GetComponent<MeshRenderer>().isVisible || started) && !isDoingBigJump;
		initWait ();
		
		timeSinceStarted += Time.deltaTime;
		timeFastLeft -= Time.deltaTime;
		timeShotLeft -= Time.deltaTime;
		timeShotSince -= Time.deltaTime;

		
		if (!thisE.ready || !started || (timeShotLeft > 0) || isDoingBigJump)
			return;



		if (thisPhys.isGrounded && (Random.value <= chanceToFire || timeShotSince <0)) {
			print ("FIRE!!!!");
			fireProjectile();
			timeShotSince=minimumFireTime;
			return;
		}



		if (timeFrozen > 0) {
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


		if (thisE.health < 10 && (Random.value < .3)) {
			GameObject fb = (GameObject)Instantiate (FireballPrefab);
			shotsFired = true;


			fb.GetComponent<PhysObj> ().setVelocity (new Vector2 (getDir () * shotSpeed, 0));
			fb.transform.position = transform.position + shotOrigin;
		}

		
	}
	
	// initial waiting time that unicorn does when first encountering Arthur
	void initWait() {
		if (GetComponent<MeshRenderer> ().isVisible && !GetComponent<AudioSource> ().isPlaying) {
			GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Stop();
			GetComponent<AudioSource>().Play();
		}
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
		if (!started || isDoingBigJump || dramaticWait)
			return;

		if (timeShotLeft > 0 && !shotsFired) {
			if (!thisPhys.isGrounded) return;
			Manager.shakeCamera (.1f);
			if (Random.value < chanceToRock && rocksLaunched++ < numRocks) {
				GameObject n = (GameObject)Instantiate(rockPrefab);
				n.transform.position = new Vector3(Arthur.arthurPos.x + Random.value*12 - 6, Arthur.arthurPos.y+6f	, 0);
			}
			return;
		} else if (timeShotLeft < 0 && shotsFired) {shotsFired = false;}



		if (Random.value <= chanceToChangeDir) {
			if (Random.value < .4) {
				thisPhys.setVelocity(
					new Vector2(getDir ()*Mathf.Abs (thisPhys.getVelocity().x), thisPhys.getVelocity().y)
					);
			} else {
				thisPhys.setVelocity(
					new Vector2((Random.value<.5f?-1:1)*Mathf.Abs (thisPhys.getVelocity().x), thisPhys.getVelocity().y)
					);
			}
		}

		// this takes priority
		if (!GetComponent<MeshRenderer>().isVisible) {
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
		if (other.tag == "Weapon" && (timeFrozen <= 0) && !isDoingBigJump) {
			timeFrozen = freezeDuration;

			if (thisE.health % 10 == 0 && thisE.health < 30) {
				print ("Health Left:" + thisE.health.ToString());
				thisPhys.setVelocity (new Vector2(0, jumpVelocity));
				isDoingBigJump = true;
				thisPhys.ignoreGround = true;
			} else {
				if (thisPhys.isGrounded && timeShotLeft < 0) {
					thisPhys.setVelocity (new Vector2(0, 7));

					frozenVel = thisPhys.getVelocity ();	
				}
			}

			if (thisE.health == 20) {
				chanceToFire+= .01f;
				GameObject i = (GameObject) Instantiate (item); 
				i.GetComponent<Items>().item = ItemType.ARMOR;
				i.transform.position = FinalBossManager.basePos + new Vector3(0, 3f, 0);
				GameObject n = (GameObject) Instantiate (platform);
				n.transform.position = FinalBossManager.basePos + new Vector3(0, 4.5f, 0);
				n = (GameObject) Instantiate (platform);
				n.transform.position = FinalBossManager.basePos + new Vector3(0, 6.5f, 0);
				
				
			}
			
			if (thisE.health == 10) {
				
				
				GameObject i = (GameObject) Instantiate (item); 
				i.GetComponent<Items>().item = ItemType.ARMOR;
				i.transform.position = FinalBossManager.basePos + new Vector3(-2.04f, 10f, 0);
				GameObject n = (GameObject) Instantiate (movingPlatform);
				n.transform.position = FinalBossManager.basePos + new Vector3(-2.04f, 11.51f, 0);
				n = (GameObject) Instantiate (movingPlatform);
				n.transform.position = FinalBossManager.basePos + new Vector3(2.04f, 13.75f, 0);
				n.GetComponent<MovingPlatform>().speed = 1.5f;
			}
			
			// ROCKIN MODE
			if (thisE.health == 7) {
				numRocks = 8;
				chanceToFire = .06f;
				shotDuration = .8f;
				chanceToRock = 1f;
			}



		}
	}
	
	void OnDestroy() {

		FinalBossManager.defeated = true;
	}

	float getDir() {
				return (Arthur.arthurPos.x < transform.position.x ? -1f:1f);
	}
	
	
}
