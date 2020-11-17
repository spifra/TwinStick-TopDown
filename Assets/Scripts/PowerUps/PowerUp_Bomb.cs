using System.Collections;
using UnityEngine;

public class PowerUp_Bomb : PowerUp
{
    [SerializeField]
    private Projectile bombProjectile = null;

    protected override IEnumerator Effect()
    {
        player.powerUp = "power up: bomb";
        player.projectile = bombProjectile;
        yield return new WaitForSeconds(lifeTime);
        player.projectile = player.baseProjectile;
        player.powerUp = "";
        Destroy(gameObject);
    }
}
