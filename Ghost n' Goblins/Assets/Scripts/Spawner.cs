using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject example;
	public GameObject arthurPrefab;

	GameObject arthur;
	float timer;

	// Use this for initialization
	void Start () {
		arthur = (GameObject)Instantiate (arthurPrefab);
		arthur.transform.position = new Vector3 (0, 4, 0);



		timer = 4;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			createZombie();
			timer = 2;
		}

	}



	void createZombie() {
		GameObject z = (GameObject)Instantiate (example);
		z.transform.position = arthur.transform.position;
		z.transform.position += new Vector3 (
			Random.Range (-10, 10), 0, 0);

		z.GetComponent<Zombie>().init (arthur.transform.position);
	
	}
}
