using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2WarmthBar : MonoBehaviour
{

    public int P2MaxWarmth = 100;
    public int P2currentWarmth;

    public P2HealthBar P2HealthBar;

    void Start()
    {
        P2currentWarmth = P2MaxWarmth;
        P2HealthBar.P2SetWarmth(P2MaxWarmth);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.N))
        {
            P2loseWarmth(10);
        }

        if(P2currentWarmth <= 0)
        {
            Debug.Log("dead from cold");
        }
        //if not in trigger, -1 health per second
        //add trigger collider here
    }

    void P2loseWarmth(int P2warmthLoss)
    {
        P2currentWarmth -= P2warmthLoss;

        P2HealthBar.P2SetWarmth(P2currentWarmth);
    }
}
