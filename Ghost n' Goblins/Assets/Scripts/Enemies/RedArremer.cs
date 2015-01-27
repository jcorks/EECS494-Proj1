using UnityEngine;
using System.Collections;

public class RedArremer : MonoBehaviour {

	public GameObject projPrefab;

	public float speed;
	public float speedFlight;
	public float leftAndRightEdge;
	public Arthur thisArthur;
	public float distanceFromArthur;
	public float chanceToChangeDirection;
	public float chanceToFly;
	public float ground;
	public float swoopStart;
	public float swoopDuration;

	public Vector3 flightRight;
	public Vector3 flightLeft;

	GameObject Cam;
	float maxHeight = 6f;
	float timer1 = 0;
	float timer2 = 0;
	float groundedTimer = 0;
	float floatingTimer = 0;
	float timeOnFloor = 3.0f;
	float timeToCharge = 2.0f;
	float timeToShoot = 5.0f;
	float timeFloating = 2.5f;

	bool awaken; //If it is sitting down stationary
	bool grounded; //If it start walking along the ground
	bool hover; //If it starts flying
	bool dodge; //If it is able to dodge
	bool swooping; //Starts swopping motion

	// Use this for initialization
	void Start () {
		awaken = false;
		grounded = false;
		hover = false;
		dodge = false;
		GetComponent<Enemy>().score = 500;
		speed = 0.025f;
		speedFlight = 10f;
		leftAndRightEdge =13f;
		chanceToChangeDirection = 0.05f;
		ground = transform.position.y;
		thisArthur = GetComponent<Arthur>();
		GameObject[] Cam1 = GameObject.FindGameObjectsWithTag ("MainCamera"); 
		Cam = Cam1[0];
		Vector3 camPos = Cam.transform.position;
		camPos.z = 0f;
		camPos.x += 6.54f;
		camPos.y = 3.22f;
		flightRight = camPos;
		camPos.x -= 6.54f * 2f;
		flightLeft = camPos;
		swoopStart = 0f;
		swoopDuration = 2f;

	}

	void Move() {
		if (awaken && grounded) {
			groundedTimer += Time.deltaTime;
			Vector3 pos = transform.position;
			pos.x += speed;
			transform.position = pos;
			//print (transform.position);
			//Changing Direction
			if (Arthur.arthurPos.x - 4f < transform.position.x) {
				speed = Mathf.Abs (speed); // move right
			} 
			else if (Arthur.arthurPos.x + 4f > transform.position.x) {
				speed = -Mathf.Abs (speed); // move right
			}
			if (groundedTimer > timeOnFloor) {
				grounded = false;
				hover = true;
			}
		}
		else if (!grounded && groundedTimer != 0){
			groundedTimer = 0;
		}
	}

	void Ascend() {
		//Goes to the top
		if (awaken && hover) {
			floatingTimer += Time.deltaTime;
			Vector2 non = new Vector2(0,0);
			if (transform.position.y < flightRight.y) {// If you are not in max speed
				//Debug.Log("WORKING!!!");
			//	Debug.Log(flightRight);
				//Debug.Log(transform.position);
				Vector2 dirVec = flightRight - transform.position; 
				//Debug.Log(dirVec);
				dirVec.Normalize ();
				GetComponent<PhysObj> ().setVelocity (dirVec * speedFlight);
			}
			else if (transform.position.y >= flightRight.y &&  GetComponent<PhysObj> ().getVelocity() !=non){
				GetComponent<PhysObj> ().setVelocity (non);
				dodge = true;
			}
		}
	}
	
	void Dodge() {
		if (awaken && grounded) {
			hover = true;
			grounded = false;
		}
		if (awaken && hover) {
			//Dodges shots 
			/*if (dodge && Input.GetKey(KeyCode.X) && thisArthur.weaponCount != 0) {
				Vector3 lastPath = transform.position;
				if (lastPath+1f < transform.position.y) {// go up
					Vector3 pos = transform.position;
					pos.y += speedFlight;
					transform.position = pos;
					print (transform.position);
				}
				if (lastPath+1f > transform.position.y)
			}*/
		}
	}

	void Charge(Vector3 start, Vector3 enemy, Vector3 dest) {
		Vector3 temp = start;
		temp.y = enemy.y - 1f;
		float u = (Time.time - swoopStart / swoopDuration);
		Vector3 p01 = (1 - u) * start + u * temp;
		Vector3 p12 = (1 - u) * temp + u * dest;
		Vector3 p012 = (1 - u) * p01 + u * p12;
		transform.position = p012;
		if (
	}

	void Shoot() {
		if (awaken) {
			GameObject proj = (GameObject)Instantiate (projPrefab);
			proj.transform.position = transform.position;
		}
	}

	void posUpdate() {
		Vector3 camPos = Cam.transform.position;
		camPos.z = 0f;
		camPos.x += 6.54f;
		camPos.y = 3.22f;
		flightRight = camPos;
		camPos.x -= 6.54f * 2f;
		flightLeft = camPos;
		Debug.Log (flightRight);
		Debug.Log (flightLeft);
	}

	// Update is called once per frame
	void Update () {
		posUpdate ();
		timer1+=Time.deltaTime;
		timer2+=Time.deltaTime;
		if (!awaken && Arthur.arthurPos.x > transform.position.x - 1.5f 
		    && Arthur.arthurPos.x  < transform.position.x + 1.5f) {
			awaken = true;
			grounded = true;
			dodge = true;
			print ("awakened");
		}
		ground = Arthur.arthurPos.y; //Red Arremer "lands" where arthur is on the y plane
		Move (); //If grounded
		Ascend (); //If hovering
		if (dodge && Input.GetKeyDown (KeyCode.X) && Arthur.weaponCount < 2) { 
			Dodge (); //If hovering and dodging
		}
		//Charge motion
		if (floatingTimer > timeFloating) {
			Debug.Log("BEGIN");
			dodge = false;
			Charge (flightRight, Arthur.arthurPos, flightLeft); //If timer is up and hovering
			floatingTimer = 0;
		}
		timer2 += Time.deltaTime;
		if (timeToShoot < timer2) {
			Shoot();
			timer2 = 0f;
		}
	}

	void FixedUpdate() {
		if (Random.value < chanceToChangeDirection && !hover) {
			speed *= -1; //Change direction randomly
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && GetComponent<Enemy>().ready) {
			if (!awaken) {
				awaken = true;
				grounded = true;
				dodge = true;
				print ("awakened");
			}
			else if (awaken && grounded){
				hover = true;
				dodge = true;
				grounded = false;
				print ("hovering");

			}

		}
		
		if (other.gameObject.GetComponent<Arthur> ()) {
			print ("Hello, arthur!");
		}
	}
}
