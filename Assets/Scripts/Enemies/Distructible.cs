using UnityEngine;

public class Distructible : MonoBehaviour, IDamageable
{
    public float lifePoints = 2f;

    private Material myMaterial;
    private ExplosibleDeath death;

    void Start()
    {
        myMaterial = GetComponentInChildren<MeshRenderer>().material;
        death = GetComponent<ExplosibleDeath>();
    }

    public void LifeChecker(float damage, Player player)
    {
        
        lifePoints -= damage;
        if (lifePoints <= 0)
        {
            death.Explosion();
            Destroy(gameObject);
        }
    }
}
