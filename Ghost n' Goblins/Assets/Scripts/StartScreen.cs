using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartScreen : MonoBehaviour {
	public bool canContinue;


	// Use this for initialization
	void Start () {
		Arthur.lives = 3;
		if (Respawner.checkpointNum != -1 && !canContinue) {
			GameObject k = GameObject.FindGameObjectWithTag("KeyText");
			k.GetComponent<Text>().text += "\nPress 3 to CONTINUE!";
			canContinue=true;
		}
		Respawner.checkpointNum = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			GameOver.theStage = "groundScene";
			Respawner.checkpoint = Respawner.originalStart;
			Respawner.checkpointNum = -1;
			Application.LoadLevel("gameOver");
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Respawner.checkpoint = Respawner.customStart;
			GameOver.theStage = "custom";
			Respawner.checkpointNum = -1;
			Application.LoadLevel ("gameOver");
		}

		if (Input.GetKeyDown (KeyCode.Alpha3) && canContinue) {
			Application.LoadLevel ("gameOver");
		}

	}
}
