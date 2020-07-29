using System.Collections;
using UnityEngine;

public class PowerUp_Bomb : PowerUp
{
    public Projectile bombProjectile;

    protected override IEnumerator Effect()
    {
        player.powerUp = "Bomb!";
        player.projectile = bombProjectile;
        yield return new WaitForSeconds(lifeTime);
        player.projectile = player.baseProjectile;
        player.powerUp = "";
        Destroy(gameObject);
    }
}
