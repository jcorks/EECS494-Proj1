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
	float timeToShoot = 7.5f;
	float timeFloating = 2.5f;

	Vector3 Lock;
	Vector3 temp;
	Vector3 pastPos;
	
	bool awaken; //If it is sitting down stationary
	bool grounded; //If it start walking along the ground
	bool hover; //If it starts flying
	bool dodge; //If it is able to dodge
	bool swooping; //Starts swopping motion
	bool side; //Determines which side arthur's in
	bool top; //If it hovering at the top
	bool down; //If it is desending

	public Sprite sit;
	public Sprite flying;
	public Sprite walking;

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
		camPos.x += 5.54f;
		camPos.y = 6.22f;
		flightRight = camPos;
		camPos.x -= 5.54f * 2f;
		flightLeft = camPos;
		swoopStart = 0f;
		swoopDuration = 2.25f;
		pastPos = transform.position;
		down = false;

	}

	void Move() {
		if (awaken && grounded && !hover && !down) {
			groundedTimer += Time.deltaTime;
			Vector3 pos = transform.position;
			pos.x += speed;
			transform.position = pos;
			//print (transform.position);
			//Changing Direction
			/*if (Arthur.arthurPos.x - 4f < transform.position.x) {
				speed = Mathf.Abs (speed); // move right
			} 
			else if (Arthur.arthurPos.x + 4f > transform.position.x) {
				speed = -Mathf.Abs (speed); // move right
			}*/
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
		if (awaken && hover && !down) {
			floatingTimer += Time.deltaTime;
			Vector2 non = new Vector2(0,0);
			if (side) {
				if (transform.position.y < flightRight.y) {// If you are not in max speed
					top = false;
					Vector2 dirVec = flightRight - transform.position; 
					dirVec.Normalize ();
					GetComponent<PhysObj> ().setVelocity (dirVec * 8.0f);
				}
				else if (transform.position.y >= flightRight.y &&  GetComponent<PhysObj> ().getVelocity() !=non){
					GetComponent<PhysObj> ().setVelocity (non);
					top = true;
				}
			}
			else {
				if (transform.position.y < flightLeft.y) {// If you are not in max speed
					top = false;
					Vector2 dirVec = flightLeft - transform.position; 
					dirVec.Normalize ();
					GetComponent<PhysObj> ().setVelocity (dirVec * 8.0f);
				}
				else if (transform.position.y >= flightLeft.y &&  GetComponent<PhysObj> ().getVelocity() !=non){
					GetComponent<PhysObj> ().setVelocity (non);
					top = true;
				}
			}
		}
	}

	bool Decsend() {
		if (awaken && hover && down) {
			top = false;
			Vector3 t = transform.position;
			t.y = transform.position.y - 0.1f;
			transform.position = t;
		}
		if (transform.position.y < Lock.y+0.1f) {
			grounded = true;
			return false;
		}
		return true;
	}
	
	void Dodge() {
		if (awaken && grounded) {
			hover = true;
			down = false;
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


	bool Charge(Vector3 start, Vector3 enemy, Vector3 dest) {
			Vector3 temp = enemy;
			float u = ( (Time.time - swoopStart) / swoopDuration);
			//u = u % 1f;
			Vector3 p01 = (1 - u) * start + u * temp;
			Vector3 p12 = (1 - u) * temp + u * dest;
			Vector3 p012 = (1 - u) * p01 + u * p12;
			transform.position = p012;
			if (u > 1) {
				//Debug.Log("stop!");
				swoopStart = 0f;
				return false;
			}
		return true;
	}

	void Shoot() {
		if (awaken) {
			GameObject proj = (GameObject)Instantiate (projPrefab);
			proj.transform.position = transform.position;
		}
	}

	void posUpdate() {
		if (Arthur.arthurPos.x > transform.position.x && !swooping) {
			Vector3 temp = GetComponentInChildren<RectTransform>().localScale;
			temp.x = -5;
			GetComponentInChildren<RectTransform>().localScale = temp;
			side = false;
		}
		else if (Arthur.arthurPos.x < transform.position.x && !swooping) {
			Vector3 temp = GetComponentInChildren<RectTransform>().localScale;
			temp.x = 5;
			GetComponentInChildren<RectTransform>().localScale = temp;
			side = true;
		}

		//Shift flight coordinates
		Vector3 camPos = Cam.transform.position;
		camPos.z = 0f;
		camPos.x += 6.54f;
		camPos.y = 6.22f;
		flightRight = camPos;
		camPos.x -= 6.54f * 2f;
		flightLeft = camPos;


		if (hover && !swooping && top && !down) {
			//Debug.Log("No");
			Vector3 t = transform.position;
			if (side) {
				t.x = flightRight.x;
			}
			else {
				t.x = flightLeft.x;
			}
			transform.position = t;

		}
		//Shift red arremer

		pastPos = transform.position;
		//Debug.Log (flightRight);
		//Debug.Log (flightLeft);
	}

	// Update is called once per frame
	void Update () {
		posUpdate ();
		timer1+=Time.deltaTime;
		timer2+=Time.deltaTime;

		//Awaken if too close
		if (!awaken && Arthur.arthurPos.x > transform.position.x - 2f 
		    && Arthur.arthurPos.x  < transform.position.x + 2f) {
			awaken = true;
			grounded = true;
			dodge = true;
			print ("Too close");
		}

		if (grounded) 
			GetComponentInChildren<SpriteRenderer>().sprite = walking; 
		if (hover)
			GetComponentInChildren<SpriteRenderer>().sprite = flying; 
		
		ground = Arthur.arthurPos.y; //Red Arremer "lands" where arthur is on the y plane
		Move (); //If grounded
		Ascend (); //If hovering
		if (dodge && Input.GetKeyDown (KeyCode.X) && Arthur.weaponCount < 2) { 
			Dodge ();
		}
		//Set Charge motion
		if (floatingTimer > timeFloating) {
			float whatNow = Random.Range (0f, 1f);
			if (whatNow < 0.65f) {
				dodge = false;
				Lock = Arthur.arthurPos;
				swooping = true;
				swoopStart = Time.time;
				floatingTimer = 0;
				float height = Random.Range (4.5f, 5f);
				float side = Random.Range (-5f, 5f);
				temp.y = Lock.y - height;
				temp.x = Lock.x - side;
				temp.z = 0;
			}
			else {
				floatingTimer = 0;
				Lock = Arthur.arthurPos;
				down = true;
			}
		}

			//Charging
		if (swooping) {
			if (side) {
				if (!Charge (flightRight, temp, flightLeft)) {//If timer is up and hovering
					swooping = false;
					dodge = true;
					side = false;
				}
			}
			else {
				if (!Charge (flightLeft, Lock, flightRight)) {//If timer is up and hovering
					swooping = false;
					dodge = true;
					side = true;
				}
			}
		}

		if (down) {
			if (!Decsend()) {
				hover = false;
				dodge = true;
				down = false;
			}
		}
		
		//Shooting
		timer2 += Time.deltaTime;
		if (timeToShoot < timer2) {
			float t = Random.Range(0,1f);
			if (t < 0.5)
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
