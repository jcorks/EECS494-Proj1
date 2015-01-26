using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {
	public GameObject arthur;
	float checkpointX;
	public float checkpoint1 = 9000f;
	public float checkpoint2 = 9000f;
	public float checkpoint3 = 9000f;
	public float checkpoint4 = 9000f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Arthur.isDead) { 
			GameObject inst = (GameObject)Instantiate (arthur);
			inst.transform.position = new Vector3(-10f, 0.78f, 0);
		}
	}
}
