using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Range(0f, 10f)]
    public float movingSpeed = 5f;

    public int lifePoints;

    public Projectile projectile;

    public float fireTime;

    private new Rigidbody rigidbody;

    private GameObject muzzle;
    private Transform projectileParent;

    private bool isShooting = false;

    private Vector3 movement = Vector3.zero;

    private Vector3 rotation = Vector3.zero;

    private Vector3 startingPosition = Vector3.zero;

    private ExplosibleDeath death;

    //We need this variable to restore the base projectile after the bonus
    [HideInInspector]
    public Projectile baseProjectile;

    //We need this variable to restore the base speed after the bonus
    [HideInInspector]
    public float baseSpeed;


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
        //Movement
        rigidbody.MovePosition(rigidbody.position + movement * movingSpeed * Time.fixedDeltaTime);

        //Rotation
        if (rotation != Vector3.zero)
        {
            rigidbody.rotation = Quaternion.LookRotation(rotation * Time.fixedDeltaTime);
        }

        //Reset Position
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

    private void LifeChecker()
    {
        lifePoints--;
        if (lifePoints <= 0)
        {
            death.Explosion();
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
