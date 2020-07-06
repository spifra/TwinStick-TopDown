using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force;
    public float lifetime;
    public float damage;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Lifetime());
    }

    private void Start()
    {
        rigidbody.AddForce(rigidbody.transform.forward * force);
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("projectile particles");
    }
}
