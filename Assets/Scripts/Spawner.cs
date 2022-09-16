using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float Radius;

    [SerializeField]
    int MaxZombies;

    [SerializeField]
    GameObject Zombie;
    [SerializeField]
    float SpawnPeriod;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if(FindObjectsOfType<Zombie>().Length < MaxZombies)
            {
                Instantiate(Zombie, transform.position + new Vector3((Random.value * 2 - 1) * Radius, 0, (Random.value * 2 - 1) * Radius), Zombie.transform.rotation);
                yield return new WaitForSeconds(SpawnPeriod);
            }
            else
            {
                yield return new WaitForSeconds(SpawnPeriod);
            }
        }
    }
}
