using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public static float weaponDistance = 20f;
	public Arthur thisArthur;
	public PhysObj thisPhys;
	public float sides;
	public int weapon;
	private float weaponSpeed = 9f;

	// Use this for initialization
	void Start () {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		Debug.Log (weaponSpeed*sides);
		Debug.Log (weapon);
		if (weapon == 0) {
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 0f));
		}
		if (weapon == 2) {
			thisPhys.ignoreGravity = false;
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 2f));
		}
		//thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 4f));
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hostile") {
			thisArthur.weaponCount--;
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		//Keeps track if weapon is offscreen;
		if (transform.position.x > weaponDistance && sides == 1) {
			Debug.Log ("weapon gone ");
			thisArthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (transform.position.x < -weaponDistance && sides == -1) {
			Debug.Log ("weapon gone ");
			thisArthur.weaponCount--;
			Destroy (this.gameObject);	
		}
	}
}
