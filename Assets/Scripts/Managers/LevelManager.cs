using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    private static LevelManager m_intance = null;

    public static LevelManager Instance
    {
        get
        {
            if (m_intance == null)
            {
                m_intance = new LevelManager();
                
            }
            return m_intance;
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
