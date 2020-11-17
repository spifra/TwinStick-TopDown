using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
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
    [Header("Game Rules")]
    [Tooltip("When the player died the level will be loaded after this time")]
    [SerializeField]
    private float timeRestartLevel = 0.0f;

    [Space]
    [Header("DEBUG")]
    [SerializeField]
    private Levels level;

    private List<GameObject> levels = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        levels = Resources.LoadAll<GameObject>("Levels").ToList();
        DontDestroyOnLoad(this.gameObject);
    }

    // Set enemies to kill in the level and return the level prefab to load
    public GameObject GetCurrentLevel()
    {
        LevelManager.Instance.enemiesToKill = (int)Random.Range(5f, 10f);
        return levels.Where(x => x.name == level.ToString()).FirstOrDefault();
    }

    // To go to the next level we add to the level and we load gameplay scene again. LevelManager will call its awake again and it will load the new level
    public void NextLevel()
    {
        level = level + 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }
    private IEnumerator LoadLevel(string sceneName)
    {
        LevelManager.Instance.isLevelEnded = true;
        yield return new WaitForSeconds(timeRestartLevel);
        SceneManager.LoadScene(sceneName);
    }
}
