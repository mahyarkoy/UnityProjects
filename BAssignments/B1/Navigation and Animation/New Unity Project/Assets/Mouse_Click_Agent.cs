using UnityEngine;
using System.Collections;

public class Mouse_Click_Agent : MonoBehaviour {

	//public bool agent_selected;
	public Camera camera;
	public Vector3 target_position;

	public bool target_setting_possible;
	public bool agent1_selected;
	//public bool agent2_selected;

	//public bool agent2_selected;
	public bool obstacle1_selected;
	public bool obstacle2_selected;
	
	
	// Use this for initialization
	void Start () {
		agent1_selected = false;
		target_setting_possible = false;
		obstacle1_selected=false;
		obstacle2_selected=false;
		//agent2_selected = false;
		//obstacle_selected = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			
			if (Physics.Raycast (camera.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				if (hit.transform.gameObject.tag == "agent1")
				{
				//	print ("1");     
					agent1_selected = true;
					//target_position = hit.point; 
				}

			    
				else
				{
					if (hit.transform.gameObject.tag=="door1")
					{
						obstacle1_selected=true;
						obstacle2_selected=false;
					}
					else 
					{   
						if(hit.transform.gameObject.tag=="door2")
						{
							obstacle2_selected=true;
							obstacle1_selected=false;
						}
						else{	
						//print (" 3?");
						if(agent1_selected)// || other selected)
						{
							target_position = hit.point;
							target_setting_possible=true; //other selected then . deactivate.
								obstacle1_selected=false;//print ("3?location");
								obstacle2_selected=false;
						}
						}
					}

				}

				
			}
			
		}
		
		
	}
}




