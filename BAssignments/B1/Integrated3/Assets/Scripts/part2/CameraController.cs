using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float distanceAway;
    public float distanceUp;

    public Transform target;

    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
        transform.LookAt(target);
        transform.Rotate(new Vector3(-20, 0, 0));
    }
}
