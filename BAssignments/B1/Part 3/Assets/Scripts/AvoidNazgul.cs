using UnityEngine;
using System.Collections;

public class AvoidNazgul : MonoBehaviour {

	public float repulsionSpeed;

	private NavMeshAgent nav;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	void OnTriggerStay(Collider other) {
		if(other.CompareTag("nazgul field")) {
			Debug.Log ("Naz");
			Vector3 repulsion = (transform.position - other.gameObject.transform.position).normalized;
			nav.velocity = repulsion * repulsionSpeed;
			Debug.Log (repulsion * repulsionSpeed);
			Debug.Log (repulsion + "      " + repulsionSpeed);
			Debug.Log (nav.velocity);
		}
	}
}
