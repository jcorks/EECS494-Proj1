using UnityEngine;
using System.Collections;

public class FinalBossManager : MonoBehaviour {

	static public Vector3 basePos;
	static public bool defeated = false;
	public GameObject rock;
	float alpha = 0f;

	// Use this for initialization
	void Start () {
		basePos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (defeated) {
			Manager.shakeCamera(.1f);
			if (Random.value < .1) {
				GameObject n = (GameObject)Instantiate(rock);
				n.transform.position = Arthur.arthurPos + new Vector3(Random.Range (-7, 7), 6, 0);
				n.GetComponent<Enemy>().ready = false;
			}
			alpha+=.002f;
			GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
		}

		if (alpha > 1.2f)
						Application.LoadLevel ("congrats");
	}

	void OnDestroy() {
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Hostile");
		foreach(GameObject o in arr) {
			Destroy(o.gameObject);
		}
	}
}
