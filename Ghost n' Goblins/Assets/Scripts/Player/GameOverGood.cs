using UnityEngine;
using System.Collections;

public class GameOverGood : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(restart ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator restart() {
		Respawner.checkpointX = -10f;
		yield return new WaitForSeconds(5);
		
		Application.LoadLevel ("groundScene");
	}
}
