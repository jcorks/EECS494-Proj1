using UnityEngine;
using System.Collections;

public enum WeaponType {
	LANCE,
	KNIFE,
	FIREBALL
};

public class Weapon : MonoBehaviour {
	public static float weaponDistance = 10f;
	public Arthur thisArthur;
	public PhysObj thisPhys;
	public float sides;
	public WeaponType weapon;
	private float weaponSpeed = 9f;
	private bool burning = false;
	private float burnCount = 0f;

	// Use this for initialization
	void Start () {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		Debug.Log (weaponSpeed*sides);
		Debug.Log (weapon);
		if (weapon == WeaponType.LANCE) {
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 0f));
		}
		if (weapon == WeaponType.KNIFE) {
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides * 1.5f, 0f));
		}
		if (weapon == WeaponType.FIREBALL) {
			thisPhys.ignoreGravity = false;
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 3f));
		}
		//thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 4f));
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hostile") {
			thisArthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (other.tag == "Wall") {
			thisArthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (other.tag == "Ground" && weapon == WeaponType.FIREBALL) {
			thisPhys.setVelocity (new Vector2 (0f, 0f));
			burning = true;
			burnCount = Time.time;
			Debug.Log(burnCount);
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

	void FixedUpdate() {
		if (burning == true) {
			if (burnCount != 0 && burnCount+2f < Time.time) {
				burnCount = 0;
				thisArthur.weaponCount--;
				Destroy (this.gameObject);
			}
		}
	}
}

