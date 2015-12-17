using UnityEngine;
using System.Collections;

public class DevilController : MonoBehaviour {

	public float startX, startY, startZ, endX, endY, endZ;
	public float distPerTime;
	private Vector3 startPos, endPos;
	private int travelState;
	private NavMeshObstacle nav;

	// Use this for initialization
	void Start () {
		startPos = new Vector3 (startX, startY, startZ);
		endPos = new Vector3 (endX, endY, endZ);
		transform.position = startPos;
		travelState = 0;
		nav = GetComponent<NavMeshObstacle> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.Equals (endPos))
			travelState = 1;
		else if (transform.position.Equals (startPos))
			travelState = 0;
		if (travelState == 0) {
			transform.position = Vector3.MoveTowards (transform.position, endPos, distPerTime);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, startPos, distPerTime);
		}
	}

	void LateUpdate() {
		nav.carving = false;
	}
}
