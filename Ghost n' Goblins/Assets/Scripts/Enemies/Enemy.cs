using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	PhysObj phys;
	public int health = 0;
	public int score = 0;
	public bool ready = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && ready && other.GetComponent<Weapon>().count == 1) {
			print (other.GetComponent<Weapon>().count);
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
