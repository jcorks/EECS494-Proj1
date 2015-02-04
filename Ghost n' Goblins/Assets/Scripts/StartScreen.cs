using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Arthur.lives = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			GameOver.theStage = "groundScene";
			Respawner.checkpoint = Respawner.originalStart;
			Application.LoadLevel("gameOver");
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Respawner.checkpoint = Respawner.customStart;
			GameOver.theStage = "custom";
			Application.LoadLevel ("gameOver");
		}

	}
}
