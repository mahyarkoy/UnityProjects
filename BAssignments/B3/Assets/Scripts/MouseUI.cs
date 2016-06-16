using UnityEngine;
using System.Collections;

public class MouseUI : MonoBehaviour
{
    public Camera camera;

    public GameObject Chris;
    public GameObject Daniel;
	public GameObject Tom;
	public GameObject Harry;

	public GameObject SelectorIndicator;

	void Start()
    {
    }

	void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 10000)) {
                switch (hit.transform.gameObject.tag) {
                    case "Daniel":
                        SelectorIndicator.transform.parent = Daniel.transform;
                        SelectorIndicator.active = true;
                        break;
                    case "Chris":
                        SelectorIndicator.transform.parent = Chris.transform;
                        SelectorIndicator.active = true;
                        break;
                    case "Harry":
                        SelectorIndicator.transform.parent = Harry.transform;
                        SelectorIndicator.active = true;
                        break;
                    case "Tom":
                        SelectorIndicator.transform.parent = Tom.transform;
                        SelectorIndicator.active = true;
                        break;
                    default:
                        SelectorIndicator.active = false;
                        break;
                }
            }
        }
	}
}
