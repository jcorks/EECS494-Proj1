using UnityEngine;
using System.Collections;

public class RedArremer : MonoBehaviour {

	bool awaken;
	bool grounded;
	bool hover;

	// Use this for initialization
	void Start () {
		awaken = false;
		grounded = false;
		hover = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && GetComponent<Enemy>().ready) {
			if (!awaken) {
				awaken = true;
				grounded = true;
				print ("awakened");
			}

		}
		
		if (other.gameObject.GetComponent<Arthur> ()) {
			print ("Hello, arthur!");
		}
	}
}
