using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmthBar : MonoBehaviour
{

    public int P1MaxWarmth = 100;
    public int P1currentWarmth;

    public P1HealthBar P1HealthBar;

    void Start()
    {
        P1currentWarmth = P1MaxWarmth;
        P1HealthBar.P1SetWarmth(P1MaxWarmth);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loseWarmth(10);
        }

        if(P1currentWarmth <= 0)
        {
            Debug.Log("dead from cold");
        }
        //if not in trigger, -1 health per second
        //add trigger collider here
    }

    void loseWarmth(int warmthLoss)
    {
        P1currentWarmth -= warmthLoss;

        P1HealthBar.P1SetWarmth(P1currentWarmth);
    }
}
