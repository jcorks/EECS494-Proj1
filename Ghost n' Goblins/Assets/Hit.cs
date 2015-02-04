using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	float timer = 0;
	float duration = 0.5f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer > duration) {
			Destroy (this.gameObject);
			timer = 0;
		}
	}
}
