using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForTheJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().IsCanJump(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().IsCanJump(false);
        }
    }
}
