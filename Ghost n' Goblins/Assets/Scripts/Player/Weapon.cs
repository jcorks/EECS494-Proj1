using UnityEngine;
using System.Collections;

public enum WeaponType {
	LANCE,
	KNIFE,
	FIREBALL,
	PROJECTILE
};

public class Weapon : MonoBehaviour {
	public static float weaponDistance = 10f;
	public Arthur thisArthur;
	public PhysObj thisPhys;
	public float sides;
	public WeaponType weapon;
	private float weaponSpeed = 12f;
	private bool burning = false;
	private float burnCount = 0f;
	private Vector3 arthurLastPos;

	// Use this for initialization
	void Start () {
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		Debug.Log (weaponSpeed*sides);
		Debug.Log (weapon);
		arthurLastPos = thisArthur.transform.position;
		Debug.Log (arthurLastPos);
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
		if (other.tag == "Hostile" && other.GetComponent<Enemy>().ready && !other.GetComponent<Enemy>().ignoreProjectiles) {
			print ("hit!");
			int score = int.Parse (thisArthur.scoreGT.text);
			score += other.GetComponent<Enemy>().score;
			thisArthur.scoreGT.text = score.ToString ();
			if (!burning) {
				thisArthur.weaponCount--;
				Destroy (this.gameObject);
			}
		}
		if (other.tag == "Wall") {
			thisArthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (other.tag == "Ground" && weapon == WeaponType.FIREBALL) {
			Debug.Log("fireballHit");
			thisPhys.setVelocity (new Vector2 (0f, 0f));
			burning = true;
			burnCount = Time.time;
			Debug.Log(burnCount);
		}
	}

	// Update is called once per frame
	void Update () {
		//Keeps track if weapon is offscreen;
		if (transform.position.x > arthurLastPos.x+weaponDistance && sides == 1) {
			//Debug.Log (arthurLastPos.x + '>' + transform.position.x+weaponDistance);
			thisArthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (transform.position.x < arthurLastPos.x-weaponDistance && sides == -1) {
			//Debug.Log ("weapon gone ");
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

