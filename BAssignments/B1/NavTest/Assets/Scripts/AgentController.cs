using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentController : MonoBehaviour {

	public Color unselectedColor;
	public Color selectedColor;
	public Vector3 currDest;
	public bool routing;
	public float devilDistance;

	private Renderer rend;
	private NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		nav = GetComponent<NavMeshAgent> ();
		currDest = transform.position;
		routing = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(this.CompareTag("select"))
			rend.material.color = selectedColor;
		else
			rend.material.color = unselectedColor;
		if (Vector3.Distance (currDest, transform.position) <= 2f) {
			Debug.Log ("at dest");
			routing = false;
			nav.ResetPath();
		}
		detectDevil ();
		if (nav.velocity == Vector3.zero && routing == true) {
			Debug.Log ("reroute");
			nav.SetDestination(currDest);
		}
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
