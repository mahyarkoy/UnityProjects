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

    private BehaviorAgent behaviorAgent;

    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    void Update()
    {
    }

    protected Node ST_ApproachAndWait(GameObject participants,Transform target)
    {
        Val<Vector3> position = Val.V (() => target.position);
        return new Sequence(participants.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
    {
        return
            //new DecoratorLoop(
            new SequenceParallel(this.ST_ApproachAndWait(this.Daniel,  this.meetingPointDaniel),
                                 this.ST_ApproachAndWait(this.Richard, this.meetingPointRichard),
                                 this.ST_ApproachAndWait(this.Tom,     this.meetingPointTom));
    }
}

