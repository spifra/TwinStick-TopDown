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

    private void FollowTarget()
    {
        transform.LookAt(target.transform);

        rigidbody.AddForce(transform.forward * movingSpeed, ForceMode.Force);

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}



