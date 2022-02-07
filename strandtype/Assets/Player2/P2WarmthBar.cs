using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2WarmthBar : MonoBehaviour
{
    [SerializeField]
    Light P2light;

    public float P2MaxWarmth = 100;
    public float P2currentWarmth;

    public P2HealthBar P2HealthBar;

    void Start()
    {
        P2light.intensity = 4;
        P2currentWarmth = P2MaxWarmth;
        P2HealthBar.P2SetWarmth(P2MaxWarmth);
    }

    // Update is called once per frame
    void Update()
    {   
   
        if(P2currentWarmth > 0)
        {
            P2loseWarmth(0.005f);
        }    
        if(P2currentWarmth <= 0)
        {
            Debug.Log("dead from cold");
        }
        //if not in trigger, -1 health per second
        //add trigger collider here
    }

    void P2loseWarmth(float P2warmthLoss)
    {
        P2currentWarmth -= P2warmthLoss;
        P2HealthBar.P2SetWarmth(P2currentWarmth);
    }



    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Fire") && P2currentWarmth<=P2MaxWarmth)
        {
            if(P2currentWarmth < 100)
            {
                P2currentWarmth += .75f;
            }
            P2HealthBar.P2SetWarmth(P2currentWarmth);
        }




    }


}
