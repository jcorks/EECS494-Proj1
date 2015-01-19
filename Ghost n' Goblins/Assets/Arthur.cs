using UnityEngine;
using System.Collections;


//Trying to work on a raycasting solution for collision based upon
//http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html

public class Arthur : MonoBehaviour {

	public float acceleration  = 4f;
	public float maxSpeed = 150f;
	public float gravity = 9.8f;
	public float maxfall = 200f;
	public float jump = 200f;
	public PhysObj phys;

	int layerMask;

	Rect box;

	public Vector3 velocity;

	bool grounded = false;
	bool falling = false;

	int horizontalRays = 6;
	int verticalRays = 4;
	int margin = 2;
	
	// Use this for initialization
	void Start() {
		layerMask = LayerMask.NameToLayer ("normalCollisions");
		phys = GetComponent<PhysObj> ();
	}

	void FixedUpdate(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			print ("I jumped");
			GetComponent<PhysObj>().addVelocity(new Vector2(0, 100));
		}

		/*
		box = new Rect (
			collider.bounds.min.x,
			collider.bounds.min.y,
			collider.bounds.size.x,
			collider.bounds.size.y
		);
		if (!grounded)
			velocity = new Vector2 (velocity.x, Mathf.Max (velocity.y = gravity, -maxfall));
		if (velocity.y < 0) {
			falling = true;
		}
		*/

	}
	void Update () {

		//transform.Translate (velocity * Time.deltaTime);
		//movement = Input.GetAxis ("Horizontal")
		if (Input.GetKey (KeyCode.LeftArrow)) {
			phys.setVelocity (new Vector2 (-1, phys.getVelocity ().y));
						/*
			Vector3 pos = transform.position;
			pos.x += -1 * velocity * Time.deltaTime;
			transform.position = pos;
			*/
		} else {
			phys.setVelocity (new Vector2(0, phys.getVelocity ().y));
		}




		if (Input.GetKey(KeyCode.RightArrow))
		{
			phys.setVelocity (new Vector2(1, phys.getVelocity ().y));
			/*
			Vector3 pos = transform.position;
			pos.x += 1 * velocity * Time.deltaTime;
			transform.position = pos;
			*/
		} else {
			phys.setVelocity (new Vector2(0, phys.getVelocity ().y));
		}
	}
}

