using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 10f)]
    public float movingSpeed = 5f;

    public GameObject projectile;

    public float fireTime;

    private new Rigidbody rigidbody;

    private GameObject muzzle;
    private Transform projectileParent;

    private bool isShooting = false;

    Vector3 movement = Vector3.zero;

    Vector3 rotation = Vector3.zero;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        muzzle = transform.GetChild(1).gameObject;
        projectileParent = GameObject.Find("Projectiles").transform;
    }

    private void FixedUpdate()
    {
        //Movement
        rigidbody.MovePosition(rigidbody.position + movement * movingSpeed * Time.fixedDeltaTime);

        //Rotation
        if (rotation != Vector3.zero)
        {
            rigidbody.rotation = Quaternion.LookRotation(rotation * Time.fixedDeltaTime);
        }
    }

    #region INPUT
    private void OnMove(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        movement = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private void OnRotate(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        rotation = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            isShooting = true;
            StartCoroutine(InstatiateProjectile());
        }
        else
        {
            isShooting = false;
        }
    }

    #endregion

    private IEnumerator InstatiateProjectile()
    {
        while (isShooting)
        {
            Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation, projectileParent.transform);
            yield return new WaitForSeconds(fireTime);
        }
    }
}
