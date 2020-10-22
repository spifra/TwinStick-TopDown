using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels { Level1, Level2, Level3, Level4 };

public class LevelManager : MonoBehaviour
{

    #region Singleton
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    [HideInInspector]
    public int enemiesToKill;

    [HideInInspector]
    public bool isLevelStarted = false;

    [HideInInspector]
    public bool isLevelEnded = false;

    private void Awake()
    {
        instance = this;

        StartCoroutine(InitLevel());

        LevelBuilder();
    }

    //Get current level and instatiate it
    void LevelBuilder()
    {
        GameObject currentLevel = GameManager.Instance.GetCurrentLevel();

        Instantiate(currentLevel);
    }

    private IEnumerator InitLevel()
    {
        SoundManager.Instance.PlaySound("LevelIntro");

        yield return new WaitForSeconds(3.5f);

        isLevelStarted = true;
    }

    //Check if the player kill all the enemies and call next level
    public void CheckForEndLevel(Player player)
    {
        if (player.enemiesCounter >= enemiesToKill)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
