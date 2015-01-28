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

	public GameObject Sprite; 
	PhysObj phys;
	public ItemType item;
	void Start() {
		Sprite = GameObject.FindWithTag ("spriteItem");
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
		if (item == ItemType.LANCE) 
			Sprite.GetComponent<SpriteRenderer> ().sprite = Lance;
		if (item == ItemType.KNIFE) 
			Sprite.GetComponent<SpriteRenderer> ().sprite = Knife;
		if (item == ItemType.FIREBALL)
			Sprite.GetComponent<SpriteRenderer> ().sprite = Fireball;
		if (item == ItemType.ARMOR) 
			Sprite.GetComponent<SpriteRenderer> ().sprite = Armor;
		if (item == ItemType.MONEY) 
			Sprite.GetComponent<SpriteRenderer> ().sprite = Money;
					
		
	}


	public void init(ItemType what) {
		item = what;
	}

	public ItemType get(){
		return item;
	}

}
