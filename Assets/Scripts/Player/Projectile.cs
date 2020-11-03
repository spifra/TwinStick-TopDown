using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float force;
    [SerializeField]
    private float lifetime;
    [SerializeField]
    private float damage;

    [HideInInspector]
    public Player myPlayer;

    private new Rigidbody rigidbody;

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

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.LifeChecker(damage, myPlayer);
            Destroy(gameObject);
        }
    }
}
