using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {

	// Use this for initialization

	public GameObject ground;
	private float ground_z;
	private Rigidbody rb;
	private AudioSource audioSource;

	/* Since we are moving WAY too fast, let'see if 
	http://wiki.unity3d.com/index.php?title=DontGoThroughThings can help
	 */

	public bool sendTriggerMessage = false; 	
	private float minimumExtent; 

	private Vector3 initialPosition;
	private Collider myCollider;

	public Text counter;
	// speed
	public int warmUpSeconds = 3;
	private bool isKicking;
	private float startKicking;
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		initialPosition = rb.position;
	
		ground_z = ground.GetComponent<Collider>().bounds.size.z / 2;

		isKicking = true;
	}

	void Kick() {
		rb.velocity = Vector3.zero;
		counter.enabled = true;
		counter.text = warmUpSeconds.ToString ();
		isKicking = true;
		startKicking = Time.time;
		transform.position = initialPosition;
	}

	void Go() {
		float xmax = 10;
		float xmin = -xmax;
		
		float zmax = 20;
		float zmin = 5;
		Random.seed = Random.Range (1, 414);
		Vector3 force = Vector3.zero;
		force.x = Random.Range (xmin, xmax);
		force.z = Random.Range (zmin, zmax);
		
		if (Random.Range (-1, 1) < 0) {
			force.z = - force.z;
		}
		
		force.y = .0f;
		rb.velocity = force;
	}

	void FixedUpdate() {
		if (isKicking) {
			float elapsed = Time.time - startKicking;
			counter.text = (warmUpSeconds - ((int)elapsed)).ToString ();
			if (elapsed < warmUpSeconds) {
				return;
			} else {
				counter.enabled = false;
				isKicking = false;
				Go ();
			}
		}

		if (isClamped ()) {
			Vector3 kick = new Vector3(-transform.position.x, 0, 0);
			Debug.Log("Kicking ...");
			rb.AddForce(kick);
		}

		// se la palla s'incastra in orizzontale.
		if (Mathf.Abs (rb.velocity.z) < 0.01f) {
			rb.AddForce (new Vector3 (0, 0, Random.Range (-10, 10)));
		}

		bool gameEnded = Mathf.Abs (transform.position.z) > ground_z;
		if (gameEnded) {
			GameObject player = null;
			string tag = transform.position.z > ground_z ? "Player" : "CPU Player";
			player = GameObject.FindGameObjectWithTag (tag);
			player.SendMessage ("IncrementScore");
			GameObject.FindGameObjectWithTag("CPU Player").SendMessage("Init");
			transform.position = initialPosition;
			Kick ();
		}
	}

	public void OnCollisionEnter(Collision col) {
		Debug.Log (col.gameObject.tag);
		if (!col.gameObject.CompareTag ("GroundLines")) {
			audioSource.PlayOneShot (audioSource.clip);
		}
	}

	public void OnCollisionExit(Collision col) {
		if (!col.gameObject.CompareTag ("GroundLines")) {
			rb.velocity = rb.velocity * 1.2f;
		}
	}

	bool isClamped() {
		float x = transform.position.x;
		return Mathf.Abs (x) > 9 && rb.velocity.x == 0;
	}
}
