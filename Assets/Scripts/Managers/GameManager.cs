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

    [HideInInspector]
    public GameObject pauseMenu;

    private bool isPaused = false;

    [Space]
    [Header("DEBUG")]
    [SerializeField]
    private Levels level;

    private List<GameObject> levels = new List<GameObject>();


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        levels = Resources.LoadAll<GameObject>("Levels").ToList();
    }

    private void GameManagerInit()
    {
        level = Levels.Level1;
        isPaused = false;
        Time.timeScale = 1;
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
        if (level == Levels.Level4)
        {
            StartCoroutine(EndGame());
        }
        else
        {

            level = level + 1;

            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
        }
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

    private IEnumerator EndGame()
    {
        SceneManager.LoadScene(2);

        yield return new WaitForSeconds(5);

        OnMainMenu();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
        GameManagerInit();
    }

    public void OnPause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        else
        {
            OnResume();
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

}
