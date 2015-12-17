using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class Movement_BehaviorTree : MonoBehaviour
{
	public Transform meetingPointDaniel;
	public Transform meetingPointRichard;
	public Transform meetingPointTom;

	public GameObject Daniel;
	public GameObject Richard;
	public GameObject Tom;
	public Story_Monitor story;

	private BehaviorAgent behaviorAgent;


	void Start()
	{
		behaviorAgent = new BehaviorAgent(BuildTreeRoot());
		BehaviorManager.Instance.Register(behaviorAgent);
		behaviorAgent.StartBehavior();
	}

	void Update()
	{
	}
	/*
    protected Node ST_ApproachAndWait(GameObject participants, Transform target)
    {
        Val<Vector3> position = Val.V (() => target.position);
        return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_Chat(GameObject participants)
    {
        return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_BodyAnimation("ADAPTMan@shaking_head_no", true), new LeafWait(1000));
    }
*/
	protected Node BuildTreeRoot()
	{/*
        Node walkToMeetingPoint = new SequenceParallel(
            ST_ApproachAndWait(Daniel,  meetingPointDaniel),
            ST_ApproachAndWait(Richard, meetingPointRichard),
            ST_ApproachAndWait(Tom,     meetingPointTom));

        Node chat = new ForEach<GameObject>((GameObject participant) => ST_Chat(participant), new [] {Daniel, Richard, Tom});

        return new Sequence(walkToMeetingPoint, chat);*/
		print ("this is root");

		Node root= story.B2_ST_Story_selector();
		print ("after root node");
		return root;
	}
}

