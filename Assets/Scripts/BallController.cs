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
		/*myCollider = GetComponent<Collider>();
		previousPosition = rb.position; 

		minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z); 
		partialExtent = minimumExtent * (1.0f - skinWidth); 
		sqrMinimumExtent = minimumExtent * minimumExtent;*/



		ground_z = ground.GetComponent<Collider>().bounds.size.z / 2;

		float xmax = 100;
		float xmin = -xmax;

		float zmax = 500;
		float zmin = 200;
		Random.seed = 13;
		Vector3 force = Vector3.zero;
		force.x = Random.Range (xmin, xmax);
		force.z = Random.Range (zmin, zmax);

		if (Random.Range (-1, 1) < 0) {
			force.z = - force.z;
		}

		force.y = .0f;
		rb.AddForce(force);
	}


	void FixedUpdate() {
		/*Vector3 movementThisStep = rb.position - previousPosition; 
		float movementSqrMagnitude = movementThisStep.sqrMagnitude;
		
		if (movementSqrMagnitude > sqrMinimumExtent) 
		{ 
			float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
			RaycastHit hitInfo; 
			
			//check for obstructions we might have missed 
			if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
			{
				if (!hitInfo.collider)
					return;
				
				if (hitInfo.collider.isTrigger) 
					hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);
				
				if (!hitInfo.collider.isTrigger)
					rb.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent; 
				
			}
		} 
		
		previousPosition = rb.position; 
*/
		// se la palla s'incastra in orizzontale.
		if (Mathf.Abs (rb.velocity.z) < 0.01f) {
			rb.AddForce(new Vector3(0, 0, Random.Range(-10, 10)));
		}

		bool gameEnded = Mathf.Abs (transform.position.z) > ground_z;
		if (gameEnded) {
			GameObject player = null;
			if (transform.position.z > ground_z) {
				player = GameObject.FindGameObjectWithTag("Player");
				player.SendMessage("IncrementScore");
			} else if (transform.position.z < -ground_z) {
				player = GameObject.FindGameObjectWithTag("CPU Player");
				player.SendMessage("IncrementScore");
			}
			transform.position = initialPosition;
		}
		//rb.velocity = rb.velocity * (1f+dspeed);
	}

	public void OnCollisionExit(Collision col) {
		if (col.gameObject.CompareTag ("GroundLines")) {
			rb.velocity = rb.velocity * 1.01f;
		}
	}
}
