using UnityEngine;
using System.Collections;

public enum ItemType {
	LANCE,
	KNIFE,
	FIREBALL,
	ARMOR,
	MONEY,
	XBOW
};

public class Items : MonoBehaviour {

	public Sprite Lance;
	public Sprite Knife;
	public Sprite Fireball;
	public Sprite Armor;
	public Sprite Money;
	public Sprite Xbow;

	SpriteRenderer sr;


	PhysObj phys;
	public ItemType item;
	void Start() {
		/*
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
		*/
		sr = GetComponentInChildren<SpriteRenderer> ();
		Vector3 t = sr.transform.localScale;
		t.x = 8;
		t.y = 20;
		//sr.transform.localScale = t;
		sr.sprite = Xbow;		
		if (item == ItemType.LANCE) 
			sr.sprite = Lance;
		if (item == ItemType.KNIFE) 
			sr.sprite = Knife;
		if (item == ItemType.FIREBALL)
			sr.sprite = Fireball;
		if (item == ItemType.ARMOR) 
			sr.sprite = Armor;
		if (item == ItemType.MONEY) 
			sr.sprite = Money;
		if (item == ItemType.XBOW) {
			Vector3 theScale = sr.transform.localScale;
			theScale.x = 2;
			theScale.y = 5;
			sr.transform.localScale = theScale;
			sr.sprite = Xbow;		
		}
	}


	public void init(ItemType what) {
		item = what;
	}

	public ItemType get(){
		return item;
	}

}
