using UnityEngine;
using System.Collections;

public class RedArremer : MonoBehaviour {

	public float speed;
	public float speedFlight;
	public float leftAndRightEdge;
	public Arthur thisArthur;
	public float distanceFromArthur;
	public float chanceToChangeDirection;
	public float chanceToFly;
	public float ground;

	float maxHeight = 6f;
	int timer1 = 0;
	int timer2 = 0;
	int timeToCharge = 100;
	int timeToShoot = 80;

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
		speedFlight = 0.1f;
		leftAndRightEdge =13f;
		chanceToChangeDirection = 0.05f;
		ground = transform.position.y;
		thisArthur = GetComponent<Arthur>();
	}

	void Move() {
		if (awaken && grounded) {
			Vector3 pos = transform.position;
			pos.x += speed;
			transform.position = pos;
			print (transform.position);
			//Changing Direction
			if (pos.x < -leftAndRightEdge) {
				speed = Mathf.Abs (speed); // move right
			} else if (pos.x > leftAndRightEdge) {
				speed = -Mathf.Abs (speed); //move left
			}
		}
	}

	void Ascend() {
		//Goes to the top
		if (awaken && hover) {
			if (transform.position.y < maxHeight) {// If you are not in max speed
				dodge = false;
				Vector3 pos = transform.position;
				pos.y += speedFlight;
				transform.position = pos;
				print (transform.position);
			}
			else {
				dodge = true;
			}
		}
	}
	
	void Dodge() {
		if (awaken && hover && dodge) {
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

	void Charge() {
		if (awaken && hover) {
			dodge = false;
		}
	}

	void Shoot() {
		if (awaken) {

		}
	}

	// Update is called once per frame
	void Update () {
		timer1++;
		timer2++;
		ground = Arthur.arthurPos.y; //Red Arremer "lands" where arthur is on the y plane
		Move (); //If grounded
		Ascend (); //If hovering
		Dodge (); //If hovering and dodging

		//Charge motion
		if (timer1 == timeToCharge) {
			Charge (); //If timer is up and hovering
			timer1 = 0;
		}
		else {
			timer1 = 0;
		}


		//Shooting
		if (timer2 == timeToShoot) {
			Shoot (); //If timer is up
			timer2 = 0;
		}
		else {
			timer2 = 0;
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
