using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	const float AGENTDEFAULTSPEED = 3.5f;

	public Texture2D selectionTexture = null;

	private RaycastHit myHit = new RaycastHit();
	private Ray myRay = new Ray();
	private GameObject select;
	private GameObject righClickObj;
	private float selectStartPosX;
	private float selectStartPosY;
	private float selectEndPosX;
	private float selectEndPosY;
	private Rect selectionBox;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/*if (Input.GetMouseButtonDown (0)) {
			myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			select = GameObject.FindWithTag("select");
			if(select!=null) {
				select.tag = "none";
			}
			if (Physics.Raycast (myRay, out myHit, 100.0f)){ //hit
				if(myHit.collider.transform.CompareTag("level")) {
					return;
				}
				myHit.collider.transform.tag = "select";
			}
		}*/


		if (Input.GetMouseButtonDown (0)) {
			selectStartPosX = Input.mousePosition.x;
			selectStartPosY = Input.mousePosition.y;
		} 
		else if (Input.GetMouseButtonUp (0)) {
			selectEndPosX = Input.mousePosition.x;
			selectEndPosY = Input.mousePosition.y;

			//make all start coordinates less than end coordinates
			if(selectEndPosX < selectStartPosX) {
				float temp = selectEndPosX;
				selectEndPosX = selectStartPosX;
				selectStartPosX = temp;
			}
			if(selectEndPosY < selectStartPosY) {
				float temp = selectEndPosY;
				selectEndPosY = selectStartPosY;
				selectStartPosY = temp;
			}

			//Deselect all objects
			GameObject[] selected = GameObject.FindGameObjectsWithTag("select");
			for(int i=0; i<selected.Length; i++) {
				selected[i].tag = "none";
			}

			for(float i=selectStartPosX; i<=selectEndPosX; i=i+20) {
				for(float j=selectStartPosY; j<=selectEndPosY; j=j+20) {
					Vector3 testRay = new Vector3(i,j,0);
					myRay = Camera.main.ScreenPointToRay (testRay);
					if (Physics.Raycast (myRay, out myHit, 100.0f)){ //hit
						if(!myHit.collider.transform.CompareTag("none")) {
							continue;
						}
						myHit.collider.transform.tag = "select";
					}
				}
			}
		}
		else if (Input.GetMouseButtonDown (1)) {
			/*select = GameObject.FindWithTag("select");
			if(select==null) {
				return;
			}
			myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (myRay, out myHit, 100.0f)){ //hit
				if(myHit.collider.transform.CompareTag("level")) {
					NavMeshAgent agent = select.GetComponent<NavMeshAgent>();
					agent.destination = myHit.point;
				}
			}*/

			GameObject[] selected = GameObject.FindGameObjectsWithTag("select");
			myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (myRay, out myHit, 100.0f)){ //hit
				if(myHit.collider.transform.CompareTag("level")) {
					for(int i=0; i<selected.Length; i++) {
						NavMeshAgent agent = selected[i].GetComponent<NavMeshAgent>();
						if(agent != null) {
							if(Vector3.Distance(agent.destination, myHit.point) < 1) { //Dont change destination, double the speed
								agent.speed = AGENTDEFAULTSPEED * 2;
							}
							else {
								agent.speed = AGENTDEFAULTSPEED;
								selected[i].GetComponent<AgentController>().currDest = myHit.point;
								selected[i].GetComponent<AgentController>().routing = true;
								agent.destination = myHit.point;
							}
						}
					}
				}
			}
		}

		if(Input.GetMouseButton(0)) {
			selectionBox = new Rect(selectStartPosX, Screen.height - selectStartPosY, Input.mousePosition.x - selectStartPosX, selectStartPosY - Input.mousePosition.y);
		}
	}

	private void OnGUI() {
		if (Input.GetMouseButton (0)) {
			GUI.color = new Color (0, 1, 0, 0.3f);
			GUI.DrawTexture (selectionBox, selectionTexture);
		}
	}
}
