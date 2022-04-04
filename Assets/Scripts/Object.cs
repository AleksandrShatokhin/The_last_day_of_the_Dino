using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private GameObject ps_ContactWithSand;
    [SerializeField] private ParticleSystem ps_Tail;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Ground)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);

            ps_Tail.Stop();
            Instantiate(ps_ContactWithSand, spawnPos, ps_ContactWithSand.transform.rotation);
        }
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
