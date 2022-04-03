using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNoTrigger : MonoBehaviour
{
    private Vector3 startPos, endPos;
    private float step;

    void Start()
    {
        startPos = transform.position;
        endPos = GameObject.Find("LittleSmoke").GetComponent<Transform>().position;
        step = 0;
    }

    void Update()
    {
        if (step <= 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, step);
            step += 0.03f;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
