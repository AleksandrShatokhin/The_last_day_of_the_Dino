using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLittleAsteroid : MonoBehaviour
{
    private MeshCollider spawner;

    [SerializeField] private GameObject asteroid;

    private float posX, posZ;
    private Vector3 spawnPos;

    private int delayTheFirstFall = 2;
    private int minTime = 8, maxTime = 12;

    void Start()
    {
        spawner = this.GetComponent<MeshCollider>();

        StartCoroutine(CreateFallAsteroid());
    }

    void CreateAsteroid()
    {
        posX = Random.Range(spawner.transform.position.x - Random.Range(0, spawner.bounds.extents.x), spawner.transform.position.x + Random.Range(0, spawner.bounds.extents.x));
        posZ = Random.Range(spawner.transform.position.z - Random.Range(0, spawner.bounds.extents.z), spawner.transform.position.z + Random.Range(0, spawner.bounds.extents.z));

        spawnPos = new Vector3(posX, this.gameObject.transform.position.y, posZ);

        Instantiate(asteroid, spawnPos, Quaternion.identity);
    }

    IEnumerator CreateFallAsteroid()
    {
        yield return new WaitForSeconds(delayTheFirstFall);

        while (true)
        {
            CreateAsteroid();

            yield return new WaitForSeconds(Mathf.Lerp(minTime, maxTime, Random.value));
        }
    }
}
