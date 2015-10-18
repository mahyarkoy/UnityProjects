using UnityEngine;
using System.Collections;

public class P3_Player3_controller : MonoBehaviour {
	//public float jumpSpeed;
	public Mouse_Click_Agent mouse_controller;
	private float speed=10;
	private Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	void Update()
	{
		//float h = Input.GetAxis("Horizontal");
		//float v = Input.GetAxis("Vertical");
		float jump = Input.GetAxis("Jump");
		//float run = Input.GetAxis("Run");
		
		float effectiveSpeed = speed;
		//if (run > 0)
		//  effectiveSpeed *= 2;
		if (mouse_controller.state_of_agent == 3||mouse_controller.state_of_agent ==0) { //
			
			animator.SetFloat ("Speed", mouse_controller.number_of_clicks * speed);
			animator.SetBool ("Jump", jump > 0);
		}
		
		//   transform.Rotate(new Vector3(0, h * effectiveSpeed, 0) * Time.deltaTime);
	}
}
