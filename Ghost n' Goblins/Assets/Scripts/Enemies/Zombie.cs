using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	
	PhysObj phys;
	int direction = 1;
	public static float zombieSpeedMin = 1.7f;
	public static float zombieSpeedMax = 2.3f	;
	public Vector3 speed;
	public bool spawned = false;
	public float spawnTime = 20.0f;
	public float lifetime = 5.0f; // seconds of life before despawn

	SpriteRenderer spr;

	float lifetick = 0;
	float curSpawnTime = 1000;
	float originalYscale;
	float originalYposDelta;
	float originalYposMin;
	float despawnTime = 1.8f; // time it takes to despawn







	public void init(Vector3 arthurPos) {
		curSpawnTime = spawnTime;
			
		phys = GetComponent<PhysObj> ();
		

		
		
		
	}
	
	void Awake() {
		spr = GetComponentInChildren<SpriteRenderer> ();
		originalYscale = transform.localScale.y;
		originalYposDelta = spr.bounds.extents.y/2f;
	
		GetComponent<PhysObj> ().ignoreGravity = true;
		GetComponent<BoxCollider> ().enabled = false;
	}
	
	
	// Use this for initialization
	void Start () {

		GetComponent<Enemy>().score = 100;
		if (transform.position.x > Arthur.arthurPos.x) {
			direction = -1;
		}
		spr.transform.localScale = 
			new Vector3(direction*spr.transform.localScale.x,
			            spr.transform.localScale.y,
			            spr.transform.localScale.z);
	}



	void FixedUpdate() {
		lifetick += Time.deltaTime;
		if (lifetick > lifetime) {
			despawn ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "ZDespawner") {
			lifetick = lifetime;
		}
	}

	void despawn() {
		GetComponent<PhysObj> ().setVelocity (new Vector2 (0, 0));
		GetComponent<Enemy>().ready = false;
		drawDespawnAnimation ();
		if (lifetick - lifetime > despawnTime) {
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (curSpawnTime < 0 && !spawned) {
			GetComponent<PhysObj> ().ignoreGravity = false;
			GetComponent<BoxCollider> ().enabled =true;


			
			speed = new Vector3(direction * (Random.value * zombieSpeedMax + zombieSpeedMin), 0, 0);




			phys.addVelocity (speed);
			spawned = true;

			GetComponent<Enemy>().ready = true;
			spr.color = new Color (64, 0, 0, 255);
		} else {
			if (!spawned) {
				curSpawnTime -= Time.deltaTime;
				drawSpawnAnimation();
			}
		}
	}

	void drawSpawnAnimation() {
		spr.color = new Color (255, 255, 255, 255);
		Vector3 newScale = spr.transform.localScale;
		newScale.y = originalYscale * (spawnTime - curSpawnTime) / spawnTime;	    
		spr.transform.localScale = newScale;


	}

	void drawDespawnAnimation() {
		spr.color = new Color (255, 255, 255, 255);
		Vector3 newScale = spr.transform.localScale;
		newScale.y = originalYscale * (despawnTime - (lifetick - lifetime)) / despawnTime;	    
		spr.transform.localScale = newScale;
	}
}