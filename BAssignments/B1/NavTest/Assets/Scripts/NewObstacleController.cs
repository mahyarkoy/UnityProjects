using UnityEngine;
using System.Collections;

public class NewObstacleController : MonoBehaviour {
	
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
			if(Input.GetButton("left")) {
				move.x = move.x - .1f;
			}
			if(Input.GetButton("right")) {
				move.x = move.x + .1f;
			}
			if(Input.GetButton("up")) {
				move.z = move.z + .1f;
			}
			if(Input.GetButton("down")) {
				move.z = move.z - .1f;
			}
			rb.position = rb.position + move;
		}
		else
			rend.material.color = Color.blue;
	}
}
