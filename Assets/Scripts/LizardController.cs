using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LizardController : MonoBehaviour
{
    private NavMeshAgent agent_Lizard;

    private SpawnLizard creatorRandomPoint;


    void Start()
    {
        agent_Lizard = GetComponent<NavMeshAgent>();
        creatorRandomPoint = GameObject.Find("Game").GetComponent<SpawnLizard>();

        if (transform.position.z > 0)
        {
            agent_Lizard.SetDestination(creatorRandomPoint.PointInStoperBottom());
        }
        else
        {
            agent_Lizard.SetDestination(creatorRandomPoint.PointInStoperUp());
        }
    }

    private void Update()
    {
        if (agent_Lizard.hasPath == false)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().EatLizard(this.gameObject.transform);
        }
    }
}
