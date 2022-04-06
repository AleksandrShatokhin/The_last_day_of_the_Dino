using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForTheThrow : MonoBehaviour
{
    private const string noStone = "Нет обломков!";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().IsPickUpLittleAsteroid())
            {
                other.GetComponent<PlayerController>().ThrowObject(false, this.gameObject.transform);
            }
            else
            {
                GameController.GetInstance().ShowInfoOnScreen(noStone);
            }
        }
    }
}
