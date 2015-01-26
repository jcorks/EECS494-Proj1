using UnityEngine;
using System.Collections;

public class Wraith : MonoBehaviour {

	public GameObject projPrefab;
	public float speed  = 2f;
	public float speedDown  = 0.5f;

	
	public float leftAndRightEdge = 2.5f;

	public float downLimit = 0.25f;

	Vector3 startingPos;
	Vector3 downPos;
	float side = 0f;
	float down = 0f;
	float chanceToThrowProjectiles = 0.1f;


	// Use this for initialization
	void Start () {
		startingPos = transform.position; 
		side = 1f;
	}

	// Update is called once per frame
	void Update() {
		//Basic Movement
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime * side;
		pos.y += speedDown * Time.deltaTime * down;
		transform.position = pos;
		Debug.Log (downPos.y-downLimit);
		//if on leftEdge while going left
		if (pos.x < -leftAndRightEdge+startingPos.x && side == -1) {
			side = 0;
			down = -1f; //start going down
			downPos = transform.position;
			if (Random.value < chanceToThrowProjectiles) {
				GameObject proj = (GameObject)Instantiate (projPrefab);
				proj.GetComponent<Leek>().side = -1;
				proj.GetComponent<Leek>().weaponDirection = false;
				proj.transform.position = transform.position;
			}
		}
		if (pos.x > leftAndRightEdge+startingPos.x && side == 1) {
			side = 0;
			down = -1f; //start going down
			downPos = transform.position;
			if (Random.value < chanceToThrowProjectiles) {
				GameObject proj = (GameObject)Instantiate (projPrefab);
				proj.GetComponent<Leek>().side = 1;
				proj.GetComponent<Leek>().weaponDirection = false;
				proj.transform.position = transform.position;	
			}
		}
		//If on the leftside and we are at down limit
		if (pos.x < -leftAndRightEdge+startingPos.x && side == 0 && pos.y < downPos.y-downLimit) {
			side = 1f;
			down = 0; //start going down
			if (Random.value < chanceToThrowProjectiles) {
				GameObject proj = (GameObject)Instantiate (projPrefab);
				proj.GetComponent<Leek>().weaponDirection = true;
				proj.transform.position = transform.position;			
			}
		}
		if (pos.x > leftAndRightEdge+startingPos.x && side == 0 && pos.y < downPos.y-downLimit) {
			side = -1f;
			down = 0; //start going down
			Debug.Log("yo");
			if (Random.value < chanceToThrowProjectiles) {
				GameObject proj = (GameObject)Instantiate (projPrefab);
				proj.GetComponent<Leek>().weaponDirection = true;
				proj.transform.position = transform.position;	
			}
		}
	}
}
