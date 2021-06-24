using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Image _foreground;
    [SerializeField]
    float _fakeTime = 5f;
    float _fakeCurrentTime;

    void Awake()
    {
        _foreground.fillAmount = 0.0f;
        _fakeCurrentTime = _fakeTime;
    }

    void Update()
    {
        _fakeCurrentTime -= Time.deltaTime;

        _foreground.fillAmount = 1 - _fakeCurrentTime / _fakeTime;
        if (_fakeCurrentTime < 0)
        {
            LevelManager.Instance.LoadMenu();
        }
    }
}
