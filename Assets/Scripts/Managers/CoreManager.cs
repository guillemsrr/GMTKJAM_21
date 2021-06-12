using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour
{
    [SerializeField]
    private DialogeCanvas _dialogeCanvas;
    [SerializeField]
    private TextConfiguration _textConfiguration;

    private Tutorial _tutorial;

    private static CoreManager _intance = null;
    public static CoreManager Instance
    {
        get
        {
            if (_intance == null)
            {
                GameObject go = new GameObject();
                go.name = "CoreManager";
                _intance = go.AddComponent<CoreManager>();
            }
            return _intance;
        }
    }
    public DialogeCanvas GetDialogeCanvas => _dialogeCanvas;

    public bool IsDebug { get; private set; }

    public event EventHandler OnIsDebug;

    private void Awake()
    {
        _intance = this;
    }

    private void Start()
    {
        if(_dialogeCanvas)
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
        _tutorial = new Tutorial(_textConfiguration);
        _dialogeCanvas.enabled = true;
        for (int x = 0; x < _tutorial.Max_steps; x++) {
            GetDialogeCanvas.ActivateCanvasWithTextWithDelay(_tutorial.GetNextStep(), 5*x);
        }

        GetDialogeCanvas.DeactivateCanvasWithDelay(5f* _tutorial.Max_steps);
    }
}
