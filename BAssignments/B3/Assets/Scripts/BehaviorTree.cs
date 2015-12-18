using UnityEngine;
using System.Collections;
using System;
using TreeSharpPlus;

public enum StoryStatus //story state according to the input status.
{
    Meeting,
    Train,
    Butterfly,
    Mom,
    Fall,
    Car,
    GoHome,
    Accident,
    Cry,
    TheEnd
}

public enum InputStatus
{
    None,
    Butterfly,
    Mom,
    Fall,
    Car
}

public class BehaviorTree : MonoBehaviour
{
	public GameObject  meetingPointChar1;
	//GameObject meetingPointChar2;
    //public Transform meetingPointChar3;
    //public Transform meetingPointChar4;

    public GameObject Chris;
    public GameObject Daniel;
	public GameObject Tom;
	public GameObject Harry;
   //public GameObject Char3;
    //public GameObject Char4;

    private BehaviorAgent behaviorAgent;

    public StoryStatus story_status;
    public StoryStatus previous_status;
    public InputStatus input;

    public bool meet_one_point;

    void Start()
    {
        story_status    = StoryStatus.Meeting;
        previous_status = StoryStatus.Meeting;

        input = InputStatus.None;

        behaviorAgent = new BehaviorAgent(BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    void Update()
    {
        if (Input.GetKey("up"))
            input = InputStatus.Butterfly;
        else if (Input.GetKey("down"))
            input = InputStatus.Mom;
        else if (Input.GetKey("left"))
            input = InputStatus.Fall;
        else if (Input.GetKey("right"))
            input = InputStatus.Car;
        else
            input = InputStatus.None;

        switch (story_status)
        {
            case StoryStatus.Meeting:
                story_status = StoryStatus.Train;
                break;

            case StoryStatus.Train:
                if (input == InputStatus.Butterfly)
                    story_status = StoryStatus.Butterfly;
                else if (input == InputStatus.Mom)
                    story_status = StoryStatus.Mom;
                else if (input == InputStatus.Fall)
                    story_status = StoryStatus.Fall;
                else if (input == InputStatus.Car)
                    story_status = StoryStatus.Car;
                break;

            case StoryStatus.Butterfly:
                break;

            case StoryStatus.Mom:
                break;

            case StoryStatus.Fall:
                break;

            case StoryStatus.Car:
                break;

            case StoryStatus.GoHome:
                story_status = StoryStatus.TheEnd;
                break;

            case StoryStatus.Accident:
                story_status = StoryStatus.TheEnd;
                break;

            case StoryStatus.Cry:
                story_status = StoryStatus.TheEnd;
                break;

            case StoryStatus.TheEnd:
                break;
        }
    }

    protected Node ST_ApproachAndWait(GameObject participants, Transform target)
    {
        Val<Vector3> position = Val.V (() => target.position);
        return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
	/*
    protected Node ST_Appraoch_at_certain_raidusAndWait(GameObject participants, Transform target, float radius)
    {
        Val<Vector3> position = Val.V (() => target.position);
        Val<float> dist=Val.V (()=>radius );

        return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position,dist), new LeafWait(1000));
    }

    protected Node ST_Chat(GameObject participants)
    {
        //return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_BodyAnimation("ADAPTMan@shaking_head_no", true), new LeafWait(1000));
        // Val<string> gesture = Val.V (() => "ADAPTMan@shaking_head_no");
        // Val<long> duration = Val.V (() => 1000L);
        Val<Vector3> target = Val.V (() => new Vector3(0, 0, 0));

        // return new DecoratorLoop(new SequenceShuffle(participants.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(gesture, duration), new LeafWait(1000)));
        // return new SequenceShuffle(participants.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(gesture, duration), new LeafWait(1000));
        return new SequenceShuffle(participants.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
    }

    protected Node ST_story_one()
    {
        Node walkToMeetingPoint = new SequenceParallel(
            ST_ApproachAndWait(Chris, meetingPointChar1),
			ST_ApproachAndWait(Daniel, meetingPointChar2),
            );

        if (walkToMeetingPoint.LastStatus == RunStatus.Running)
            meet_one_point = true;

        return new SelectorParallel(new Sequence(walkToMeetingPoint), new LeafWait(10000));
    }

    protected Node ST_story_two()
    {
        Node walkToMeetingPoint = new SequenceParallel(
            ST_ApproachAndWait(Char1, meetingPointChar1),
            ST_ApproachAndWait(Char2, meetingPointChar2),
            ST_ApproachAndWait(Char3, meetingPointChar3),
            ST_ApproachAndWait(Char4, meetingPointChar4));

        return new SelectorParallel(new Sequence(walkToMeetingPoint), new LeafWait(10000));
    }*/

	protected Node ST_ApproachAndWait(GameObject player, GameObject meet_position)
    {
        Val<Vector3> position = Val.V (() => meet_position.transform.position);
        return new Sequence( player.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    protected Node ST_Meet_Wait(GameObject player, GameObject meet_position,int dist)
    {
        Val<Vector3> meet_point = Val.V (() => meet_position.transform.position);
		Node meet_to_one = new Sequence(player.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(meet_point,dist),new LeafWait(1000));
        return meet_to_one;
    }

	protected Node ST_SAY_Hello(GameObject player)
	{
		
		Val<string> hello_animation =Val.V (()=> "WAVE");
		Val<bool> set_active = Val.V(()=>true);
		Node say_hi = new Sequence(player.GetComponent<BehaviorMecanim>().Node_HandAnimation(hello_animation,set_active), new LeafWait(1000));
				return say_hi;
	}

    
	protected Node BuildTreeRoot() 
	{

		Func<bool> story1  = () => true; // we have not implemented state of story.

		Node meet_one_point = new SequenceParallel(this.ST_Meet_Wait(this.Tom,this.meetingPointChar1,2),this.ST_Meet_Wait(this.Chris,this.meetingPointChar1,2),this.ST_Meet_Wait(this.Harry,this.meetingPointChar1,2),this.ST_Meet_Wait(this.Daniel,this.meetingPointChar1,2));
					Node say_hi = new DecoratorLoop( new SequenceParallel(this.ST_SAY_Hello(this.Tom),this.ST_SAY_Hello(this.Daniel),this.ST_SAY_Hello(this.Chris),this.ST_SAY_Hello(this.Harry)));
	
		Node train_play  = new DecoratorLoop (

			new Sequence(meet_one_point,say_hi));
		Node trigger = new DecoratorLoop (new LeafAssert (story1));
		Node root_story = new DecoratorLoop (new DecoratorForceStatus (RunStatus.Success, new SequenceParallel(trigger,train_play)));

		return root_story;

	}
}


					