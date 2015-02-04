using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
	float angle;

	// Use this for initialization
	void Start () {
		angle = Random.value*2f+4;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y < Arthur.arthurPos.y - 20)
						Destroy (this.gameObject);
		GetComponent<PhysObj>().addVelocity(.05f,-90f);
		transform.Rotate (new Vector3 (0, 0, angle));
	}
}
