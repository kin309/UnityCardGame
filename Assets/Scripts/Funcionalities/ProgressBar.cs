using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetFill(int fill)
    {
        slider.value = fill;
    }

    public void SetMaxFill(int fill)
    {
        slider.maxValue = fill;
        slider.value = fill;
    }
}
