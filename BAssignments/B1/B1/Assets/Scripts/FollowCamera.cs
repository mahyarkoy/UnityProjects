using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public GameObject target;
    Vector3 offset;
    Quaternion rotation;
    Animator animator;

	// Use this for initialization
	void Start () {
        offset = target.transform.position - transform.position;
        animator = target.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate() {
        rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
        if (!(animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))) {
            transform.position = target.transform.position - (rotation * offset);
        }
        

        Vector3 lookAtPosition = target.transform.position;
        lookAtPosition.y = target.transform.position.y + 1;
        transform.LookAt(lookAtPosition);
    }
}
