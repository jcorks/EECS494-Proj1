﻿using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (
			Arthur.arthurPos.x,
			transform.position.y,
			transform.position.z);
	}
}
