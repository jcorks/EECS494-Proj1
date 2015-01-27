using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public GameObject projPrefab;
	public float fireRate = 2.0f;
	float fireCount = 0;
	bool mouthOpen = false;

	// Use this for initialization
	void Start () {
		GetComponent<Enemy> ().ready = false;

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Enemy> ().ready = GetComponent<MeshRenderer>().isVisible;
		if (!GetComponent<Enemy> ().ready)
						return;


		fireCount += Time.deltaTime;
		if (fireCount > fireRate) {
			launchProjectile();
			fireCount = 0f;
		}

		mouthOpen = fireCount > fireRate / 2f;
	}

	void launchProjectile() {

		GameObject proj = (GameObject)Instantiate (projPrefab);
		proj.transform.position = transform.position;


	}
}
