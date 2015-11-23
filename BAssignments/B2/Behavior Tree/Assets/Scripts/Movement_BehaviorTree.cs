using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class Movement_BehaviorTree : MonoBehaviour
{
    public Transform wander1;
    //public Transform wander2;
    //public Transform wander3;
    public GameObject Daniel;
    public GameObject Tom;
    public GameObject Richard;

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
            //new DecoratorLoop (
            new SequenceParallel(this.ST_ApproachAndWait (this.Daniel, this.wander1),this.ST_ApproachAndWait (this.Tom, this.wander1),this.ST_ApproachAndWait (this.Richard, this.wander1));
                /*;
                , this.ST_ApproachAndWait (this.Tom, this.wander1), this.ST_ApproachAndWait (this.Richard, this.wander1)));
                /*new DecoratorLoop(
                new SequenceShuffle(
                this.ST_ApproachAndWait(this.wander1)));
                //this.ST_ApproachAndWait(this.wander2),
                //this.ST_ApproachAndWait(this.wander3)));*/
    }
}

