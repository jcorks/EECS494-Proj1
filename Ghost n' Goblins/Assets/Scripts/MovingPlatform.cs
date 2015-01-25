using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float speed  = 1f;
	
	public float leftAndRightEdge = 2f;

	Vector3 origin;

	// Use this for initialization
	void Start () {
		origin = this.transform.position; 
		print (origin.y);
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		//Basic Movement
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
		//Changing Direction
		if (pos.x < -leftAndRightEdge + origin.x) {
			speed = Mathf.Abs (speed); // move right
		} else if (pos.x > leftAndRightEdge + origin.x) {
			speed = -Mathf.Abs (speed); //move left
		}
	}
}
