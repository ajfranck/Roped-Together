using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2HealthBar : MonoBehaviour
{

    public Slider slider;

    public void P2MaxWarmth(float p2warmth)
    {
        slider.maxValue = p2warmth;
        slider.minValue = p2warmth;
    }

    public void P2SetWarmth(float p2warmth)
    {
        slider.value = p2warmth;
    }

}

