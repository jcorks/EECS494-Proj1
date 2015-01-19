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

	private PhysObj thisPhys;

	void Start() {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		//layerMask = LayerMask.NameToLayer ("normalCollisions");
	}

	void FixedUpdate () {
		/*
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			thisPhys.movement(-1);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			thisPhys.movement(1);
		}	
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
		}
	}
}

