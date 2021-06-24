using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    private static LevelManager _intance = null;

    public static LevelManager Instance
    {
        get
        {
            if (_intance == null)
            {
                _intance = new LevelManager();
                
            }
            return _intance;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("Game");       
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
