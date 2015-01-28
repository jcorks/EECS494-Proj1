using UnityEngine;
using System.Collections;

public class ItemUI : MonoBehaviour {

	SpriteRenderer spr;
	Vector3 offset = new Vector3 (0, -3.5f, 2);
	public Sprite knife, lance, fireball; 

	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		transform.position = Manager.pos + offset;
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (Arthur.weapon == WeaponType.FIREBALL) {
			spr.sprite = fireball;
		} else if (Arthur.weapon == WeaponType.KNIFE) {
			spr.sprite = knife;
		} else if (Arthur.weapon == WeaponType.LANCE) {
			spr.sprite = lance;
		}
	}
}
