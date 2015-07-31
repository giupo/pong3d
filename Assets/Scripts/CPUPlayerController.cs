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

	//private Rigidbody rb;
	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
		score = 0;
		xp = 8;
		speed = 0.5f;
		maxSpeed = 20f;
		//rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		if (speed < maxSpeed) {
			speed += 0.003f;
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

	 	// controllo per non sconfinare.
		if (transform.position.x > xp) {
			transform.position = new Vector3(xp, transform.position.y, transform.position.z);
		} else if (transform.position.x < -xp) {
			transform.position = new Vector3(-xp, transform.position.y, transform.position.z);
		}
	}

	public void IncrementScore() {
		audio.PlayOneShot (audio.clip);
		++score;
		scoreLabel.text = score.ToString ();
	}
}
