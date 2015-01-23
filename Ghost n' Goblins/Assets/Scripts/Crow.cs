using UnityEngine;
using System.Collections;

public class Crow : MonoBehaviour {
	
	Vector3 m_centerPosition;
	float m_degrees;
	float m_speed = -2.5f;
	float m_amplitude = 0.5f;
	float m_period = 1f;
	int timer = 100;
	// Use this for initialization
	void Start () {
		m_centerPosition = this.transform.position;
		GetComponent<Enemy>().score = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (renderer.isVisible) {
			GetComponent<Enemy>().ready = true;
		}
		if (GetComponent<Enemy>().ready == true && timer > 0) {
			timer--;
		}
		if (GetComponent<Enemy>().ready == true && timer == 0) {
			movement ();
		}
	}

	void movement() {
		float deltaTime = Time.deltaTime;
		
		// Move center along x axis
		m_centerPosition.x += deltaTime * m_speed;
		
		// Update degrees
		float degreesPerSecond = 360.0f / m_period;
		m_degrees = Mathf.Repeat(m_degrees + (deltaTime * degreesPerSecond), 360.0f);
		float radians = m_degrees * Mathf.Deg2Rad;
		
		// Offset by sin wave
		Vector3 offset = new Vector3(0.0f, m_amplitude * Mathf.Sin(radians), 0.0f);
		transform.position = m_centerPosition + offset;	
	}
}
