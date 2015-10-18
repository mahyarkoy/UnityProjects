using UnityEngine;
using System.Collections;

public class playercontroller : MonoBehaviour {

	public float jump;
	

	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			this.GetComponent<Rigidbody>().AddForce (new Vector3 (0, jump, 0));
		}
	}

}
