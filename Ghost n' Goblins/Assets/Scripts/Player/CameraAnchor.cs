using UnityEngine;
using System.Collections;

public class CameraAnchor : MonoBehaviour {
	public bool active = false;
	public bool isVertical = false;
	public int checkpointNum = 0;
	float graceDist = 7.2f;
	PhysObj wall;

	// Use this for initialization
	void Start () {
		wall = GetComponentInChildren<PhysObj> ();
		wall.isObstacle = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (((!isVertical && (Arthur.arthurPos.x > transform.position.x)) ||
		    (isVertical && (Arthur.arthurPos.y > transform.position.y)))
		    && 
		    checkpointNum > Respawner.checkpointNum) {
			print ("Now set to checkpoint " + checkpointNum.ToString () + "@" + transform.position.ToString ());

			GameOver.theStage = Application.loadedLevelName;
			Vector3 offset = new Vector3((isVertical?0:4f), (isVertical?1f:0f), 0);
			Respawner.checkpoint = transform.position + offset;
			Respawner.checkpointNum = checkpointNum;
		}
		if (((!isVertical) && Arthur.arthurPos.x - graceDist > transform.position.x) ||
		     ((isVertical) && Arthur.arthurPos.y - graceDist > transform.position.y))
						active = true;
		if (active) wall.isObstacle = true;
		
	}
}
