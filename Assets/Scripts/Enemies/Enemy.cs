using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lifePoints;

    public float movingSpeed;

    //explosion
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    private Vector3 cubesPivot;
    private Material myMaterial;

    //end explosion

    private new Rigidbody rigidbody;

    private GameObject target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<MeshRenderer>().material;

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
            Explosion();
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

    private void Explosion()
    {
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("CubeExplosion"))
            {
                //get rigidbody from collider object
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //add explosion force to this body with given parameters
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                }
                Destroy(hit.gameObject, 2f);
            }
        }
    }

    void createPiece(int x, int y, int z)
    {
        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
     
        piece.tag = "CubeExplosion";

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        piece.GetComponent<MeshRenderer>().material = myMaterial;

    }

    private void OnDestroy()
    {
        Debug.Log("Enemy Killed!");
    }

}



