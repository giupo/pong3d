using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	// Use this for initialization

	public GameObject ground;
	private float ground_z;
	private Rigidbody rb;


	/* Since we are moving WAY too fast, let'see if 
	http://wiki.unity3d.com/index.php?title=DontGoThroughThings can help
	 */

	public bool sendTriggerMessage = false; 	
	
	public LayerMask layerMask = -1; //make sure we aren't in this layer 
	public float skinWidth = 0.1f; //probably doesn't need to be changed 
	
	private float minimumExtent; 
	//private float partialExtent; 
	//private float sqrMinimumExtent; 
	//private Vector3 previousPosition; 

	private Vector3 initialPosition;
	private Collider myCollider;


	// speed
	private float dspeed = .1f;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		initialPosition = rb.position;
	
		ground_z = ground.GetComponent<Collider>().bounds.size.z / 2;

		Kick ();
	}

	void Kick() {
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
		//rb.AddForce(force);

		rb.velocity = force;
	}

	void FixedUpdate() {
		// se la palla s'incastra in orizzontale.
		if (Mathf.Abs (rb.velocity.z) < 0.01f) {
			rb.AddForce(new Vector3(0, 0, Random.Range(-10, 10)));
		}

		bool gameEnded = Mathf.Abs (transform.position.z) > ground_z;
		if (gameEnded) {
			GameObject player = null;
			string tag = transform.position.z > ground_z ? "Player" : "CPU Player";
			player = GameObject.FindGameObjectWithTag(tag);
			player.SendMessage("IncrementScore");
			transform.position = initialPosition;
			Kick();
		}
	}

	public void OnCollisionExit(Collision col) {
		if (col.gameObject.CompareTag ("GroundLines")) {
			rb.velocity = rb.velocity * 1.05f;
		}
	}
}
