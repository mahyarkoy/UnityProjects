using UnityEngine;
using System.Collections;

public class Mouse_Click_Agent : MonoBehaviour {

	//public bool agent_selected;
	public Camera camera;
	public bool destination_go;
	public bool obstacle1_flag;
	public bool obstacle2_flag;
	public Vector3 target_position;

	public int number_of_clicks;
	public int state_of_agent; //0 idle
	private enum agent {agent_0= 0, agent_1 = 1, agent_2=2, agent_3=3, agent_4=4, agent_5=5,destination,obstacle_1,obstacle_2};
	private float current_time;
	private float double_click_validity=1.5f;
	private RaycastHit hit;

    void Start(){
		state_of_agent = 0;
		destination_go = false;
		current_time = 0;
		obstacle1_flag = false;
		obstacle2_flag = false;
	}


	void Update(){
		if (Input.GetMouseButtonDown (0)) {

			
			if (Physics.Raycast (camera.ScreenPointToRay (Input.mousePosition), out hit, 100)) {



				switch (hit.transform.gameObject.tag) {
				case "agent1":
					obstacle1_flag=false;
					obstacle2_flag=false;
					if (state_of_agent!=(int)agent.agent_1 && state_of_agent!=(int)agent.destination)
						new_agent_state((int)agent.agent_1);

					if(state_of_agent==(int)agent.agent_1)
					    if (check_mulitple_click())
					{
						number_of_clicks=number_of_clicks+1;
					}
						else
					{
						number_of_clicks=1; 	
					}
				
				
					 
					//state_mouse (agent_1);
					break;
				case "agent2":
					obstacle1_flag=false;
					obstacle2_flag=false;
					//print("agent2");
					if (state_of_agent!=(int)agent.agent_2 && state_of_agent!=(int)agent.destination)
						new_agent_state((int)agent.agent_2);
					
					if(state_of_agent==(int)agent.agent_2)
						if (check_mulitple_click())
					{
						print ("increasing number of clicks");
						number_of_clicks=number_of_clicks+1;
					}
					else
					{
						number_of_clicks=1; 
					}

					
					
					//sta
					break;
				case "agent3":
					obstacle1_flag=false;
					obstacle2_flag=false;
					if (state_of_agent!=(int)agent.agent_3 && state_of_agent!=(int)agent.destination)
						new_agent_state((int)agent.agent_3);
					
					if(state_of_agent==(int)agent.agent_3)
						if (check_mulitple_click())
					{
						number_of_clicks=number_of_clicks+1;
					}
					else
					{
						number_of_clicks=1; 
					}

					
					
					//sta
					break;
				case "agent4":
					obstacle1_flag=false;
					obstacle2_flag=false;
					if (state_of_agent!=(int)agent.agent_4&& state_of_agent!=(int)agent.destination)
						new_agent_state((int)agent.agent_4);
					
					if(state_of_agent==(int)agent.agent_4)
						if (check_mulitple_click())
					{
						number_of_clicks=number_of_clicks+1;
					}
					else
					{
						number_of_clicks=1; 
					}
				
					break;
				case "agent5":
					obstacle1_flag=false;
					obstacle2_flag=false;
					if (state_of_agent!=(int)agent.agent_5 && state_of_agent!=(int)agent.destination)
						new_agent_state((int)agent.agent_5);
					
					if(state_of_agent==(int)agent.agent_5)
						if (check_mulitple_click())
					{
						number_of_clicks=number_of_clicks+1;
					}
					else
					{
						number_of_clicks=1; 
					}
					break;
				case "obstacle1":
						obstacle2_flag=false;
						obstacle1_flag=true;
						break;

				case "obstacle2":
						obstacle1_flag=false;
						obstacle2_flag=true;
						break;

					
					//sta
					break;
				default: //destination. //click the target.
					obstacle1_flag=false;
					obstacle2_flag=false;
					if (state_of_agent==(int)agent.agent_0)

						;// do nothing.

					else
					{
						go_destination_state ();
						print(state_of_agent);
					}
					break;
				}

			}

		}

	}
	//return to idel state only when completing of path.

	void new_agent_state(int input)
	{

		state_of_agent = input; // new state update
		current_time = Time.realtimeSinceStartup; // current time.
		number_of_clicks = number_of_clicks + 1;


	}

	bool check_mulitple_click ( )
	{
		bool multiple_check_validity = false;
		print (" come?");
		if (Time.realtimeSinceStartup - current_time < 2) //time_eslapse. 1.5'
			print ("um?");
			multiple_check_validity = true;
		return multiple_check_validity;
	}

	void go_destination_state()
	{
		destination_go=true; //we can go.
		target_position=hit.point; //where to go.
	}
}




	

	