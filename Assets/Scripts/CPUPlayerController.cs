using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CPUPlayerController : MonoBehaviour {

	public GameObject ball;
	public Text scoreLabel;
	private int score;
	private float speed;
	public float maxSpeed;
	public float xp;

	private Rigidbody rb;

	void Start () {
		score = 0;
		xp = 8;
		speed = 1.2f;
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		Vector3 position = transform.position;
		if (speed < maxSpeed) {
			speed += 0.1f;
		}


		Vector3 ballVelocity = ball.GetComponent<Rigidbody> ().velocity;
		
		Vector3 target;
		if (ballVelocity.z > 0) {
			// vai verso la palla...
			target = ballVelocity;
		} else {
			// vai verso il centro...
			target = Vector3.zero;
		}

		Vector3 offset = position - target; 
		position.x = position.x - offset.x;

	 	// controllo per non sconfinare.
		if (position.x > xp) {
			position.x = xp;
		} else if (position.x < -xp) {
			position.x = -xp;
		}

		Debug.Log ("Cribbio x = " + position.x.ToString ());
		transform.position = position;
	}

	public void IncrementScore() {
		++score;
		scoreLabel.text = score.ToString ();
	}
}
