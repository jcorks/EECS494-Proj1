using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {
	
	public GameObject zombiePrefab;
	
	public float respawnRateSeconds = 3;
	float timer;
	
	// Use this for initialization
	void Start () {
		timer = respawnRateSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			
			// stagger creation
			StartCoroutine (createZombie());
			StartCoroutine (createZombie());
			StartCoroutine (createZombie());
			timer = respawnRateSeconds;
		}
		
	}
	
	
	// Delay creating a zombie
	IEnumerator createZombie() {
		yield return new WaitForSeconds (Random.value*2);
		GameObject z = (GameObject)Instantiate (zombiePrefab);
		z.transform.position += new Vector3 (
			Random.Range (-10, 10) + Arthur.arthurPos.x, .5f, 0);
		
		z.GetComponent<Zombie>().init (Arthur.arthurPos);
		
	}
}
