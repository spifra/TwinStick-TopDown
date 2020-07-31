using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    #region singleton
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
    [Header("Sounds")]
    public GameObject introMusic;
    public GameObject levelMusic;

    private Transform sfxParent;

    [HideInInspector]
    public bool isLevelStarted = false;

    [HideInInspector]
    public bool isLevelEnded = false;

    private void Awake()
    {
        instance = this;
        sfxParent = GameObject.Find("SFX").transform;
        StartCoroutine(InitLevel());
        Instantiate(levelMusic, sfxParent);
    }

    /// <summary>
    /// Instatiate the intro music(countdown) and after 3.5 seconds it will be destroy and isLevelStarted will be set to true
    /// </summary>
    private IEnumerator InitLevel()
    {
        GameObject intro = Instantiate(introMusic, sfxParent);
        yield return new WaitForSeconds(3.5f);
        Destroy(intro);

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
