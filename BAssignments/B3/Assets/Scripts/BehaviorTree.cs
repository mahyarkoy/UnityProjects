using UnityEngine;
using System.Collections;
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
    public Transform meetingPointDaniel;
    public Transform meetingPointRichard;
    public Transform meetingPointTom;

    public GameObject Daniel;
    public GameObject Richard;
    public GameObject Tom;

    private BehaviorAgent behaviorAgent;

	public StoryStatus story_status;
	public StoryStatus previous_status;
	public InputStatus input;

	public bool meet_one_point;

    void Start()
    {
		story_status = StoryStatus.Meeting;
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
	protected Node ST_check_status(StoryStatus story)
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
			new SelectorParallel(/*this.ST_check_status(StoryStatus.Meeting), */this.ST_story_one_arc(),new LeafWait(10000));
	}

	protected Node ST_story_two() //position, and participants.
	{
		print("this is story two process");
		return //any of one true.
			new SelectorParallel(/*this.ST_check_status(StoryStatus.Box_Moving),
                //this.ST_story_two_arc(),*/
                new LeafWait(10000));
	}

    protected Node BuildTreeRoot()
    {
		return new DecoratorLoop(new Sequence(ST_story_one(), ST_story_two()));
    }
}
