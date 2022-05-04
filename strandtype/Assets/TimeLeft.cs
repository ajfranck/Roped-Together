using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{
    //public WarmthBar p1warmthbar;
    //public P2WarmthBar p2warmthbar;

    public Slider slider;

    public float currentTimeLeft;

    public bool startLoss = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeLeft = 100;
    }
    void Update()
    {
        slider.value = currentTimeLeft;
        /*
        if(p1warmthbar.P1currentWarmth <= 0)
        {
            loseWarmth(0.001f);
        }
        if(p2warmthbar.P2currentWarmth <= 0)
        {
            loseWarmth(0.001f);
        }*/
        if(startLoss) 
        {
            loseWarmth(0.005f);
        }
    }

    void loseWarmth(float warmthLoss)
    {
        currentTimeLeft -= warmthLoss;
        slider.value = currentTimeLeft;

    }
    /*
    public void MaxTimeLeft(float timeisLeft)
    {
        slider.maxValue = timeisLeft;
        slider.minValue = timeisLeft;
    }
    public void TimeisLeft(float timeleft)
    {
        slider.value = timeleft;
    }
    */


}
