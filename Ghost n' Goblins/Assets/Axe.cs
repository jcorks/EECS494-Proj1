using UnityEngine;
using System.Collections;

public class Axe : MonoBehaviour {
	
	public float speed = 2.5f;
	public float side;
	public float start;
	
	// Use this for initialization
	void Start () {
		start = transform.position.x;
		Vector3 temp = transform.position;
		temp.x += side;
		Vector2 dirVec =  temp - transform.position; 

		GetComponent<PhysObj> ().addVelocity (dirVec * speed);	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x - start > 30f || transform.position.x - start < -30f)
			Destroy (this.gameObject);
	}
}
