using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WeaponType {
	LANCE,
	KNIFE,
	FIREBALL,
	XBOW
};

public class Weapon : MonoBehaviour {
	public static float weaponDistance = 10f;
	public Arthur thisArthur;
	public PhysObj thisPhys;
	public float sides;
	public WeaponType weapon;
	private float weaponSpeed = 10f;
	private bool burning = false;
	private float burnCount = 0f;
	private Vector3 arthurLastPos;
	public int count;

	public GameObject Sprite;
	public Sprite Lance;
	public Sprite Knife;
	public Sprite Fireball;
	public Sprite Xbow;


	public float angle;
	public Queue<Collider> HitQueue;
	Sprite Used;

	// Use this for initialization
	void Start () {
		HitQueue = new Queue<Collider> ();
		Sprite = GameObject.FindWithTag ("spriteWeapon");
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("spriteWeapon");
		GameObject closest = Sprite;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		Sprite = closest;
	
		count = 0;
		thisPhys = this.gameObject.GetComponent<PhysObj>(); 
		arthurLastPos = thisArthur.transform.position;
		Debug.Log (arthurLastPos);
		Vector3 angles = Sprite.transform.eulerAngles;
		angles.z = 0f;
		Sprite.transform.eulerAngles = angles;
		Sprite.transform.localScale = new Vector3(5f, 25f);
		if ( (sides == -1f && Sprite.transform.localScale.x > 0) || 
		    (sides == 1f && Sprite.transform.localScale.x < 0 )){
			Vector3 t = Sprite.transform.localScale;
			t.x *= -1f;
			Sprite.transform.localScale = t;
		}
		if (weapon == WeaponType.LANCE) {
			Sprite.GetComponent<SpriteRenderer> ().sprite = Lance;
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 0f));
		}
		if (weapon == WeaponType.KNIFE) {
			Sprite.GetComponent<SpriteRenderer> ().sprite = Knife;
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides * 1.5f, 0f));
		}
		if (weapon == WeaponType.FIREBALL) {
			thisPhys.ignoreGravity = false;
			Sprite.GetComponent<SpriteRenderer> ().sprite = Fireball;
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 3f));
		}
		if (weapon == WeaponType.XBOW) {
			Sprite.GetComponent<SpriteRenderer> ().sprite = Xbow;
			Sprite.transform.localScale = new Vector3(3.3f, 15f);
			if (angle != 0f) {
				Vector3 angle2 = transform.eulerAngles;
				angle2.z = 45f*sides;
				transform.eulerAngles = angle2;
			}
			thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, angle));
		}
		//thisPhys.setVelocity (new Vector2 (weaponSpeed * sides, 4f));
	}

	void OnTriggerEnter(Collider other) {
		/*if (other.tag == "Hostile" && other.GetComponent<Enemy>().ready && other &&
		    !other.GetComponent<Enemy>().ignoreProjectiles) {
			//Debug.Log (HitQueue.Count);
			/*if (HitQueue.Count != 0) {
				Debug.Log(HitQueue.Count);
				Collider hit = HitQueue.Dequeue();
				hit.GetComponent<Enemy>().dead = true;
				HitQueue.Clear ();
				if (!burning && weapon != WeaponType.XBOW) {
					Arthur.weaponCount--;
					Destroy (this.gameObject);
					count = 0;
				}
			}
			print (count);
			if (!burning && weapon != WeaponType.XBOW) {
				Arthur.weaponCount--;
				Destroy (this.gameObject);
				count = 0;
			}

		}*/
		if (other.tag == "Wall" && other.GetComponent<PhysObj>().isObstacle) {
			Arthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (other.tag == "Ground" && weapon == WeaponType.FIREBALL) {
			Debug.Log("fireballHit");
			thisPhys.setVelocity (new Vector2 (0f, 0f));
			thisPhys.ignoreGravity = true;
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
			Arthur.weaponCount--;
			Destroy (this.gameObject);
		}
		if (transform.position.x < arthurLastPos.x-weaponDistance && sides == -1) {
			//Debug.Log ("weapon gone ");
			Arthur.weaponCount--;
			Destroy (this.gameObject);	
		}
		if (HitQueue.Count != 0) {
			Debug.Log(HitQueue.Count);
			Collider hit = HitQueue.Dequeue();
			hit.GetComponent<Enemy>().dead = true;
			HitQueue.Clear ();
			if (!burning) {
				Arthur.weaponCount--;
				Destroy (this.gameObject);
				count = 0;
			}
		}
	}
	
	void FixedUpdate() {
		if (burning == true) {
			if (burnCount != 0 && burnCount+2f < Time.time) {
				burnCount = 0;
				Arthur.weaponCount--;
				Destroy (this.gameObject);
			}
		}
	}

	void LateUpdate() {
	}
}

