using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Arthur.lives++;
			Respawner.checkpointNum = -1;
			Respawner.checkpoint = Respawner.customStart;
			print(GameOver.theStage);
			Application.LoadLevel ("mainMenu");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
