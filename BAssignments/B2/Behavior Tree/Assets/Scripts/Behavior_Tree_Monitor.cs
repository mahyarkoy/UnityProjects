using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public enum story_status //story state according to the input status.
{
	Meeting_One_Point, //current object.
	Waiting_input,
	Box_Moving,
	Soccer_Playing,
	Shaking_hands_and_Bye,
	The_End,
}

public enum user_input_status
{
	None_input,
	Box_Move,
	Soccer_Play,
}

public class Behavior_Tree_Monitor : MonoBehaviour
{
	public story_status current_story_status;
	public story_status update_current_story_status;
	public story_status previous_status;
	public user_input_status user_status;
	public bool box_move_flag;
	public bool soccer_play_flag;
	public bool meet_one_point;
	//public int run_status; //updated by behavior tree.

	void Start()
    {
		current_story_status = story_status.Meeting_One_Point;
		update_current_story_status = story_status.Meeting_One_Point;
		previous_status = story_status.Meeting_One_Point;
		user_status = user_input_status.None_input;
		box_move_flag = false;
		soccer_play_flag = false;
	}

	// State Machine operate on only user input.
	void Update()
    {
		/*user_input_controller*/
        if(Input.GetKey("up")) //moving box.
			user_status = user_input_status.Box_Move;
        if(Input.GetKey("down")) //soccer play.
			user_status= user_input_status.Soccer_Play;
        else //for other input key ignore now. but probably enxtended to others.
			user_status= user_input_status.None_input;

		switch(current_story_status)
		{
            case story_status.Meeting_One_Point:
                if (user_status ==user_input_status.Box_Move) {
                    if (meet_one_point) {
                        update_current_story_status = story_status.Box_Moving;
                        print ("current state is meeting one point");
                    }
                }
                break;
            case story_status.Box_Moving:
                if (user_status==user_input_status.Soccer_Play) {
					update_current_story_status=story_status.Soccer_Playing;
				    if(!box_move_flag) // box not yet moved all.
                        previous_status=story_status.Box_Moving;

                    //update_current_story_status=story_status.Soccer_Playing;
                }
                break;
            case story_status.Soccer_Playing:
                if (soccer_play_flag==true) {
                    if (previous_status==story_status.Box_Moving)
                        update_current_story_status=story_status.Box_Moving;
                    else
                        update_current_story_status=story_status.Shaking_hands_and_Bye;
                }
                break;
            case story_status.Shaking_hands_and_Bye:
				update_current_story_status=story_status.The_End;
                break;
		}

        // update current story state. ? how the sync with node.
		current_story_status = update_current_story_status;
	}
}
