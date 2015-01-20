using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	PhysObj phys;
	public static float zombieSpeedMin;
	public static float zombieSpeedMax;
	public Vector3 speed;
	public bool spawned = false;
	public float spawnTime = .6f;


	public void init(Vector3 arthurPos) {

		

		phys = GetComponent<PhysObj> ();

		Vector3 speedVec = arthurPos - transform.position;
		speedVec.Normalize ();

		speed = speedVec * Random.Range (1, 2);



	}




	// Use this for initialization
	void Start () {

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
