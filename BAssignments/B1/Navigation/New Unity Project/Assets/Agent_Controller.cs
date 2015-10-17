using UnityEngine;
using System.Collections;

public class Agent_Controller : MonoBehaviour {

	
	private NavMeshAgent nav_agent;
	// Use this for initialization
	void Start () {
		nav_agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void agent_path(Vector3 target_position )
	{
		nav_agent.destination=target_position;
		//print (" I'm called");
	}
	
}
