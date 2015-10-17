using UnityEngine;
using System.Collections;

public class Mouse_Click_Agent : MonoBehaviour {

	//public bool agent_selected;
	public Camera camera;
	public Vector3 target_position;

	public bool target_setting_possible;
	public bool agent1_selected;
	public bool agent2_selected;
	public bool avatar_selected;
	//public bool agent2_selected;

	//public bool agent2_selected;
	public bool obstacle1_selected;
	public bool obstacle2_selected;


	public bool activate_multiple_click;
	public bool animation_process_flag;

	private int mulitple_click_count;
	private float mulitple_click_start_time;
	private float mulitple_click_previous_time;
	
	// Use this for initialization
	void Start () {
		agent1_selected = false;
		target_setting_possible = false;
		obstacle1_selected=false;
		obstacle2_selected=false;
		mulitple_click_count = 0;
		activate_multiple_click = false;
		//mulitple_click_time = 0; 
		animation_process_flag = false;
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
					if (hit.transform.gameObject.tag == "agent2")
					{
						//	print ("1");     
						agent2_selected = true;
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

								if (hit.transform.gameObject.tag=="Susan")
								{


									if(!activate_multiple_click) //begining of multiple click.
									{
										activate_multiple_click = true;
										//avatar_selected=true;
										mulitple_click_count=mulitple_click_count +1;
									
										mulitple_click_previous_time=Time.realtimeSinceStartup;
									}

									else  //re-enter of multiple click
									{
										if((Time.realtimeSinceStartup-mulitple_click_previous_time)<1.5)// accept as multiple click.
										{
											mulitple_click_previous_time=Time.realtimeSinceStartup; //current time update.
											mulitple_click_count= mulitple_click_count+1;
										  //update time.
										}
										else //do not accept as multiple time.
										{
										//	activate_multiple_click=false;
										//	mulitple_click_count=0;
										//	avatar_selected=false;
									   //elapsed time. process it.
											animation_process_flag=true; //other 

										}
									}


								

								}
								else{
						//print (" 3?");
						if(agent1_selected||agent2_selected)// || other selected)
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
		
		
	}
}




