using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    [Space]
    [Header("Movement")]
    [Range(0f, 10f)]
    public float movingSpeed = 5f;

    [Space]
    [Header("Stats")]
    public int lifePoints;

    [Space]
    [Header("Fire")]
    public Projectile projectile;
    public float fireTime;

    /*Fire*/
    private GameObject muzzle;
    private Transform projectileParent;
    private bool isShooting = false;

    /*Movement*/
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 startingPosition = Vector3.zero;
    private new Rigidbody rigidbody;

    private ExplosibleDeath death;

    /*Stats*/
    //Variable to restore the base projectile after the bonus
    [HideInInspector]
    public Projectile baseProjectile;
    //Variable to restore the base speed after the bonus
    [HideInInspector]
    public float baseSpeed;

    /*HUD*/
    [HideInInspector]
    public int enemiesCounter = 0;
    [HideInInspector]
    public string powerUp = "";

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        death = GetComponent<ExplosibleDeath>();

        muzzle = transform.GetChild(1).gameObject;
        projectileParent = GameObject.Find("Projectiles").transform;
        baseProjectile = projectile;
        baseSpeed = movingSpeed;
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Move();

        CheckForResetPosition();
    }

    /// <summary>
    /// If the level is started move and rotate the player.
    /// MovePosition: Multiply the rigidbody position by the Vector3 Input
    /// </summary>
    private void Move()
    {
        if (LevelManager.Instance.isLevelStarted)
        {
            rigidbody.MovePosition(rigidbody.position + movement * movingSpeed * Time.fixedDeltaTime);

            if (rotation != Vector3.zero)
            {
                rigidbody.rotation = Quaternion.LookRotation(rotation * Time.fixedDeltaTime);
            }
        }
    }

    //If the player fall from the ground reset the position 
    private void CheckForResetPosition()
    {
        if (transform.position.y < -5)
        {
            transform.position = startingPosition;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LifeChecker();
        }
    }

    /// <summary>
    /// When the player collide with an enemy decrement his lifepoints. 
    /// if the player doesn't have any lifepoints call the explosion function in the death component and after restart the level.
    /// </summary>
    private void LifeChecker()
    {
        lifePoints--;
        if (lifePoints <= 0)
        {
            death.Explosion();
            GameManager.Instance.RestartLevel();
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
            StopAllCoroutines();
        }
    }

    #endregion

    private IEnumerator InstatiateProjectile()
    {
        while (isShooting)
        {
            Projectile currentProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation, projectileParent.transform);

            currentProjectile.myPlayer = this;
            yield return new WaitForSeconds(fireTime);
        }
    }
}
