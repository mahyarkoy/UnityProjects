using UnityEngine;
using System.Collections;

public class P3_Director_Controller : MonoBehaviour {

	
	public  Mouse_Click_Agent  mouse_clik_tracking;
	public  GameObject agent1;
	public  GameObject agent2;
	public  GameObject agent3;
    public  GameObject agent4;
	//public  GameObject agent5;

	private enum agent {agent_0= 0, agent_1 = 1, agent_2=2, agent_3=3, agent_4=4, agent_5=5};
	
	// Use this for initialization
	void Start () {
		//agent1_processed = false;
	}
	
	// Update is called once per frame
	void Update () {
		//go 
	if (mouse_clik_tracking.destination_go) {
			print(mouse_clik_tracking.state_of_agent);
			switch (mouse_clik_tracking.state_of_agent){
			case 1: agent1.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); break;
			case 2: agent2.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); break;
			case 3: agent3.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); break;
			case 4: agent4.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); break;
			// case agent.agent_5: agent5.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); break;
			default: print ("you should not be here");break;
			}

		}
	}
}





