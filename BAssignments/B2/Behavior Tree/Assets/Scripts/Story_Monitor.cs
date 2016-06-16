using UnityEngine;
using System.Collections;
using TreeSharpPlus;

//accoring to the difference story...here. should be modifies
public class Story_Monitor : MonoBehaviour
{
	public Transform meetingPointDaniel;
	public Transform meetingPointRichard;
	public Transform meetingPointTom;
	public Transform Box1pickup;
	public Transform Box2pickup;
	public Transform Box5pickup;

	public GameObject Daniel;
	public GameObject Richard;
	public GameObject Tom;

	public GameObject Box1;
	public GameObject Box2;
	public GameObject Box3;
	public GameObject Box4;
	public GameObject Box5;

	public Behavior_Tree_Monitor behavior_tree;
	// private Transform box1_transform;
	// private Transform box2_transform;
	// private Transform box3_transform;
	// private Transform box4_transform;
	// private Transform box5_transform;

	void Start()
	{
		//behaviorAgent = new BehaviorAgent(BuildTreeRoot());
		///BehaviorManager.Instance.Register(behaviorAgent);
		//behaviorAgent.StartBehavior();
		// box1_transform = Box1.transform;
		// box2_transform = Box2.transform;
		// box3_transform = Box3.transform;
		// box4_transform = Box4.transform;
	    // box5_transform = Box5.transform;

		//print ("here Start");
	}

	protected Node ST_ApproachAndWait(GameObject participants, Transform target) // go to subtree.
	{
		Val<Vector3> position = Val.V (() => target.position);
		print ("here");
		return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

    protected Node ST_Appraoch_at_certain_raidusAndWait(GameObject participants, Transform target, float radius)
	{
		Val<Vector3> position = Val.V (() => target.position);
		Val<float> dist=Val.V (()=>radius );

		return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position,dist), new LeafWait(1000));
	}

	protected Node ST_Chat(GameObject participants)//chat subtree.
	{//JEFF addition.
		//return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_BodyAnimation("ADAPTMan@shaking_head_no", true), new LeafWait(1000));
		// Val<string> gesture = Val.V (() => "ADAPTMan@shaking_head_no");
		// Val<long> duration = Val.V (() => 1000L);
		Val<Vector3> target = Val.V (() => new Vector3(0, 0, 0));

		// return new DecoratorLoop(new SequenceShuffle(participants.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(gesture, duration), new LeafWait(1000)));
		// return new SequenceShuffle(participants.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(gesture, duration), new LeafWait(1000));
		return new SequenceShuffle(participants.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
	}

	protected Node ST_check_status(story_status story)
	{
		return new DecoratorLoop (new Sequence(Daniel.GetComponent<BehaviorMecanim> ().ST_checking_status(story), new LeafWait (1000)));
	}

	protected Node ST_story_one_arc()
	{
		print(" here is story one process");
		Node walkToMeetingPoint = new SequenceParallel(
            ST_ApproachAndWait(Daniel,  meetingPointDaniel),
            ST_ApproachAndWait(Richard, meetingPointRichard),
            ST_ApproachAndWait(Tom,     meetingPointTom));
		print("After walking");
		if (walkToMeetingPoint.LastStatus == RunStatus.Running)
			behavior_tree.meet_one_point = true; //flag set.
		//Node chat = new ForEach<GameObject>((GameObject participant) => ST_Chat(participant), new [] {Daniel, Richard, Tom});

		return new Sequence (walkToMeetingPoint);// chat);
	}

	protected Node ST_story_two_arc()
	{
		print(" here is story two process");
		Node walkToMeetingPoint = new SequenceParallel(
            ST_ApproachAndWait(Daniel,Box1pickup),
            ST_ApproachAndWait(Richard,Box5pickup));
        /*ST_ApproachAndWait(Tom,     box1_transform)*/
		print ("After walking");
		if (walkToMeetingPoint.LastStatus == RunStatus.Success)
			behavior_tree.meet_one_point = true; //flag set.
		//Node chat = new ForEach<GameObject>((GameObject participant) => ST_Chat(participant), new [] {Daniel, Richard, Tom});

		return new Sequence (walkToMeetingPoint);// chat);
	}

	protected Node ST_story_one() //position, and participants.
	{
		return //two of these true.
			new SelectorParallel(this.ST_check_status(story_status.Meeting_One_Point), this.ST_story_one_arc(),new LeafWait(10000));
	}

	protected Node ST_story_two() //position, and participants.
	{
		print("this is story two process");
		return //any of one true.
			new SelectorParallel(this.ST_check_status(story_status.Box_Moving),this.ST_story_two_arc(),new LeafWait(10000));
	}

	public Node B2_ST_Story_selector()
	{
		return
			//new DecoratorLoop(new Sequence(this.ST_story_one(),ST_Appraoch_at_certain_raidusAndWait(Daniel,Box5.transform,2)));//,ST_story_two,ST_story_three,ST_story_four));
			/*new DecoratorLoop(new Sequence(ST_Appraoch_at_certain_raidusAndWait(Daniel, Box5.transform,2),
			                               ST_Appraoch_at_certain_raidusAndWait(Richard, Box1.transform,2)
			                               ));*/
			new DecoratorLoop(new Sequence(ST_story_one(),ST_story_two()));
	}

    /*

	protected Node ST_story_one_arc() //three people get together.

	{
      return new SequenceParallel (this.
	}

    protected Node ST_story_two_arc() //three people get together to the ball. move box ...
		                             {
			//move to the box.
			//lift box. ?? >>>> Transform. participants... :)
			//box move to point.
			//move box.
			//box move to the point.
			//drop box.
			//box move to the point.
		}
	protected Node ST_story_three_arc()
		{
			//move to the circle
			//D kick ball to H
			//Ball move to H

			//H kick ball to J
			//Ball move to J

		}

	protected Node ST_story_one() //position, and participants.
	{
		return
			new SelectorParallel(new DecoratorLoop(ST_checking_status(Story one) , this.ST_story_one_arc());
	}

	protected Node ST_story_two() //box poistion, and destination and participants.
    {
				return
					new SelectorParallel(new DecoratorLoop(ST_checking_status(Story two) , ST_story_two_arc());
	}

    protected Node ST_story_three() // ball position and participatns. and transform.
    {
        return
            new SelectorParallel(new DecoratorLoop(ST_checking_status(Story three) , ST_story_three_arc());
	}

    protected Node ST_story_four() // participants.
	{
        return
            new SelectorParallel(new DecoratorLoop(ST_checking_status(Story four) , ST_story_four_arc());
	}

	{
		return
			new SelectorParallel(new DecoratorLoop(ST_checking_status(Story one) , ST_story_arc());
	}

	protected Node ST
		(new SelectorParallel
*/
}
