using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {
	public GameObject arthur;
	public static Vector3 checkpoint = new Vector3(-10f, 0, 0);
	public static readonly Vector3 originalStart = new Vector3 (-10f, 0, 0);
	public static readonly Vector3 customStart = new Vector3 (-18f, 8f, 0);
	public static int checkpointNum = -1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


		if (!GameObject.FindGameObjectWithTag("Player")) { 
			GameObject inst = (GameObject)Instantiate (arthur);
			print ("Respawned at " + checkpoint.ToString ());
			inst.transform.position = checkpoint + new Vector3(0, 0.78f, 0);
		}

	}
	void FixedUpdate() {
	}



	void Destroy() {
			
	}
}
