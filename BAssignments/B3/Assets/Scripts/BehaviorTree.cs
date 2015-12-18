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

	public GameObject linePoint1;
	public GameObject linePoint2;
	public GameObject linePoint3;
	public GameObject linePoint4;

    public GameObject trainPoint1;
	public GameObject trainPoint2;
	public GameObject trainPoint3;
	public GameObject trainPoint4;
	public GameObject trainPoint5;

    private BehaviorAgent behaviorAgent;
	private Func<bool> trainStory = () => true;
	private Func<bool> carStory = () => false;
	private Func<bool> momStory = () => false;
	private Func<bool> butterflyStory = () => false;
	private Func<bool> fallstory = ()=> false;

    public StoryStatus story_status;
    public StoryStatus previous_status;
    public InputStatus input;

    void Start()
    {
		trainStory = () => true;
		carStory = () => false;
		momStory = () => false;
		butterflyStory = () => false;
		fallstory = ()=> false;

        story_status    = StoryStatus.Meeting;
        previous_status = StoryStatus.Meeting;

        input = InputStatus.None;

        behaviorAgent = new BehaviorAgent(BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    void Update()
    {
		if (Input.GetKey ("up"))
			input = InputStatus.Butterfly;
		else if (Input.GetKey ("down"))
			input = InputStatus.Mom;
		else if (Input.GetKey ("left")) {
			print (" are you left");
			input = InputStatus.Fall;
		}
        else if (Input.GetKey("right"))
            input = InputStatus.Car;
        else
            input = InputStatus.None;

        switch (story_status)
        {
		case StoryStatus.Meeting:
                story_status = StoryStatus.Train;
			    trainStory = () => true;

                break;

		case StoryStatus.Train:
			print (input);
			if (input == InputStatus.Butterfly) {
				story_status = StoryStatus.Butterfly;
				trainStory = () => false;
				carStory = () => false;
				momStory = () => false;
				butterflyStory = () => true;

			} else if (input == InputStatus.Mom)
				story_status = StoryStatus.Mom;
			  else if (input == InputStatus.Fall) {
				print (" are you fall down");
				trainStory = () => false;
				carStory = () => false;
				momStory = () => false;
				butterflyStory = () => false;
				fallstory = ()=> true;

				story_status = StoryStatus.Fall;
			}
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
        Val<float> dist = Val.V (() => 0.5f);
        return new Sequence(
            player.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(point, dist),
            new LeafWait(100));
	}

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

	protected Node ST_others_follow(GameObject target)
    {
        Val<Vector3> destination = Val.V (() => target.transform.position);

		return new SequenceParallel(
            new DecoratorLoop(Chris.GetComponent<BehaviorMecanim>().Node_GoTo(destination)),
            new DecoratorLoop(Harry.GetComponent<BehaviorMecanim>().Node_GoTo(destination)),
            new DecoratorLoop(Daniel.GetComponent<BehaviorMecanim>().Node_GoTo(destination)));
    }

	protected Node ST_make_Train()
    {
        Val<Vector3> point1 = Val.V (() => trainPoint1.transform.position);
        Val<Vector3> point2 = Val.V (() => trainPoint2.transform.position);
        Val<Vector3> point3 = Val.V (() => trainPoint3.transform.position);
        Val<Vector3> point4 = Val.V (() => trainPoint4.transform.position);
        Val<Vector3> point5 = Val.V (() => trainPoint5.transform.position);

        Val<float> dist = Val.V (() => 1.0f);

		return new DecoratorLoop(new Sequence(
            new SelectorParallel(Tom.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(point1, dist), ST_others_follow(Tom)),
            new SelectorParallel(Tom.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(point2, dist), ST_others_follow(Tom)),
            new SelectorParallel(Tom.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(point3, dist), ST_others_follow(Tom)),
            new SelectorParallel(Tom.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(point4, dist), ST_others_follow(Tom)),
            new SelectorParallel(Tom.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(point5, dist), ST_others_follow(Tom))));
    }
	protected Node ST_Fall_Down(GameObject player)
	{
		Val<string> fall_down =Val.V (()=> "FALL_DOWN");
		Val<bool> set_active = Val.V(()=>true);
		return new Sequence(
			player.GetComponent<BehaviorMecanim>().Node_HandAnimation(fall_down, set_active),
			new LeafWait(1000));
	}
	protected Node BuildTreeRoot()
	{
        // TODO: story state is not yet implemented
	

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
            this.ST_make_line(this.Tom, this.linePoint1),
            this.ST_make_line(this.Chris,this.linePoint2),
            this.ST_make_line(this.Harry, this.linePoint3),
            this.ST_make_line(this.Daniel, this.linePoint4));

		Node fall_down = this.ST_Fall_Down(this.Tom); //randomly chooose any one of the character.

		Node train_play = new DecoratorLoop(new Sequence(meet_one_point, say_hi, make_line, ST_make_Train()));
		Node Fall_story = new DecoratorLoop(new Sequence(fall_down));
		Node trigger_train = new DecoratorLoop(new LeafAssert(trainStory));
		Node trigger_fall = new DecoratorLoop(new LeafAssert(fallstory));

		Node train_story_arc = new  SelectorParallel(trigger_train, train_play);
		Node fall_story_arc = new  SelectorParallel(trigger_fall, Fall_story);
		Node root_story = new DecoratorLoop(new DecoratorForceStatus(RunStatus.Success, new Sequence(train_story_arc, fall_story_arc)));

		return root_story;
	}
}
