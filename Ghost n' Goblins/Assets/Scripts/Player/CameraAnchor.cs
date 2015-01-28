using UnityEngine;
using System.Collections;

public class CameraAnchor : MonoBehaviour {
	public bool active = false;
	float graceDist = 7.2f;
	PhysObj wall;

	// Use this for initialization
	void Start () {
		wall = GetComponentInChildren<PhysObj> ();
		wall.isObstacle = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Arthur.arthurPos.x - graceDist > transform.position.x)
						active = true;
		if (active) wall.isObstacle = true;
		
	}
}
