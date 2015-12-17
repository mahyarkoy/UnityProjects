using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public Animator anim;
	public Rigidbody rbody;

	private float inputH;
	private float inputV;

	private bool run;
	private bool jump;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		run = false;
		jump = false;
	}
	
	// Update is called once per frame
	void Update () {

		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");
		anim.SetBool ("run", run);
		anim.SetBool ("jump", jump);

		anim.SetFloat ("inputH", inputH);
		anim.SetFloat ("inputV", inputV);

		if (Input.GetKey (KeyCode.LeftShift)) {
			run = true;
		} 
		else {
			run = false;
		}
		if (Input.GetKey (KeyCode.Space)) {
			jump = true;
		} 
		else {
			jump = false;
		}
	}
}
