using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton instance
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


    public int level;

    private void Awake()
    {
        
    }
}
