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
        endPos = GameObject.Find("PS_LittleSmoke").GetComponent<Transform>().position;
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
            GameController.GetInstance().Counter(1);
            Destroy(this.gameObject);
        }
    }
}
