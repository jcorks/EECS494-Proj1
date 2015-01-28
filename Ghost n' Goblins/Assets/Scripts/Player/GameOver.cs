using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {



	// Use this for initialization
	void Start () {
		if (Arthur.lives!=0) {
			Arthur.lives--;
			StartCoroutine(newLife());
			return;
		}
		StartCoroutine(gameOver ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator newLife() {
		GetComponent<Text> ().text = "Player 1 Ready\n Lives: " + Arthur.lives;

		yield return new WaitForSeconds(4);
		Application.LoadLevel ("groundScene");

	}

	IEnumerator gameOver() {
		GetComponent<Text> ().text = "Player 1 Ready\n Game Over";
		yield return new WaitForSeconds(4);
		Application.LoadLevel ("groundScene");
	}
}
