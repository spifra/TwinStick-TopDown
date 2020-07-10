using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float secondToSpawn;

    protected List<Transform> spawners = new List<Transform>();

    public virtual IEnumerator Spawn()
    {
        yield return null;
    }
}
