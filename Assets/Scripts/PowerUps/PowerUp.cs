using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float lifeTime;

    protected PlayerMovement player;

    private bool isTriggered = false;

    protected virtual IEnumerator Effect() { yield return null; }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            player = other.gameObject.GetComponent<PlayerMovement>();
            StartCoroutine(Effect());
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
