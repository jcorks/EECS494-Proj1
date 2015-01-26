using UnityEngine;
using System.Collections;

public class WraithSpawner : MonoBehaviour {
	public GameObject wraithPrefab;

	public float respawnRateSeconds = 3;
	float timer;

	// Use this for initialization
	void Start () {
		timer = respawnRateSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0 && Arthur.arthurPos.x + 15f < transform.position.x) {

			// stagger creation
			StartCoroutine (createWraith());
			timer = respawnRateSeconds;
		}
	}

	IEnumerator createWraith() {
		yield return new WaitForSeconds (Random.value*2);
		GameObject z = (GameObject)Instantiate (wraithPrefab);
		float randPos = Random.Range (4,7);
		z.transform.position += new Vector3 (
			transform.position.x, randPos, 0);
	}
}
