using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Bomb : PowerUp
{
    public Projectile bombProjectile;

    protected override IEnumerator Effect()
    {
        player.projectile = bombProjectile;
        yield return new WaitForSeconds(lifeTime);
        player.projectile = player.baseProjectile;
        Destroy(gameObject);
    }
}
