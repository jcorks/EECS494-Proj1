using UnityEngine;
using System.Collections;

public class RedArremer : MonoBehaviour {

	public float speed;
	public float leftAndRightEdge;
	public Arthur thisArhthur;
	public float distanceFromArthur;
	public float chanceToChangeDirection;
	public float ground;

	bool awaken;
	bool grounded;
	bool hover;


	// Use this for initialization
	void Start () {
		awaken = false;
		grounded = false;
		hover = false;
		GetComponent<Enemy>().score = 500;
		speed = 0.025f;
		leftAndRightEdge =13f;
		chanceToChangeDirection = 0.05f;
		ground = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
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

	void FixedUpdate() {
		if (Random.value < chanceToChangeDirection) {
			speed *= -1; //Change direction randomly
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && GetComponent<Enemy>().ready) {
			if (!awaken) {
				awaken = true;
				grounded = true;
				print ("awakened");
			}
			if (awaken && grounded);

		}
		
		if (other.gameObject.GetComponent<Arthur> ()) {
			print ("Hello, arthur!");
		}
	}
}
