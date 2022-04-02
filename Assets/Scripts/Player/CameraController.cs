using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();

        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, offset.z);
        transform.LookAt(target.position);
    }
}
