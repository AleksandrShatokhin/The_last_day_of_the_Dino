using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnDestroy()
    {
        player.GetComponent<PlayerController>().PickUpObject(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().PickUpObject(false, this.gameObject.transform);
            Destroy(this.gameObject, 0.8f);
        }
    }
}
