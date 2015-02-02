using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour {
	
	Vector3 pos;
	public GameObject axePrefab;

	public float side;
	public float fireRate = 4f;
	float variation;
	float fireCount = 0;
	float speed; 
	Vector3 origin;
	float chanceToChangeDirection = 0.005f;
	float range = 2f;
	float up;

	// Use this for initialization
	void Start () {
		up = 1f;
		origin = transform.position;
		speed = 0.008f;
		variation = Random.Range(0f,2f);
	}

	void FixedUpdate() {
		if (Random.Range (0f,1f) < chanceToChangeDirection) {
			speed *= -1f; //Change direction randomly
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += speed;
		fireCount += Time.deltaTime;
		transform.position = pos;
		if (transform.position.x > range+origin.x || transform.position.x < -range+origin.x) {
			speed *= -1f;
		}
		if (fireCount > fireRate + variation) {
			Debug.Log("AXING");
			launchProjectile();
			fireCount = 0f;
			variation = Random.Range(0f,1.5f);
		}
		// Only hitable if arthur is behind
		/*if (side == -1f)
			GetComponent<Enemy> ().ignoreProjectiles = (Arthur.arthurPos.x < transform.position.x);
		else if (side == 1f)
			GetComponent<Enemy> ().ignoreProjectiles = (Arthur.arthurPos.x > transform.position.x);*/
	}

	void launchProjectile() {

		GameObject axe = (GameObject)Instantiate (axePrefab);
		Vector3 pos = transform.position;
		if (up == 1f)
			pos.y -= 0.4f;
		else
			pos.y += 0.5f;
		axe.transform.position = pos;
		axe.GetComponent<Axe> ().side = side;
		up *= -1f;
		
		
	}
}
