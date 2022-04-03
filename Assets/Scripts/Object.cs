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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (!player.GetComponent<PlayerController>().IsPickUpLittleAsteroid())
            {
                player.GetComponent<PlayerController>().PickUpObject(false, this.gameObject.transform);
                Destroy(this.gameObject, 0.8f);
            }
            else
            {
                Debug.Log("Вы не можете нести больше одного обломка");
            }
        }
    }

    private void OnDestroy()
    {
        player.GetComponent<PlayerController>().PickUpObject(true);
    }
}
