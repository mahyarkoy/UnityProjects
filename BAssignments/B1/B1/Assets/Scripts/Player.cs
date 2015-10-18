using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sprint = Input.GetButton("Fire3");
        bool jump = Input.GetButtonDown("Jump");

        animator.SetFloat("Forward", v);
        animator.SetFloat("Turn", h);
        animator.SetBool("Sprint", sprint);
        animator.SetBool("Jump", jump);
    }
}
