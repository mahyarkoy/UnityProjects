using UnityEngine;
using System.Collections;

public class AgentController : MonoBehaviour {

	private Rigidbody rb;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.CompareTag("select"))
			rend.material.color = Color.green;
		else
			rend.material.color = Color.red;
	}
}
