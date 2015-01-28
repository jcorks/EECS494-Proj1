using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
	const float viewWidth = 6.7f;
	public static Vector3 pos;
	Vector3 prev;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frames
	void Update () {

		/* control camera */
		GameObject[] l = GameObject.FindGameObjectsWithTag ("CameraBound");
		foreach (GameObject cb in l) {
			if (!cb.GetComponent<CameraAnchor>().active) continue;
			if (Arthur.arthurPos.x  - cb.transform.position.x < viewWidth) {
				Vector3 n = new Vector3(cb.transform.position.x + viewWidth,
				                        transform.position.y,
				                        transform.position.z);
				transform.position= n;
				return;
			}
		}

		GameObject l2 = GameObject.FindGameObjectWithTag ("CameraLimit");
		if (Arthur.arthurPos.x >  l2.transform.position.x - viewWidth) {
			Vector3 n = new Vector3(l2.transform.position.x - viewWidth,
			                        transform.position.y,
			                        transform.position.z);
			transform.position= n; return;
		}


		transform.position = new Vector3 (
			Arthur.arthurPos.x,
			transform.position.y,
			transform.position.z);
		prev = transform.position;
		pos = transform.position;
	}
}
