using UnityEngine;
using System.Collections;

public class P3_Obstacle_controller1 : MonoBehaviour {
	
	//private Rigidbody rb;
	//private Transform ts;
	private int speed = 10;
	public Mouse_Click_Agent mouse_sig;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (mouse_sig.obstacle1_flag == true) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
		 	float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical)*-1;
			transform.Translate (movement*speed*Time.deltaTime);

		}
		
		
	}
}
	


