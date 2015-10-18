using UnityEngine;
using System.Collections;

public class P3_Agent4_Controller : MonoBehaviour {
	private NavMeshAgent nav_agent;
	private NavMeshPath path;
	private bool set_destination;
	private new Vector3 offset;
	public Mouse_Click_Agent mouse_controller;
	
	
	// Use this for initialization
	void Start () {
		nav_agent = GetComponent<NavMeshAgent> ();
		path = new NavMeshPath (); 
		set_destination = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		//checking the path completion.
		if (mouse_controller.destination_go&&mouse_controller.state_of_agent==4&&set_destination==true)
		{
			offset=transform.position-nav_agent.destination;
			if(offset.sqrMagnitude <0.2) //close to goal
			{
				mouse_controller.state_of_agent=0; //become idle.
				mouse_controller.number_of_clicks=0; //initialize.
				mouse_controller.destination_go=false;
				set_destination=false;
				//print ("2 are you here?");
			}
			
		}
	}
	
	void agent_path(Vector3 target_position )
	{
		//print ("are you set destination alreaady");
		nav_agent.destination=target_position;
		set_destination = true;
	}
}