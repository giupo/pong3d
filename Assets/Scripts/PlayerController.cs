using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : AbstractPlayerController {

	void FixedUpdate () {
		float horizontal = .0f;
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			horizontal = Input.GetAxis ("Horizontal");
		} else {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				horizontal = touchDeltaPosition.x/10;
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

}
