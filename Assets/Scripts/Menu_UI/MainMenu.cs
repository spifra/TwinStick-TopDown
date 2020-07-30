﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnStartGame()
    {
        SceneManager.LoadScene("Level1");
    }  
    
    public void OnExit()
    {
        Application.Quit();
    }
}
