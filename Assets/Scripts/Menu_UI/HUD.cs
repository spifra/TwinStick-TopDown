using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TMP_Text lifePoints;
    public TMP_Text enemiesCounter;
    public TMP_Text powerUp;
    public TMP_Text readyToStart;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        powerUp.text = "";

    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        lifePoints.text = "lifePoints: " + player.lifePoints;
        enemiesCounter.text = "enemies killed: " + player.enemiesCounter;
        powerUp.text = player.powerUp;
    }

    IEnumerator Countdown()
    {

        readyToStart.text = "3";
        yield return new WaitForSeconds(1);
        readyToStart.text = "2";
        yield return new WaitForSeconds(1);
        readyToStart.text = "1";
        yield return new WaitForSeconds(1);
        readyToStart.text = "shoot up";

        yield return new WaitForSeconds(0.5f);
        readyToStart.enabled = false;
    }

}
