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

	PhysObj phys;
	public ItemType item;

	public void init(ItemType what) {
		item = what;
	}

	public ItemType get(){
		return item;
	}

}
