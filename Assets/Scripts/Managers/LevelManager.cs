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

    [Space]
    [Header("Level Rules")]
    [Tooltip("When the player died, the level will be loaded after this time")]
    public float timeRestartLevel;

    [Space]
    [Header("DEBUG")]
    public Levels level;

    [HideInInspector]
    public bool isLevelStarted = false;

    [HideInInspector]
    public bool isLevelEnded = false;

    private List<GameObject> levels = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        levels = Resources.LoadAll<GameObject>("Levels").ToList();
        StartCoroutine(InitLevel());

        LevelBuilder();
    }

    void LevelBuilder()
    {   
        SoundManager.Instance.PlaySound(level.ToString());
        GameObject currentLevel = levels.Where(x => x.name == level.ToString()).FirstOrDefault();
        Instantiate(currentLevel);
    }

    private IEnumerator InitLevel()
    {
        SoundManager.Instance.PlaySound("LevelIntro");

        yield return new WaitForSeconds(3.5f);

        isLevelStarted = true;
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        isLevelEnded = true;
        yield return new WaitForSeconds(timeRestartLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
