using UnityEngine;
using System.Collections;

public class MouseUI : MonoBehaviour
{
    public Camera camera;

	void Start()
    {
    }

	void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                switch (hit.transform.gameObject.tag) {
                    case "Daniel":
                        print("Daniel selected");
                        break;
                    case "Chris":
                        print("Chris selected");
                        break;
                    case "Harry":
                        print("Harry selected");
                        break;
                    case "Tom":
                        print("Tom selected");
                        break;
                }
            }
        }
	}
}
