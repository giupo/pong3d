using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float damper = 0.0f;
	private int score;
	public Text scoreLabel;
	public float xp = 8f;
	void Start () {
		if (damper == 0.0f) {
			damper = 1.5f;
		}
		this.score = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 offset = Vector3.zero;
		float horizontal = .0f;
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			horizontal = Input.GetAxis ("Horizontal");
		} else {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				horizontal = touchDeltaPosition.x * 5f;
			} else {
				horizontal = Input.acceleration.x * 5f;
			}
		}

		transform.Translate (new Vector3 (horizontal * 10f, 0, 0) * Time.deltaTime);

		if (transform.position.x > xp) {
			transform.position = new Vector3 (xp, transform.position.y, transform.position.z);
		}

		if (transform.position.x < -xp) {
			transform.position = new Vector3 (-xp, transform.position.y, transform.position.z);
		}
	}

	public void IncrementScore() {
		++score;
		scoreLabel.text = score.ToString ();
	}
}
