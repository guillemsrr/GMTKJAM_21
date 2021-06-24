using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject _menuPanel;
    [SerializeField]
    GameObject _creditsPanel;

    [SerializeField]
    GameObject _soundTrack;

    private bool _creditsIsOpen = false;

    private void Awake()
    {
        _creditsPanel.SetActive(false);
    }

    private void Update()
    {
        if(_creditsIsOpen && Input.anyKeyDown)
        {
            _soundTrack.SetActive(true);
            _menuPanel.SetActive(true);
            _creditsPanel.SetActive(false);
            _creditsIsOpen = false;
        }
    }
    public void OnStartClick()
    {
        LevelManager.Instance.LoadGame();
    }

    public void OnExitClick()
    {
        CoreManager.Quit();
    }

    public void OnClickScores()
    {

    }

    public void OnClickCredits()
    {
        _soundTrack.SetActive(false);
        _menuPanel.SetActive(false);
        _creditsPanel.SetActive(true);
        _creditsIsOpen = true;
    }
}
