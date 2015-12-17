using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	Rigidbody rb;
	Renderer rend;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.CompareTag("select")) {
			rend.material.color = Color.yellow;
			Vector3 move = new Vector3(0,0,0);
			if(Input.GetKeyDown("left")) {
				move.x = move.x - .1f;
			}
			if(Input.GetKeyDown("right")) {
				move.x = move.x + .1f;
			}
			if(Input.GetKeyDown("up")) {
				move.z = move.z + .1f;
			}
			if(Input.GetKeyDown("down")) {
				move.z = move.z - .1f;
			}
			rb.velocity = move;
		}
		else
			rend.material.color = Color.blue;
	}
}
