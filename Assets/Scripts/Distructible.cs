using UnityEngine;

public class Distructible : MonoBehaviour
{
    public float lifePoints = 2f;

    private Material myMaterial;
    private ExplosibleDeath death;

    void Start()
    {
        myMaterial = GetComponentInChildren<MeshRenderer>().material;
        death = GetComponent<ExplosibleDeath>();
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

    private void LifeChecker(float damage, Player player)
    {
        lifePoints -= damage;
        if (lifePoints <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                player.enemiesCounter++;
            }

            death.Explosion();
            Destroy(gameObject);
        }
    }
}
