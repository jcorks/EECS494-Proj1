﻿using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Arthur.lives = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			GameOver.theStage = "groundScene";
			Application.LoadLevel("gameOver");
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {

		}

	}
}
