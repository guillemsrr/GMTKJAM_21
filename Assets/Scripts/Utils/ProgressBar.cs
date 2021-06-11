using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Image foreground;
    [SerializeField]
    float fakeTime = 5f;
    float fakeCurrentTime;

    void Awake()
    {
        foreground.fillAmount = 0.0f;
        fakeCurrentTime = fakeTime;
    }

    void Update()
    {
        fakeCurrentTime -= Time.deltaTime;

        foreground.fillAmount = 1 - fakeCurrentTime / fakeTime;
        if (fakeCurrentTime < 0)
        {
            LevelManager.Instance.LoadMenu();
        }
    }
}
