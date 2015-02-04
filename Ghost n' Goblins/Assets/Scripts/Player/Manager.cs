using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
	const float viewWidth = 6.7f;
	public static Vector3 pos;

	Vector3 prev;


	public GameObject keyP;

	public static bool isShuttingDown = false;
	public static bool beatLevel = false;
	public float followCameraOffset = 0f;
	public bool followArthurVert = false;
	bool instantiatedKey = false;
	bool following = false;
	float vertFollowTime = .6f;
	float currentVertTime = 0;




	// Use this for initialization
	void Start () {
		beatLevel = false;
	}
	
	// Update is called once per frames
	void Update () {

		if (beatLevel && !instantiatedKey) {
			GameObject key = (GameObject)Instantiate (keyP);
			key.transform.position = new Vector3 (153, 10, 0);
			instantiatedKey = true;
		}

		/* control camera */
		GameObject[] l = GameObject.FindGameObjectsWithTag ("CameraBound");
		foreach (GameObject cb in l) {
			if (!cb.GetComponent<CameraAnchor>().active || cb.GetComponent<CameraAnchor>().isVertical) continue;
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

	void FixedUpdate() {
		followVert ();

	}

	// follow arthur vertically
	void followVert() {
		if (!following && followArthurVert && !onSameSubdiv(Arthur.arthurPos.y+followCameraOffset, transform.position.y, .1f) 
			    && Arthur.arthurPhys && (!Arthur.jumping || Arthur.arthurPos.y+followCameraOffset < transform.position.y)) {
			following = true;
			currentVertTime = 0f;
		} else if (following) {

			currentVertTime += Time.deltaTime;
			Vector3 tr = transform.position;
			tr.y = ease(tr.y, Arthur.arthurPos.y + followCameraOffset, (currentVertTime / vertFollowTime));
			transform.position = tr;
				
			if (currentVertTime >= vertFollowTime) following = false;
		}
		prev = transform.position;
		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			transform.position.z);
		pos = transform.position;
	}

	void OnApplicationQuit() {
		isShuttingDown = true;
	}

	bool onSameSubdiv(float p1, float p2, float div) {
		return ((int)(p1 / div)) == ((int)(p2 / div));
	}

	float ease(float p0, float p1, float t) {
		return (p1 - p0) * (t/(t+1)) + p0;
	}
}
