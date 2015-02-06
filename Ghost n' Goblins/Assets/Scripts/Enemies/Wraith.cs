using UnityEngine;
using System.Collections;

public class Wraith : MonoBehaviour {

	public GameObject projPrefab;
	public float speed  = 3f;
	public float speedDown  = 0.7f;

	private Vector3 turnState = new Vector3(1.1f, 0.5f, 1f);
	private Vector3 moveState = new Vector3(1.1f, 0.5f, 1f);

	public Sprite body;
	public Sprite spawning;
	public Sprite leek;
	
	public float leftAndRightEdge = 2.5f;

	public float downLimit = 0.8f;
	Vector3 startingPos;
	Vector3 downPos;
	float side = 0f;
	float down = 0f;
	float chanceToThrowProjectiles = 0.1f;
	float timer;
	bool spawn = true;
	// Use this for initialization
	void Start () {
		GetComponent<Enemy>().score = 100;
		timer = Time.time + 1;
		startingPos = transform.position; 
		if (Arthur.arthurPos.x < transform.position.x) {
			side = -1;
		}
		else {
			side = 1;
		}

	}

	// Update is called once per frame
	void Update() {
		if (timer > Time.time) {
			if (spawn)
				GetComponentInChildren<SpriteRenderer>().sprite = spawning;
			spawn = false;
		}
		else {
			if (!spawn) {
				GetComponentInChildren<SpriteRenderer>().sprite = body;
				spawn = true;
			}
			if (side*-1 != GetComponentInChildren<RectTransform>().localScale.x/4 && down == 0f) {
				Vector3 temp = GetComponentInChildren<RectTransform>().localScale;
				temp.x *= -1;
				GetComponentInChildren<RectTransform>().localScale = temp;
			}
			if (Arthur.arthurPos.x - 10f > transform.position.x || 
				Arthur.arthurPos.x + 10f < transform.position.x) {
					PhysManager.wraithCount--;
					Destroy (this.gameObject);
			}
			//Basic Movement
			Vector3 pos = transform.position;
			pos.x += speed * Time.deltaTime * side;
			pos.y += speedDown * Time.deltaTime * down;
			transform.position = pos;
			//Debug.Log (downPos.y-downLimit);
			//if on leftEdge while going left
			if (pos.x < -leftAndRightEdge+startingPos.x && side == -1) {
				side = 0;
				if (pos.y < 1) {
					down = 1f; //start going down
				}
				else {
					down = -1f;
				}
				downPos = transform.position;
				this.transform.localScale = turnState;
				if (Random.value < chanceToThrowProjectiles) {
					GameObject proj = (GameObject)Instantiate (projPrefab);
					proj.GetComponent<Leek>().side = -1;
					proj.GetComponent<Leek>().weaponDirection = false;
					proj.transform.position = transform.position;
				}
			}
			if (pos.x > leftAndRightEdge+startingPos.x && side == 1) {
				side = 0;
				if (pos.y < 1) {
					down = 1f; //start going down
				}
				else {
					down = -1f;
				}
				this.transform.localScale = turnState;
				downPos = transform.position;
				if (Random.value < chanceToThrowProjectiles) {
					GameObject proj = (GameObject)Instantiate (projPrefab);
					proj.GetComponent<Leek>().side = 1;
					proj.GetComponent<Leek>().weaponDirection = false;
					proj.transform.position = transform.position;	
				}
			}
			//If on the leftside and we are at down limit
			if (pos.x < -leftAndRightEdge+startingPos.x && side == 0) {
				if (down == 1f && pos.y > downPos.y+downLimit) {
					side = 1f;
					down = 0; //start going down
					this.transform.localScale = moveState;
					if (Random.value < chanceToThrowProjectiles) {
						GameObject proj = (GameObject)Instantiate (projPrefab);
						proj.GetComponent<Leek>().weaponDirection = true;
						proj.transform.position = transform.position;			
					}
				}
				else if (down == -1f && pos.y < downPos.y-downLimit) {
					side = 1f;
					down = 0; //start going down
					this.transform.localScale = moveState;
					if (Random.value < chanceToThrowProjectiles) {
						GameObject proj = (GameObject)Instantiate (projPrefab);
						proj.GetComponent<Leek>().weaponDirection = true;
						proj.transform.position = transform.position;			
					}
				}
			}
			if (pos.x > leftAndRightEdge+startingPos.x && side == 0) {
				if (down == 1f && pos.y > downPos.y+downLimit) {
					side = -1f;
					down = 0; //start going down
					this.transform.localScale = moveState;
					if (Random.value < chanceToThrowProjectiles) {
						GameObject proj = (GameObject)Instantiate (projPrefab);
						proj.GetComponent<Leek>().weaponDirection = true;
						proj.transform.position = transform.position;	
					}
				}
				else if (down == -1f && pos.y < downPos.y-downLimit) {
					side = -1f;
					down = 0; //start going down
					this.transform.localScale = moveState;
					if (Random.value < chanceToThrowProjectiles) {
						GameObject proj = (GameObject)Instantiate (projPrefab);
						proj.GetComponent<Leek>().weaponDirection = true;
						proj.transform.position = transform.position;	
					}
				}
		}
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon") {
			Debug.Log("wraith destroyed");
			PhysManager.wraithCount--;
		}

	}
}
