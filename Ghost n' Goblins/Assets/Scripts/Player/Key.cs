using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Respawner.checkpointNum = -1;
			Respawner.checkpoint = Respawner.customStart;
			Application.LoadLevel ("gameOver");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
