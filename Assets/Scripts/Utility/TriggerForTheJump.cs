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
            GameController.GetInstance().GetMainUI().JumpButtonOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().IsCanJump(false);
            GameController.GetInstance().GetMainUI().JumpButtonOff();
        }
    }
}
