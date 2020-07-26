using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lifePoints;

    public float movingSpeed;

    private new Rigidbody rigidbody;

    private GameObject target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        FollowTarget();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile currentProjectile = collision.gameObject.GetComponent<Projectile>();

            Destroy(currentProjectile.gameObject);

            LifeChecker(currentProjectile.damage, currentProjectile.myPlayer);
        }
    }

    private void LifeChecker(float damage, PlayerMovement player)
    {
        lifePoints -= damage;
        if (lifePoints <= 0)
        {
            player.enemiesCounter++;
            Destroy(gameObject);
        }
    }

    private void FollowTarget()
    {
        transform.LookAt(target.transform);

        rigidbody.AddForce(transform.forward * movingSpeed, ForceMode.Force);

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Enemy Killed!");
    }
}
