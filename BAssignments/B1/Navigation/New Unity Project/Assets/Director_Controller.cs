using UnityEngine;
using System.Collections;

public class Director_Controller : MonoBehaviour {

	
	public  Mouse_Click_Agent  mouse_clik_tracking;
	public  GameObject agent1;
	public  GameObject agent2;

	
	// Use this for initialization
	void Start () {
		//agent1_processed = false;
	}
	
	// Update is called once per frame
	void Update () {
		//print ("2");
		if (mouse_clik_tracking.agent1_selected == true && mouse_clik_tracking.target_setting_possible==true) { //only trying to set the target then it is true.
			//send current postion.
			//print ("4?");
			agent1.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); //can I desgine...objet.
			mouse_clik_tracking.agent1_selected=false;
			mouse_clik_tracking.target_setting_possible=false;
			//mouse_clik_tracking.agent1_selected=false; //here can be changed?
			
		}


		if (mouse_clik_tracking.agent2_selected == true && mouse_clik_tracking.target_setting_possible==true) { //only trying to set the target then it is true.
			//send current postion.
			print ("agent2");
			agent2.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); //can I desgine...objet.
			mouse_clik_tracking.agent2_selected=false;
			mouse_clik_tracking.target_setting_possible=false;
			//mouse_clik_tracking.agent1_selected=false; //here can be changed?
			
		}

		if (mouse_clik_tracking.animation_process_flag) {
			//speed = mouse_clik_tracking.mulitple_click_count;
			mouse_clik_tracking.animation_process_flag=false;
			mouse_clik_tracking.activate_multiple_click=false;
		}

			
		//	agent2.BroadcastMessage("agent_path",mouse_clik_tracking.target_position); //can I desgine...objet.
			
		//}
	}
}

//void void agent_path(Vector3 target_position )(Vector3 target_position )