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
    [Tooltip("When the player died, the level will be loaded after this time")]
    public float timeRestartLevel;

    [Space]
    [Header("DEBUG")]
    public Levels level;

    private List<GameObject> levels = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        levels = Resources.LoadAll<GameObject>("Levels").ToList();
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject GetCurrentLevel()
    {
        LevelManager.Instance.enemiesToKill = (int)Random.Range(5f, 10f);
        return levels.Where(x => x.name == level.ToString()).FirstOrDefault();
    }

    public void NextLevel()
    {
        level = level + 1;
        Debug.Log(level);
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
