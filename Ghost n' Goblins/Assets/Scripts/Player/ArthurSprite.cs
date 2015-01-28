using UnityEngine;
using System.Collections;

public class ArthurSprite : MonoBehaviour {

	public void Change(Sprite what) {
		this.GetComponent<SpriteRenderer> ().sprite = what;
	}

}
