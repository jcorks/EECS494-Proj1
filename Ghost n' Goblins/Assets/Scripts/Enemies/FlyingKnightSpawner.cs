using UnityEngine;
using System.Collections;

public class FlyingKnightSpawner : MonoBehaviour {


	/* When knights are spawned, there are 3 possible height offsets:
	 * low -> Will hit the player. THey must avoid it
	 * med -> Will not hit the player if the player ducks
	 * high -> Will not hit the player. Meant to distract
	 * At least one of the knights will be low
	 */
	public float spawnInterval = 7f;
	float spawnTime = 0f;

	public int numPerWave = 3;

	float[] yPosSource = {3.7f, 4.9f, 6f}; // low, med, hi positions
	float xPos = 9;
	float minXSpace = .5f;


	public float spawnXBegin = 71.0f;
	public float spawnXEnd = 93.0f;

	public Sprite knight;

	enum Positions {
		LOW,
		MED,
		HIGH,
	}

	float degInterval = 30f;
	float Yspread = 3f; // range of possible height spawning values

	public GameObject FKprefab;



	// Use this for initialization
	void Start () {
		spawnTime = spawnInterval+1;
	}
	
	// Update is called once per frame
	void Update () {




		spawnTime += Time.deltaTime;
		if (spawnTime > spawnInterval &&
		    Arthur.arthurPos.x >= spawnXBegin &&
		    Arthur.arthurPos.x <= spawnXEnd) {
			spawnKnights();
			spawnTime = 0;
		}
	}


	void spawnKnights() {

		/* At least one knight is at a "low" position.
		 * The others seem to be randomly distributed, but with
		 * the restriction that there is never the case where all knights are 
		 * at the lowest position
		 */
		float[] yPositions = new float[numPerWave];
		int numLow = 0;

		for (int i = 0; i < numPerWave; ++i) {

			int candidateSourceIndex = (int)Random.Range(0, 3);
			if (candidateSourceIndex == (int)Positions.LOW) {
			    if (numLow >= numPerWave - 1) { continue; };
				numLow++;
			}



			if (i == numPerWave - 1 && numLow == 0) {
				yPositions[i] = yPosSource[(int)Positions.LOW];
			} else {
				yPositions[i] = yPosSource[candidateSourceIndex];
			}

		}



		for(int i = 0; i < numPerWave; ++i) {
			GameObject knight = (GameObject) Instantiate (FKprefab);


			knight.GetComponent <FlyingKnight>().degOffset = degInterval*i* Random.value;
			knight.transform.position = (new Vector3(Arthur.arthurPos.x + xPos, yPositions[i], 0)) 	
								      + (new Vector3(minXSpace + (Random.value*.3f+.6f), 0, 0)) * i;
		}
	}
}
