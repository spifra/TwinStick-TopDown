using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawerActivator : MonoBehaviour
{
    public List<GameObject> spawners = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject go in spawners)
            {
                go.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
