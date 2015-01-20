using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	
	PhysObj phys;
	public static float zombieSpeedMin = 3;
	public static float zombieSpeedMax = 5;
	public Vector3 speed;
	public bool spawned = false;
	public float spawnTime = .6f;
	
	
	public void init(Vector3 arthurPos) {
		
		
		
		phys = GetComponent<PhysObj> ();
		
		Vector3 speedVec = arthurPos - transform.position;
		speedVec.Normalize ();
		
		speed = speedVec * Random.Range (zombieSpeedMin, zombieSpeedMax);
		
		
		
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
		if (spawnTime < 0 && !spawned) {
			phys.addVelocity (speed);
			spawned = true;
		}
		spawnTime -= Time.deltaTime;
	}
}