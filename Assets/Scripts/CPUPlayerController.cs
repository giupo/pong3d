using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CPUPlayerController : AbstractPlayerController {

	public GameObject ball;
	private int score;
	public float speed;
	public float maxSpeed;

	//private Rigidbody rb;
	private Vector3 initialPosition = new Vector3(0f, .5f, 16f);

	void Init() {
		Debug.Log ("Init : " + transform.position.ToString());
		transform.position = initialPosition;
	}

	void FixedUpdate () {
		if (speed < maxSpeed) {
			speed += 0.001f;
		}

		// usata per capire cosa fa la CPU
		Vector3 ballVelocity = ball.GetComponent<Rigidbody> ().velocity;
		Vector3 target;
		if (ballVelocity.z > 0) {
			// vai verso la palla...
			target = ball.transform.position;
		} else {
			// vai verso il centro...
			target = Vector3.zero;
		}

		float offset = target.x - transform.position.x;
		transform.Translate (new Vector3(offset,0,0) * speed * Time.deltaTime);
		//transform.rotation = Quaternion.Euler (0, -offset*10, 0);
	 	// controllo per non sconfinare.
		if (transform.position.x > xp) {
			transform.position = new Vector3(xp, initialPosition.y, initialPosition.z);
		} else if (transform.position.x < -xp) {
			transform.position = new Vector3(-xp, initialPosition.y, initialPosition.z);
		}
	}
}
