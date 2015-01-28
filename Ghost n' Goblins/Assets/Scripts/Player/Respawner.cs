using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {
	public GameObject arthur;
	public static float checkpointX = -10f;
	float checkpoint1 = -10f;
	float checkpoint2 = 73f;
	float checkpoint3 = 9000f;
	float checkpoint4 = 9000f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


		if (!GameObject.FindGameObjectWithTag("Player")) { 
			GameObject inst = (GameObject)Instantiate (arthur);
			print ("Respawned at " + checkpointX.ToString ());
			inst.transform.position = new Vector3(checkpointX, 0.78f, 0);
		}

	}
	void FixedUpdate() {
		updateCheckpoint ();
	}

	void updateCheckpoint() {
		if (Arthur.arthurPos.x > checkpoint1) {
			checkpointX = checkpoint1;
		} 
		if (Arthur.arthurPos.x > checkpoint2) {
			checkpointX = checkpoint2;
		} 
		if (Arthur.arthurPos.x > checkpoint3) {
			checkpointX = checkpoint3;
		} 
		if (Arthur.arthurPos.x > checkpoint4) {
			checkpointX = checkpoint4;
		}

	}

	void Destroy() {
			
	}
}
