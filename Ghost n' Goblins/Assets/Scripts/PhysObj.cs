using UnityEngine;
using System.Collections;

/* A PhysObject grants physics to a GameObject */

/* For the physics to work properly, the object needs:
 * - A Rigidbody with kinetic set to true
 * - a Collider with Is Trigger enabled
 */

public class PhysObj : MonoBehaviour {


	public bool ActiveObject;
	public Vector2 InitialVelocity;

	private Vector2 vel			= new Vector2(0.0f, 0.0f);
	private Vector2 lastPos; // previous position;
	private float friction		= 0.0f; 
	private bool active 		= true;
	public  bool isGround 		= false;

	public bool isGrounded = false;
	public bool isObstacle = false;
	public bool ignoreGravity = false;


	/* Public Interface */

	//. Velocity Management

	// Adds a velocity component given a velocity amount and degree direction
	public void addVelocity(float velocity, float degrees) {
		vel.x += velocity * Mathf.Cos (degrees * (Mathf.PI / 180.0f));
		vel.y += velocity * Mathf.Sin (degrees * (Mathf.PI / 180.0f));
	}

	// Adds a velocity component
	public void addVelocity(Vector2 newVel) {
		vel += newVel;
	}

	// Explicitly sets the velocity to the specified value
	public void setVelocity(Vector2 newVel) {
		vel = newVel;
	}

	// Returns the current Velocity
	public Vector2 getVelocity() {
		return vel;
	}

	// Sets a friction value. Friction reduces velocity every step by a percentage
	// thus, A friction value will be between 0.0f and 1.0f inclusive
	public void setFriction(float fr) {
		friction = fr;
	}
	public float getFriction() {
		return friction;
	}


	// Returns whether or not the object is falling
	public bool isFalling() {
		return vel.y < 0;
	}


	//. Positional handling

	// Returns the last position
	public Vector2 getLastPos() {
		return lastPos;
	}

	public void setLastPos(Vector2 v) {
		lastPos = v;
	}


	//. Inactive
	public void setActive(bool b) {
		active = b;
	}

	public bool isActive() {
		return active;
	}

	//. Collision 
	void OnTriggerEnter(Collider other) {

		PhysObj otherPhys = other.gameObject.GetComponent<PhysObj>();
		if (otherPhys)
			resolveCollision (otherPhys, true);
	}

	void OnTriggerStay(Collider other) {
		PhysObj otherPhys = other.gameObject.GetComponent<PhysObj>();
		if (otherPhys)
			resolveCollision (otherPhys, false);
	}



	private void resolveCollision(PhysObj other, bool enter) {

		if (!isActive()) return;

		if (other.isGround) {

			if (vel.y < 0) {
				//transform.position = getLastPos ();
				transform.position = new Vector3(
					transform.position.x, 
					other.transform.position.y + GetComponent<BoxCollider>().collider.bounds.extents.y,
					transform.position.z);

				// nullify y component
				setVelocity (new Vector2 (vel.x, 0.0f));
			}
			isGrounded = true;
		}

		if (other.isObstacle && enter) {
			print ("i hit a wall");
			//Debug.Log(other.gameObject);
			setVelocity (new Vector2(0.0f, vel.y)); 
		}

	}



	// Use this for initialization
	void Awake () {
		active = ActiveObject;
		vel = InitialVelocity;

			
	}

	void Start() {
		PhysManager.register (this);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LateUpdate() {

	}
}
