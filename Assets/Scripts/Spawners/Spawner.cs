using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Seconds between spawned entities")]
    public float spawnTimer;

    [Tooltip("Total Number of entities to spawn in this spawner")]
    public float entitiesToSpawn;

    protected List<Transform> spawners = new List<Transform>();

    protected virtual IEnumerator Spawn()
    {
        yield return null;
    }
}
