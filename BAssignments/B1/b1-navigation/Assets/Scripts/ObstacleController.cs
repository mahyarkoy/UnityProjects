using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public Mouse_Click_Agent mouse_sig;
    public int obstacleNum;

	private int speed = 10;

	void Update() {
		if (mouse_sig.selected_obstacle == obstacleNum) {
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * -1;
			transform.Translate(movement * speed * Time.deltaTime);
		}
	}
}

