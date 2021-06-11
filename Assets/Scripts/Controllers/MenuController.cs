using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{


    public void OnStartClick()
    {
        LevelManager.Instance.LoadGame();
    }

    public void OnExitClick()
    {

    }
}
