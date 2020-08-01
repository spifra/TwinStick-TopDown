using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float lifeTime;

    protected Player player;

    private bool isTriggered = false;

    protected virtual IEnumerator Effect() { yield return null; }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound("Bonus");
            isTriggered = true;
            player = other.gameObject.GetComponent<Player>();
            StartCoroutine(Effect());
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
