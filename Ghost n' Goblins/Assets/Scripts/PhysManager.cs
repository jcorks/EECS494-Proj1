using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Object manager that resolves collisions */
public class PhysManager : MonoBehaviour {
	
	private static List<PhysObj> objs;
	private static Vector2 accConstant = new Vector2(0.0f, -.8f);


	/* Public interface */

	// Adds an instance to manage and resolve collisions for
	public static void register(PhysObj phys) {
		objs.Add (phys);
	}


	void Awake() {
		objs = new List<PhysObj> ();
	}

	// Update is called once per frame
	void Update () {
		foreach (PhysObj co in objs) {
			if (!co.isActive ()) continue;
			co.setLastPos (co.transform.position);

			// Add gravity or (other constant) to the obj's velocity 

			co.setVelocity(co.getVelocity() + accConstant);
			Vector2 newPos = co.getLastPos () + (co.getVelocity ()*Time.fixedDeltaTime) * (1.0f - co.getFriction ());

			co.transform.position = newPos;
		}
	}
}
