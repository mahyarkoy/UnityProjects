using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public enum story_status //story state according to the input status.
{
	Meeting_One_Point, //current object.
	Waiting_input,
	Box_Moving,
	Soccer_Playing,
	Shaking_hands_and_Bye,
	The_End,
}

public enum user_input_status
{
	None_input,
	Box_Move,
	Soccer_Play,
}

public class BehaviorTree : MonoBehaviour
{
    public Transform meetingPointDaniel;
    public Transform meetingPointRichard;
    public Transform meetingPointTom;

    public GameObject Daniel;
    public GameObject Richard;
    public GameObject Tom;

    private BehaviorAgent behaviorAgent;

	public story_status current_story_status;
	public story_status update_current_story_status;
	public story_status previous_status;
	public user_input_status user_status;
	public bool box_move_flag;
	public bool soccer_play_flag;
	public bool meet_one_point;
	//public int run_status; //updated by behavior tree.

    void Start()
    {
		current_story_status = story_status.Meeting_One_Point;
		update_current_story_status = story_status.Meeting_One_Point;
		previous_status = story_status.Meeting_One_Point;
		user_status = user_input_status.None_input;
		box_move_flag = false;
		soccer_play_flag = false;

        behaviorAgent = new BehaviorAgent(BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

	void Update()
    {
		/*user_input_controller*/
        if(Input.GetKey("up")) //moving box.
			user_status = user_input_status.Box_Move;
        if(Input.GetKey("down")) //soccer play.
			user_status= user_input_status.Soccer_Play;
        else //for other input key ignore now. but probably enxtended to others.
			user_status= user_input_status.None_input;

		switch(current_story_status)
		{
            case story_status.Meeting_One_Point:
                if (user_status ==user_input_status.Box_Move) {
                    if (meet_one_point) {
                        update_current_story_status = story_status.Box_Moving;
                        print ("current state is meeting one point");
                    }
                }
                break;
            case story_status.Box_Moving:
                if (user_status==user_input_status.Soccer_Play) {
					update_current_story_status=story_status.Soccer_Playing;
				    if(!box_move_flag) // box not yet moved all.
                        previous_status=story_status.Box_Moving;

                    //update_current_story_status=story_status.Soccer_Playing;
                }
                break;
            case story_status.Soccer_Playing:
                if (soccer_play_flag==true) {
                    if (previous_status==story_status.Box_Moving)
                        update_current_story_status=story_status.Box_Moving;
                    else
                        update_current_story_status=story_status.Shaking_hands_and_Bye;
                }
                break;
            case story_status.Shaking_hands_and_Bye:
				update_current_story_status=story_status.The_End;
                break;
		}

        // update current story state. ? how the sync with node.
		current_story_status = update_current_story_status;
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
	/*
	protected Node ST_check_status(story_status story)
	{
		return new DecoratorLoop (new Sequence(Daniel.GetComponent<BehaviorMecanim>().ST_checking_status(story), new LeafWait (1000)));
	}
*/
	protected Node ST_story_one_arc()
	{
		print(" here is story one process");
		Node walkToMeetingPoint = new SequenceParallel(
            ST_ApproachAndWait(Daniel,  meetingPointDaniel),
            ST_ApproachAndWait(Richard, meetingPointRichard),
            ST_ApproachAndWait(Tom,     meetingPointTom));
		print("After walking");
		if (walkToMeetingPoint.LastStatus == RunStatus.Running)
			meet_one_point = true; //flag set.
		//Node chat = new ForEach<GameObject>((GameObject participant) => ST_Chat(participant), new [] {Daniel, Richard, Tom});

		return new Sequence (walkToMeetingPoint);// chat);
	}

	// protected Node ST_story_two_arc()
	// {
	// 	print(" here is story two process");
	// 	Node walkToMeetingPoint = new SequenceParallel(
    //         ST_ApproachAndWait(Daniel,Box1pickup),
    //         ST_ApproachAndWait(Richard,Box5pickup));
    //     /*ST_ApproachAndWait(Tom,     box1_transform)*/
	// 	print ("After walking");
	// 	if (walkToMeetingPoint.LastStatus == RunStatus.Success)
	// 		meet_one_point = true; //flag set.
	// 	//Node chat = new ForEach<GameObject>((GameObject participant) => ST_Chat(participant), new [] {Daniel, Richard, Tom});

	// 	return new Sequence (walkToMeetingPoint);// chat);
	// }

	protected Node ST_story_one() //position, and participants.
	{
		return //two of these true.
			new SelectorParallel(/*this.ST_check_status(story_status.Meeting_One_Point), */this.ST_story_one_arc(),new LeafWait(10000));
	}

	protected Node ST_story_two() //position, and participants.
	{
		print("this is story two process");
		return //any of one true.
			new SelectorParallel(/*this.ST_check_status(story_status.Box_Moving),
                //this.ST_story_two_arc(),*/
                new LeafWait(10000));
	}

    protected Node BuildTreeRoot()
    {
		return
			//new DecoratorLoop(new Sequence(this.ST_story_one(),ST_Appraoch_at_certain_raidusAndWait(Daniel,Box5.transform,2)));//,ST_story_two,ST_story_three,ST_story_four));
			/*new DecoratorLoop(new Sequence(ST_Appraoch_at_certain_raidusAndWait(Daniel, Box5.transform,2),
			                               ST_Appraoch_at_certain_raidusAndWait(Richard, Box1.transform,2)
			                               ));*/
			new DecoratorLoop(new Sequence(ST_story_one(), ST_story_two()));
    }
}
