using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamageable
{
    [SerializeField]
    private float lifePoints;
    [SerializeField]
    private float movingSpeed;

    private new Rigidbody rigidbody;
    private GameObject target;
    private ExplosibleDeath death;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        death = GetComponent<ExplosibleDeath>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (!LevelManager.Instance.isLevelEnded)
        {
            transform.LookAt(target.transform);

            rigidbody.AddForce(transform.forward * movingSpeed, ForceMode.Force);

            if (transform.position.y < -5)
            {
                Destroy(gameObject);
            }
        }
    }

    public void LifeChecker(float damage, Player player)
    {

        lifePoints -= damage;
        if (lifePoints <= 0)
        {
            player.enemiesCounter++;

            SoundManager.Instance.PlaySound("EnemyDeath");

            death.Explosion();

            LevelManager.Instance.CheckForEndLevel(player);

            Destroy(gameObject);
        }
    }
}



