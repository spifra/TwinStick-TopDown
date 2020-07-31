using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpawner : Spawner
{

    private List<PowerUp> powerUpResources = new List<PowerUp>();

    void Start()
    {
        //Get PowerUp spawners
        spawners = transform.GetComponentsInChildren<Transform>().ToList();
        spawners.Remove(transform);


        powerUpResources = Resources.LoadAll<PowerUp>("PowerUps").ToList();

        if (powerUpResources.Count > 0)
        {
            StartCoroutine(Spawn());
        }
        else
        {
            Debug.LogWarning("No PowerUp to spawn in the list!");
        }
    }

    protected override IEnumerator Spawn()
    {
        while (entitiesToSpawn > 0)
        {
            if (LevelManager.Instance.isLevelStarted)
            {
                int resourcesIndex = Random.Range(0, powerUpResources.Count);
                int transformIndex = Random.Range(0, spawners.Count);

                //If there isn't any other powerup we'll spawn one
                if (FindObjectOfType<PowerUp>() == null)
                    Instantiate(powerUpResources[resourcesIndex].gameObject, spawners[transformIndex].transform.position, Quaternion.identity, spawners[transformIndex]);

                entitiesToSpawn--;

                yield return new WaitForSeconds(spawnTimer);
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
