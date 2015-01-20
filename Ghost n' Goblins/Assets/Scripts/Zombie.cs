using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	
	PhysObj phys;
	public static float zombieSpeedMin = 2;
	public static float zombieSpeedMax = 4;
	public Vector3 speed;
	public bool spawned = false;
	public float spawnTime = 20.0f;
	float curSpawnTime = 1000;
	float originalYscale;
	
	
	public void init(Vector3 arthurPos) {
		curSpawnTime = spawnTime;
			
		phys = GetComponent<PhysObj> ();
		
		Vector3 speedVec = arthurPos - transform.position;
		speedVec.Normalize ();
		
		speed = speedVec * Random.Range (zombieSpeedMin, zombieSpeedMax);
		
		
		
	}
	
	void Awake() {
		originalYscale = transform.localScale.y;
	}
	
	
	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon") {
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (curSpawnTime < 0 && !spawned) {
						phys.addVelocity (speed);
						spawned = true;
		} else {
			if (!spawned) {
				curSpawnTime -= Time.deltaTime;
				drawSpawnAnimation();
			}
		}
	}

	void drawSpawnAnimation() {
		Vector3 newScale = transform.localScale;
		newScale.y = originalYscale * (1 - curSpawnTime / spawnTime);
		transform.localScale = newScale;
	}
}