using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class BotControlScript : MonoBehaviour
{
	
	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion
	
	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	
	static int idleState = Animator.StringToHash("Base Layer.Idle");	
	static int walkState = Animator.StringToHash("Base Layer.Walk");			// these integers are references to our animator's states
    static int runState = Animator.StringToHash("Base Layer.Run");              // and are used to check state for various actions to occur
    static int jumpState = Animator.StringToHash("Base Layer.Jump");                // within our FixedUpdate() function below
	

	void Start ()
	{
		// initialising reference variables
		anim = GetComponent<Animator>();
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

        // toggle run boolean
        if (Input.GetButtonDown("Run"))
        {
            anim.SetBool("Run", true);
            
        }
        if (Input.GetButtonUp("Run"))
        {
            anim.SetBool("Run", false);

        }


        // jumping

        // only jump if we are in walk or jump state
        if (currentBaseState.nameHash == walkState || currentBaseState.nameHash == runState)
		{
			if(Input.GetButtonDown("Jump"))
			{
				anim.SetBool("Jump", true);
			}
		}
		
		// if we are in the jumping state... 
		else if(currentBaseState.nameHash == jumpState)
		{
			//  ..and not still in transition..
			if(!anim.IsInTransition(0))
			{
				// set the Jump to false so that the state does not loop 
				anim.SetBool("Jump", false);
			}
			
		}
		
		
	}
}
