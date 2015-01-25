using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public float speed = 3f;

	// Use this for initialization
	void Start () {
		Vector2 dirVec = Arthur.arthurPos - transform.position; 
		dirVec.Normalize ();
		GetComponent<PhysObj> ().addVelocity (dirVec * speed);	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, Arthur.arthurPos) > 100)
						Destroy (this.gameObject);
	}
}
