﻿using UnityEngine;
using System.Collections;

public class Leek : MonoBehaviour {

	public bool weaponDirection;
	public int side;
	public float speed = 1.5f;

	
	// Use this for initialization
	void Start () {
		if (weaponDirection) {
			var rotationVector = transform.rotation.eulerAngles;
			rotationVector.z = 90f;
			transform.rotation = Quaternion.Euler(rotationVector);
			Debug.Log(speed);
			GetComponent<PhysObj> ().addVelocity (-speed, 90f);	
		}
		else {
			Debug.Log(side);
			Vector2 dirVec = new Vector2(speed*side, 0f); 
			GetComponent<PhysObj> ().addVelocity (dirVec);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, Arthur.arthurPos) > 10)
			Destroy (this.gameObject);
	}
}