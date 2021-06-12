using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial
{
    public int Max_steps { get; private set; }
    private int current_step = -1;
    
    private TextConfiguration textConfiguration;

    public Tutorial(TextConfiguration textConfiguration)
    {
        this.textConfiguration = textConfiguration;
        Max_steps = textConfiguration.tutorial_step.Length;
    }


    public string GetNextStep()
    {
        if (current_step > Max_steps) return "";
        current_step++;
        return textConfiguration.tutorial_step[current_step];
    }

}
