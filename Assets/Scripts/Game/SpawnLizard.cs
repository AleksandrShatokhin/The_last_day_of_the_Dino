using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLizard : MonoBehaviour
{
    [SerializeField] MeshCollider stoperUp, stoperBottom;

    [SerializeField] GameObject lizard;

    private float posX, posZ;
    private Vector3 pointPos;

    void Start()
    {
        StartCoroutine(SpawnNewLizard());
    }

    void Update()
    {
        
    }

    // генерируем случайную точку в верхнем стопере
    public Vector3 PointInStoperUp()
    {
        posX = Random.Range(stoperUp.transform.position.x - Random.Range(0, stoperUp.bounds.extents.x), stoperUp.transform.position.x + Random.Range(0, stoperUp.bounds.extents.x));
        posZ = Random.Range(stoperUp.transform.position.z - Random.Range(0, stoperUp.bounds.extents.z), stoperUp.transform.position.z + Random.Range(0, stoperUp.bounds.extents.z));

        pointPos = new Vector3(posX, 1, posZ);

        return pointPos;
    }

    // генерируем случайную точку в нижнем стопере
    public Vector3 PointInStoperBottom()
    {
        posX = Random.Range(stoperBottom.transform.position.x - Random.Range(0, stoperBottom.bounds.extents.x), stoperBottom.transform.position.x + Random.Range(0, stoperBottom.bounds.extents.x));
        posZ = Random.Range(stoperBottom.transform.position.z - Random.Range(0, stoperBottom.bounds.extents.z), stoperBottom.transform.position.z + Random.Range(0, stoperBottom.bounds.extents.z));

        pointPos = new Vector3(posX, 1, posZ);

        return pointPos;
    }

    IEnumerator SpawnNewLizard()
    {
        int count;

        while (true)
        {
            count = Random.Range(0, 10);

            if (count < 5)
            {
                Instantiate(lizard, PointInStoperUp(), Quaternion.identity);
            }
            else
            {
                Instantiate(lizard, PointInStoperBottom(), Quaternion.identity);
            }

            yield return new WaitForSeconds(7.0f);
        }
    }
}
