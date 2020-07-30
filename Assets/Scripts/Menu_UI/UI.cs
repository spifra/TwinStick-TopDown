using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TMP_Text lifePoints;
    public TMP_Text enemiesCounter;
    public TMP_Text powerUp;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        lifePoints.text = "LifePoints: " + player.lifePoints;
        enemiesCounter.text = "Enemies Killed: " + player.enemiesCounter;
        powerUp.text = "Power Up " + player.powerUp;
    }
}
