using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	PhysObj phys;
	public int health = 0;
	public int score = 0;
	public bool ready = false;
	public bool ignoreProjectiles = false;
	public bool dropsItem = true;
	public bool dead;
	public GameObject ItemPrefab;
	public GameObject HitPrefab;

	void Start() {
		dead = false;
	}

	void Update() {
		if (dead) {
			health--;
			if (health < 1) {
				GameObject Hit = (GameObject)Instantiate (HitPrefab);
				Hit.transform.position = transform.position;

				float drop = Random.Range(0f,1f);
				if (drop < 0.1 && dropsItem) {
					GameObject Item = (GameObject)Instantiate (ItemPrefab);
					Item.transform.position = transform.position;
					drop = Random.Range(0f,1f);
					if (drop < 0.2) { //weapon drop
						drop = Random.Range(0f,1f);
						if (drop < 0.33) 
							Item.GetComponent<Items>().item = ItemType.KNIFE;
						if (drop > 0.33 & drop < 0.66)
							Item.GetComponent<Items>().item = ItemType.LANCE;
						else 
							Item.GetComponent<Items>().item = ItemType.FIREBALL;
					}
					else { //scoreDrop
						if (drop < 0.05) 
							Item.GetComponent<Items>().item = ItemType.ARMOR;
						else
							Item.GetComponent<Items>().item = ItemType.MONEY;
					}
				}
				Debug.Log("score");
				int scoreNow = int.Parse (Arthur.scoreGT.text);
				scoreNow = scoreNow + score;
				Arthur.scoreGT.text = scoreNow.ToString ();
				Destroy(this.gameObject);
			}
			dead = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && ready && !ignoreProjectiles) {
			other.GetComponent<Weapon>().HitQueue.Enqueue(this.GetComponent<Collider>());
			GameObject spk = GameObject.FindGameObjectWithTag("Speaker"); 
			spk.GetComponent<AudioSource>().Play ();
			Debug.Log (other.GetComponent<Weapon>().HitQueue.Count);
		}
		
		if (other.gameObject.GetComponent<Arthur> ()) {
			print ("Hello, arthur!");
		}
	}
}
