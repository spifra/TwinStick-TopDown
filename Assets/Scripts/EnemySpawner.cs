using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public float secondToSpawn;

    private List<Enemy> enemiesResources = new List<Enemy>();
    private List<Transform> enemiesSpawners = new List<Transform>();

    void Start()
    {

        //Get Enemy spawners
        enemiesSpawners = transform.GetComponentsInChildren<Transform>().ToList();
        enemiesSpawners.Remove(transform);


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

            int transformIndex = Random.Range(0, enemiesSpawners.Count);

            Instantiate(enemiesResources[resourcesIndex].gameObject, enemiesSpawners[transformIndex].transform.position, Quaternion.identity, enemiesSpawners[transformIndex]);

            yield return new WaitForSeconds(secondToSpawn);
        }
    }
}
