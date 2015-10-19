using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	const float AGENTDEFAULTSPEED = 2f;

	Animator anim;
	int jumpHash = Animator.StringToHash("jump2");
	int runStateHash = Animator.StringToHash("Base Layer.run");
	int walkStateHash = Animator.StringToHash("Base Layer.walk");
	NavMeshAgent nav;
	bool justJumped;
	
	void Start ()
	{
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent> ();
		justJumped = false;
	}
	
	
	void Update ()
	{
		if (nav.isOnOffMeshLink && justJumped == false) {
			anim.SetTrigger(jumpHash);
			justJumped = true;
			return;
		}

		if (!nav.isOnOffMeshLink) {
			justJumped = false;
		}

		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		/*if(nav.isOnOffMeshLink && stateInfo.nameHash == runStateHash)
		{
			anim.SetTrigger (jumpHash);
		}
		
		else if(nav.isOnOffMeshLink && stateInfo.nameHash == walkStateHash) {
			anim.SetTrigger(jumpHash);
		}*/

		if (Vector3.Magnitude (nav.velocity) > .01f && Vector3.Magnitude (nav.velocity) < 3f) {
			anim.SetFloat ("Speed", .5f);
		} else if (Vector3.Magnitude (nav.velocity) >= 3f) {
			anim.SetFloat ("Speed", 1.0f);
		} else {
			anim.SetFloat("Speed", 0f);
		}
		

	}
}
