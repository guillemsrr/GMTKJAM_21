using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial
{
    public int Max_steps { get; private set; }
    private int _current_step = -1;
    
    private TextConfiguration _textConfiguration;

    public Tutorial(TextConfiguration textConfiguration)
    {
        this._textConfiguration = textConfiguration;
        Max_steps = textConfiguration._tutorial_step.Length;
    }


    public string GetNextStep()
    {
        if (_current_step > Max_steps) return "";
        _current_step++;
        return _textConfiguration._tutorial_step[_current_step];
    }

}
