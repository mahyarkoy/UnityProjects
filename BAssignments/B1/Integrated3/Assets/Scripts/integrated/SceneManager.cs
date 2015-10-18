using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	void Start()
    {
	}

	void Update()
    {
	}

    public void loadScene(int sceneNum)
    {
        if (sceneNum == 1) {
            Application.LoadLevel("part1");
        } else if (sceneNum == 2) {
            Application.LoadLevel("part2");
        } else if (sceneNum == 3) {
            Application.LoadLevel("part3");
        }
    }
}
