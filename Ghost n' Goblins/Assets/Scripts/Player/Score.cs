using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
	public int totalTime;
	public bool gibson = false;

	private int currentTime;
	private GUIText thisGUI;
	private int Minutes;
	private int Seconds;
	private float seconds = 0f;
	string colon = ":";
	// Use this for initialization
	void Start () {	
		currentTime = totalTime;
		Seconds = currentTime % 60 ;
		Minutes = (currentTime - Seconds) / 60;
		thisGUI = this.GetComponent<GUIText> ();
		thisGUI.text = string.Concat(Minutes.ToString(),colon,Seconds.ToString());
		seconds = 0f;	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime != 0 && !gibson) {
			seconds += Time.deltaTime;
		}
		if (seconds > 1f) {
			currentTime--;
			Seconds = currentTime % 60 ;
			Minutes = (currentTime - Seconds) / 60;
			thisGUI.text = string.Concat(Minutes.ToString(),colon,Seconds.ToString());
			seconds = 0f;
		}
	}
}
