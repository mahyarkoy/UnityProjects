using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    private Animator animator;

	void Start()
    {
        animator = GetComponent<Animator>();
	}

	void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");
        float run = Input.GetAxis("Run");

        float effectiveSpeed = speed;
        if (run > 0)
            effectiveSpeed *= 2;

        animator.SetFloat("Speed", v * effectiveSpeed);
        animator.SetBool("Jump", jump > 0);

        transform.Rotate(new Vector3(0, h * effectiveSpeed, 0) * Time.deltaTime);
	}
}
