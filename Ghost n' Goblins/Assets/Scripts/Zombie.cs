using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	
	PhysObj phys;
	public static float zombieSpeedMin = 2;
	public static float zombieSpeedMax = 4;
	public Vector3 speed;
	public bool spawned = false;
	public float spawnTime = 20.0f;
	public float spawnPeriod = 1000.0f;

	float timeSpawn = 0;
	float curSpawnTime = 1000;
	float originalYscale;
	


	public void init(Vector3 arthurPos) {
		curSpawnTime = spawnTime;
			
		phys = GetComponent<PhysObj> ();
		

		
		
		
	}
	
	void Awake() {
		originalYscale = transform.localScale.y;
	}
	
	
	// Use this for initialization
	void Start () {

	}

	/*void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon" && GetComponent<Enemy>().ready) {
			Destroy(this.gameObject);
		}

		if (other.gameObject.GetComponent<Arthur> ()) {
			print ("Hello, arthur!");
		}
	}*/

	void FixedUpdate() {
		timeSpawn++;
		if (spawnPeriod == timeSpawn)
			Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (curSpawnTime < 0 && !spawned) {
			Vector3 speedVec = Arthur.arthurPos - transform.position;
			speedVec.Normalize ();
			if (speedVec == new Vector3(0, 0, 0)) {
				speedVec = new Vector3(1, 0, 0);
			}	
			
			speed = speedVec * Random.Range (zombieSpeedMin, zombieSpeedMax);
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
		GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, 255);
		/*
		Vector3 newScale = transform.localScale;
		newScale.y = originalYscale * (1 - curSpawnTime / spawnTime);
		transform.localScale = newScale;
		*/
	}
}