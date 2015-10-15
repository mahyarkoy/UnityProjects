using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentController : MonoBehaviour {

	public float devilDistance;

	private Renderer rend;
	private NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.CompareTag("select"))
			rend.material.color = Color.green;
		else
			rend.material.color = Color.red;
		detectDevil ();
	}

	void detectDevil() {
		GameObject[] allDevils = GameObject.FindGameObjectsWithTag ("devil");
		List<GameObject> localDevils = new List<GameObject> ();
		foreach (GameObject dev in allDevils) {
			if(Vector3.Distance(this.transform.position, dev.transform.position)<devilDistance && !localDevils.Contains(dev)) {
				localDevils.Add(dev);
			}
		}
		if (localDevils.Count <= 0)
			return;
		foreach (GameObject dev in localDevils) {
			dev.GetComponent<NavMeshObstacle>().carving = true;
		}
	
		Vector3 tempDest = nav.destination;
		nav.ResetPath ();
		nav.SetDestination (tempDest);
	}
}
