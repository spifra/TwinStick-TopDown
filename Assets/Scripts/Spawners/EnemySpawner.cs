using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : Spawner
{

    private List<Enemy> enemiesResources = new List<Enemy>();

    void Start()
    {
        //Get Enemy spawners
        spawners = transform.GetComponentsInChildren<Transform>().ToList();
        spawners.Remove(transform);


        enemiesResources = Resources.LoadAll<Enemy>("Enemies").ToList();

        if (enemiesResources.Count > 0)
        {
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.LogWarning("No enemy to spawn in the list!");
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int resourcesIndex = Random.Range(0, enemiesResources.Count);

            int transformIndex = Random.Range(0, spawners.Count);

            Instantiate(enemiesResources[resourcesIndex].gameObject, spawners[transformIndex].transform.position, Quaternion.identity, spawners[transformIndex]);

            yield return new WaitForSeconds(secondToSpawn);
        }
    }
}
