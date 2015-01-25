using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	
	PhysObj phys;
	public static float zombieSpeedMin = 1.7f;
	public static float zombieSpeedMax = 2.3f	;
	public Vector3 speed;
	public bool spawned = false;
	public float spawnTime = 20.0f;
	public float lifetime = 5.0f; // seconds of life before despawn

	float lifetick = 0;
	float curSpawnTime = 1000;
	float originalYscale;
	float despawnTime = 1.8f; // tiem it takes to despawn


	public void init(Vector3 arthurPos) {
		curSpawnTime = spawnTime;
			
		phys = GetComponent<PhysObj> ();
		

		
		
		
	}
	
	void Awake() {
		originalYscale = transform.localScale.y;
	}
	
	
	// Use this for initialization
	void Start () {
		GetComponent<Enemy>().score = 100;
	}



	void FixedUpdate() {
		lifetick += Time.deltaTime;
		if (lifetick > lifetime) {
			despawn ();
		}
	}

	void despawn() {
		GetComponent<PhysObj> ().setVelocity (new Vector2 (0, 0));
		GetComponent<Enemy>().ready = false;
		GetComponent<MeshRenderer>().material.color = new Color(255, 255, 0, 255);
		if (lifetick - lifetime > despawnTime) {
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (curSpawnTime < 0 && !spawned) {
			Vector3 speedVec = Arthur.arthurPos - transform.position;
			speedVec.Normalize ();
			if (speedVec == new Vector3(0, 0, 0)) {
				speedVec = new Vector3(1, 0, 0);
			}	
			
			speed = speedVec * (Random.value * zombieSpeedMax + zombieSpeedMin);
			phys.addVelocity (speed);
			spawned = true;

			GetComponent<Enemy>().ready = true;
			GetComponent<MeshRenderer> ().material.color = new Color (64, 0, 0, 255);
		} else {
			if (!spawned) {
				curSpawnTime -= Time.deltaTime;
				drawSpawnAnimation();
			}
		}
	}

	void drawSpawnAnimation() {
		GetComponent<MeshRenderer> ().material.color = new Color (255, 255, 255, 255);
		/*
		Vector3 newScale = transform.localScale;
		newScale.y = originalYscale * (1 - curSpawnTime / spawnTime);
		transform.localScale = newScale;
		*/
	}

	void drawDespawnAnimation() {
		GetComponent<MeshRenderer> ().material.color = new Color (255, 255, 255, 255);

	}
}