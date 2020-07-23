using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_SpeedUp : PowerUp
{
    public float newSpeed;

    protected override IEnumerator Effect()
    {
        player.powerUp = "Speed Up!";
        player.movingSpeed = newSpeed;
        yield return new WaitForSeconds(lifeTime);
        player.movingSpeed = player.baseSpeed;
        player.powerUp = "";
        Destroy(gameObject);
    }
}
