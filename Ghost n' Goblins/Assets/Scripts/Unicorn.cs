using UnityEngine;
using System.Collections;

public class Unicorn : MonoBehaviour {

	Enemy thisE;
	PhysObj thisPhys;

	float initWaitTime 		= 2f;
	float chanceToGoFast 	= .001f;
	float chanceToChangeDir = .01f;
	float chanceToJumpOnDmg = .2f;
	float fastDuration 		= 4f; // durtion for fastness in seconds
	int numShotsPerVoley 	= 2;
	float shotTime          = 1f;


	float timeSinceStarted	= 0f;
	float timeAtFast     = 0f;

	float speed				= 2f;
	float fastSpeed 		= 6f;
	float jumpVelocity		= 20f;

	bool started = false;


	// Use this for initialization
	void Start () {
		thisE = GetComponent<Enemy> ();
		thisPhys = GetComponent<PhysObj> ();
	}
	
	// Update is called once per frame
	void Update () {

		thisE.ready = GetComponent<MeshRenderer> ().isVisible;
		initWait ();
		if (!thisE.ready && !started)
						return;

		// reset speed if fastness expired
		if (timeAtFast + fastDuration < timeSinceStarted) {
			thisPhys.setVelocity(
				new Vector2(speed, thisPhys.getVelocity().y)
			);
			print ("SPEED EXPIRED :(");
		}

	}


	// initial waiting time that unicorn does when first encountering Arthur
	void initWait() {
		if (thisE.ready && !started) {
			timeSinceStarted += Time.deltaTime;
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
		if (Random.value <= chanceToChangeDir) {
			thisPhys.setVelocity(
				new Vector2(-thisPhys.getVelocity().x, thisPhys.getVelocity().y)
			);
		}

		// Go fast 
		if (Random.value <= chanceToGoFast) {
			timeAtFast = timeSinceStarted;
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
				thisPhys.addVelocity (jumpVelocity, 90);
			}
		}
	}
}
