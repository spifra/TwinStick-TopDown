using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life;

    public float movingSpeed;

    private Rigidbody rigidbody;

    private GameObject target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        FollowTarget();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            LifeChecker(collision.gameObject.GetComponent<Projectile>().damage);
        }
    }

    private void LifeChecker(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FollowTarget()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        transform.LookAt(target.transform);

        rigidbody.AddRelativeForce(Vector3.forward * movingSpeed, ForceMode.Force);
    }

    private void OnDestroy()
    {
        Debug.Log("Enemy Killed!");
    }
}
