using UnityEngine;
using System.Collections;

public enum ItemType {
	LANCE,
	KNIFE,
	FIREBALL,
	ARMOR,
	MONEY
};

public class Items : MonoBehaviour {

	public Sprite Lance;
	public Sprite Knife;
	public Sprite Fireball;
	public Sprite Armor;
	public Sprite Money;

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
					
		
	}


	public void init(ItemType what) {
		item = what;
	}

	public ItemType get(){
		return item;
	}

}
