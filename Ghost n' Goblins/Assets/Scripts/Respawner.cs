using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {
	public GameObject arthur;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Arthur.isDead) { 
			GameObject inst = (GameObject)Instantiate (arthur);
			inst.transform.position = new Vector3(0, 4, 0);
		}
	}
}
