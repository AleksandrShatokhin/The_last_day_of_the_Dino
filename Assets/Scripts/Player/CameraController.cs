using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator anim_camera;

    private Transform target;
    private Vector3 offset;

    void Start()
    {
        anim_camera = GetComponent<Animator>();

        target = GameObject.Find("Player").GetComponent<Transform>();

        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
         transform.position = new Vector3(target.position.x, transform.position.y, offset.z);
         transform.LookAt(target.position);
    }

    public void ShakeCamera()
    {
        anim_camera.SetTrigger("isShake");
    }
}
