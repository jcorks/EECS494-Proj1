using UnityEngine;
using System.Collections;

public class WraithSpawner : MonoBehaviour {
	public GameObject wraithPrefab;
	public float respawnRateSeconds = 3f;
	float initTime;
	float timer;

	// Use this for initialization
	void Start () {
		timer = respawnRateSeconds;
		initTime = Random.Range(1f, respawnRateSeconds);
		timer -= initTime;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = respawnRateSeconds;
			if (PhysManager.wraithCount < 6 && 
		    (Arthur.arthurPos.x - 10f < transform.position.x || 
		    Arthur.arthurPos.x + 10f > transform.position.x)) {
				PhysManager.wraithCount++;
				// stagger creation
				StartCoroutine (createWraith());
			}
		}
	}

	IEnumerator createWraith() {
		yield return new WaitForSeconds (Random.value*2);
		GameObject z = (GameObject)Instantiate (wraithPrefab);
		float rand = Random.Range (0,1);
		float randPos;
		if (rand > 0.66) 
			randPos = 3.8f;
		else if (rand < 0.66 && rand > 0.33)
			randPos = 3.1f;	
		else
			randPos = 2.4f;	
		z.transform.position = new Vector3 (
			transform.position.x, randPos, 0);
	}
}
