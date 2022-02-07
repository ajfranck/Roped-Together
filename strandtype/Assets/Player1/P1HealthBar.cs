using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1HealthBar : MonoBehaviour
{

    public Slider slider;

    public void P1MaxWarmth(int p1warmth)
    {
        slider.maxValue = p1warmth;
        slider.minValue = p1warmth;
    }

    public void P1SetWarmth(int p1warmth)
    {
        slider.value = p1warmth;
    }

}

