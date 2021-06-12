using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour
{
    [SerializeField]
    private DialogeCanvas dialogeCanvas;
    [SerializeField]
    private TextConfiguration textConfiguration;

    private Tutorial tutorial;

    public static CoreManager Instance;
    public DialogeCanvas GetDialogeCanvas => dialogeCanvas;

    public bool IsDebug { get; private set; }

    public event EventHandler OnIsDebug;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartTutorial();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            OnIsDebugChange(EventArgs.Empty);
        }
    }

    private void OnIsDebugChange(EventArgs eventArgs)
    {
        OnIsDebug.Invoke(null, eventArgs);
    }

    private void StartTutorial()
    {
        tutorial = new Tutorial(textConfiguration);
        dialogeCanvas.enabled = true;
        for (int x = 0; x < tutorial.Max_steps; x++) {
            GetDialogeCanvas.ActivateCanvasWithTextWithDelay(tutorial.GetNextStep(), 5*x);
        }

        GetDialogeCanvas.DeactivateCanvasWithDelay(5f* tutorial.Max_steps);
    }
}
