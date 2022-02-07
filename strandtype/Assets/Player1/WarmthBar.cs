using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmthBar : MonoBehaviour
{

    [SerializeField]
    Light light;

    public float P1MaxWarmth = 100f;
    public float P1currentWarmth;
    

    public P1HealthBar P1HealthBar;

    void Start()
    {
        light.intensity = 4;
        P1currentWarmth = P1MaxWarmth;
        P1HealthBar.P1SetWarmth(P1MaxWarmth);
    }

    // Update is called once per frame
    void Update()
    {   
        if(P1currentWarmth > 0)
        {
            loseWarmth(0.005f);
        }    

        if(P1currentWarmth <= 0)
        {
            Debug.Log("dead from cold");
        }
        //if not in trigger, -1 health per second
        //add trigger collider here
    }

    void loseWarmth(float warmthLoss)
    {
        P1currentWarmth -= warmthLoss;
        P1HealthBar.P1SetWarmth(P1currentWarmth);

    }



    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Fire") && P1currentWarmth <= P1MaxWarmth) 
        {
            if(P1currentWarmth < 100)
            {
                P1currentWarmth += .75f;
            }
            P1HealthBar.P1SetWarmth(P1currentWarmth);
        }




    }

}
