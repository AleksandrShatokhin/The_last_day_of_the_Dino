using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LizardController : MonoBehaviour
{
    private NavMeshAgent agent_Lizard;

    private SpawnLizard creatorRandomPoint;
    [SerializeField] AudioClip audioEatLizard;


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

    void EatLizard(PlayerController player)
    {
        agent_Lizard.isStopped = true;

        player.PickUpLizard(false, this.gameObject.transform);
        player.StartRecoverMove();

        GameController.GetInstance().AddFeelingFull();

        GameController.GetInstance().AudioGame(audioEatLizard);
        Destroy(this.gameObject, 1.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EatLizard(other.GetComponent<PlayerController>());
        }
    }
}
