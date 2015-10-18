using UnityEngine;
using System.Collections;

public class navcharscript : MonoBehaviour {

	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast (ray, out hit, 100))
			{
				agent.SetDestination(hit.point);
			}
		}

	}
	

}
