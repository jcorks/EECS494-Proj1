using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	PhysObj phys;
	public int health = 0;
	public int score = 0;
	public bool ready = false;
	public bool ignoreProjectiles = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && ready && !ignoreProjectiles) {
			health--;
			if (health < 1) {
				Destroy(this.gameObject);
			}
		}
		
		if (other.gameObject.GetComponent<Arthur> ()) {
			print ("Hello, arthur!");
		}
	}
}
