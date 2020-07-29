using System.Collections;
using UnityEngine;

public class PowerUp_SpeedUp : PowerUp
{
    private float newSpeed;

    private void Start()
    {
        newSpeed = Random.Range(5, 20);
    }
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
