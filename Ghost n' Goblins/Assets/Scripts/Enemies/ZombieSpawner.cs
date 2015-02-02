using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {
	
	public GameObject zombiePrefab;
	
	public float respawnRateSeconds = 3;
	public float respawnXbegin = 0;
	public float respawnXEnd = 50;
	public float yOffset = 0f;
	public bool multiLevel = true; // whether or not to consider the upper area for spawning
	float timer;


	public bool considerUpper = true;
	public float yUpper = 5f;
	public float yUpperThreshold = .3f;
	public float xUpperMin = 14.16f;
	public float xUpperMax = 37.16f;


	bool firstWave = false;
	
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0 &&
		    Arthur.arthurPos.x >= respawnXbegin &&
		    Arthur.arthurPos.x <= respawnXEnd) {
			
			// stagger creation
			StartCoroutine (createZombie());
			StartCoroutine (createZombie());
			StartCoroutine (createZombie());
			timer = respawnRateSeconds;
		}
		
	}
	
	
	// Delay creating a zombie
	IEnumerator createZombie() {
		yield return new WaitForSeconds (Random.value*.8f);
		GameObject z = (GameObject)Instantiate (zombiePrefab);
		float randPos = Random.Range (0, 5) + 3;
		randPos *= (Random.value > .5 ? -1 : 1);
		z.transform.position += new Vector3 (
			randPos	 + Arthur.arthurPos.x, 1.0f, 0);

		// Account for if on higher ground!
		if (multiLevel && Arthur.arthurPos.y > yUpper - yUpperThreshold) {
			float xPos = z.transform.position.x;
			if (xPos < xUpperMin) xPos = xUpperMin + Random.value*2f;
			if (xPos > xUpperMax) xPos = xUpperMax - Random.value*2f;
						z.transform.position = new Vector3 (xPos,
			                                   			   yUpper,
			                                   			   z.transform.position.z);
		}
		z.transform.position += new Vector3 (0, yOffset, 0);
		z.GetComponent<Zombie>().init (Arthur.arthurPos);
		
	}
}
