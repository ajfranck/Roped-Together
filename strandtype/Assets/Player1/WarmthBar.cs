using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WarmthBar : MonoBehaviour
{

    [SerializeField]
    Light light;

    public Camera p1camera;

    public float P1MaxWarmth = 100f;
    public float P1currentWarmth;  

    bool isInteracting = false;
    bool contactingFire = false;
    
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

        if (other.gameObject.CompareTag("Fire")) 
        {
            contactingFire = true;
            if(Input.GetKey(KeyCode.E))
            {
                isInteracting = true;
                Debug.Log("interacting");
                p1camera.transform.position = new Vector3(-20,5,-10);
                //p1camera.transform.rotation = new Vector3(23,0,0);
            }
            else
            {
                isInteracting = false;
            }
            if(P1currentWarmth <= P1MaxWarmth)
            {
                P1currentWarmth += .75f;
            }
            P1HealthBar.P1SetWarmth(P1currentWarmth);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        contactingFire = false;
    }

}

