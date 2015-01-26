﻿using UnityEngine;
using System.Collections;

public class FlyingKnight : MonoBehaviour {

	Vector3 pos;
	float sAmplitude = 3f;
	float speed = -2.2f;
	float sinInterval = 0;
	float yOffset = 0f;
	public float degOffset;



	// Use this for initialization
	void Start () {
		yOffset = transform.position.y;
		sinInterval = degOffset;
	}

	void FixedUpdate() {
		sinInterval += .08f;
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		pos.x += Time.deltaTime * speed;

		pos.y = sAmplitude * Mathf.Sin (sinInterval) + yOffset;
		transform.position = pos;	
	}
}