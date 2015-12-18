using UnityEngine;
using System.Collections;
using System;
using TreeSharpPlus;
using RootMotion.FinalIK;

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

    public GameObject Chris;
    public GameObject Daniel;
	public GameObject Tom;
	public GameObject Harry;
	public GameObject Virtual_human;

	/*
	public Transform Chris_hand;
	public Transform Daniel_hand;
	public Transform Tom_hand;
	public Transform Harry_hand;

	public Transform Chris_waist;
	public Transform Daniel_waist;
	public Transform Tom_waist;
	public Transform Harry_waist;
    */

	public GameObject Virtual_point;
	public GameObject First_point;
	public GameObject Second_point;
	public GameObject Third_point;
	public GameObject Fourth_point;

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

	/*
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
    }
    */

    protected Node ST_ApproachAndWait(GameObject player, Transform target)
    {
        Val<Vector3> position = Val.V (() => target.position);
        return new Sequence(
            player.GetComponent<BehaviorMecanim>().Node_GoTo(position),
            new LeafWait(1000));
    }

	protected Node ST_ApproachAndWait(GameObject player, GameObject destination)
    {
        return ST_ApproachAndWait(player, destination.transform);
    }

    protected Node ST_Meet_Wait(GameObject player, GameObject meet_position,int dist)
    {
        Val<Vector3> meet_point = Val.V (() => meet_position.transform.position);
        return new Sequence(
            player.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(meet_point, dist),
            new LeafWait(1000));
    }

	protected Node ST_make_line(GameObject player, GameObject go_postion)
	{
		Val<Vector3> point = Val.V(()=>go_postion.transform.position);
        return new Sequence(
            player.GetComponent<BehaviorMecanim>().Node_GoTo(point),
            new LeafWait(1000));
	}

	/*
	protected Node ST_make_line(GameObject player, GameObject go_position)
	{

		Val<Vector3> front_point = Val.V (() => go_position.transform.position);
		Node line = new Sequence (player.GetComponent<BehaviorMecanim>().Node_GoTo(front_point),new LeafWait(1000));
		return line;
	}
    */

	protected Node ST_SAY_Hello(GameObject player)
	{
		Val<string> hello_animation =Val.V (()=> "WAVE");
		Val<bool> set_active = Val.V(()=>true);
		return new Sequence(
            player.GetComponent<BehaviorMecanim>().Node_HandAnimation(hello_animation, set_active),
            new LeafWait(1000));
	}

	protected Node ST_make_Train(GameObject player_back, GameObject player_front) //back effector //front object. //back is object
	{
		Val<FullBodyBipedEffector> effector = Val.V (() => player_back.GetComponent<FullBodyBipedEffector>());
		Val<InteractionObject> waist = Val.V (() => player_front.GetComponent<InteractionObject>());

		return new Sequence(
            player_back.GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, waist),
            new LeafWait(1000));
	}

	protected Node BuildTreeRoot()
	{
		Func<bool> story1 = () => true; // we have not implemented state of story.

		Node meet_one_point = new SequenceParallel(
            this.ST_Meet_Wait(this.Tom, this.meetingPointChar1, 2),
            this.ST_Meet_Wait(this.Chris, this.meetingPointChar1, 2),
            this.ST_Meet_Wait(this.Harry, this.meetingPointChar1, 2),
            this.ST_Meet_Wait(this.Daniel, this.meetingPointChar1, 2));

		Node say_hi = new SequenceParallel(
            this.ST_SAY_Hello(this.Tom),
            this.ST_SAY_Hello(this.Chris),
            this.ST_SAY_Hello(this.Harry),
            this.ST_SAY_Hello(this.Daniel));

		Node make_line = new Sequence(
            this.ST_make_line(this.Tom, this.First_point),
            this.ST_make_line(this.Chris,this.Second_point),
            this.ST_make_line(this.Harry, this.Third_point),
            this.ST_make_line(this.Daniel, this.Fourth_point));

		Node make_train = new Sequence(
            this.ST_make_Train(this.Tom, this.Virtual_human),
            this.ST_make_Train(this.Chris, this.Tom),
            this.ST_make_Train(this.Harry, this.Chris),
            this.ST_make_Train(this.Daniel, this.Harry));

		Node train_play = new DecoratorLoop(new Sequence(meet_one_point, say_hi, make_line, make_train));
		Node trigger = new DecoratorLoop(new LeafAssert(story1));
		Node root_story = new DecoratorLoop(new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger, train_play)));

		return root_story;
	}
}
