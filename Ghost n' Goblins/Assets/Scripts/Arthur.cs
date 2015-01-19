using UnityEngine;
using System.Collections;


//Trying to work on a raycasting solution for collision based upon
//http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html

public class Arthur : MonoBehaviour {

	/*public float acceleration  = 4f;
	public float maxSpeed = 150f;
	public float gravity = 9.8f;
	public float maxfall = 200f;
	public float jump = 200f;

	int layerMask;

	Rect box;

	public Vector3 velocity;

	bool grounded = false;
	bool falling = false;

	int horizontalRays = 6;
	int verticalRays = 4;
	int margin = 2;*/
	
	// Use this for initialization

	public int health;

	private PhysObj thisPhys;
	private GameObject arthurObject;
	private bool crouching;
	private bool sides;


	void Start() {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		arthurObject = this.gameObject;
		health = 2;
		crouching = false;
	}

	void changeSide(bool sides) {
		if (sides) {
			Vector3 theScale = transform.localScale;
			theScale.z *= -1;
			transform.localScale = theScale;
		}
	}

<<<<<<< HEAD

	void Update () {

		if (Input.GetKeyDown(KeyCode.LeftArrow) && !crouching)
=======
	void FixedUpdate () {
		/*
		if (Input.GetKey(KeyCode.LeftArrow))
>>>>>>> origin/master
		{
			Debug.Log ("movingLeft");
		}
		if (Input.GetKey(KeyCode.RightArrow) && !crouching)
		{
			Debug.Log ("movingRight");
		}	
<<<<<<< HEAD
		if (Input.GetKey(KeyCode.DownArrow))
		{
			crouching = true;
			Debug.Log (crouching);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			crouching = false;
			Debug.Log (crouching);
		}
	}

	void OnCollisionEnter(Collision coll){
		//Find out what hit this basket
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Enemy") {
			health--;
			if (health == 0) {
				Destroy (Arthur);
			}

			//movement and invincibility

			//change state
=======
		*/
	}

	void Update() {
		if (Input.GetKey (KeyCode.LeftArrow)) {
				thisPhys.setVelocity (new Vector2 (-1, thisPhys.getVelocity ().y));


		} else {
			thisPhys.setVelocity (new Vector2 (0, thisPhys.getVelocity ().y));
		}




		if (Input.GetKey (KeyCode.RightArrow)) {
			thisPhys.setVelocity (new Vector2 (1, thisPhys.getVelocity ().y));

		} else {
			thisPhys.setVelocity (new Vector2 (0, thisPhys.getVelocity ().y));
>>>>>>> origin/master
		}
	}
}

