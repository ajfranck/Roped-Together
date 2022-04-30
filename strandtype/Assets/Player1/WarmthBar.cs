using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WarmthBar : MonoBehaviour
{

    [SerializeField]
    Light light;

    public GameObject p1Interact;
    public GameObject p1Inventory;

    public float P1MaxWarmth = 100f;
    public float P1currentWarmth;  

    public bool isInteracting = false;
    public bool contactingFire = false;
    
    public P1HealthBar P1HealthBar;

    public MovementController movementController;


    //all fires:
    public bool fire1 = false;
    public bool fire2 = false;
    public bool fire3 = false;

    public int lastFire;

    void Start()
    {
        LoadPlayer();
        light.intensity = 4;
        P1currentWarmth = P1MaxWarmth;
        P1HealthBar.P1SetWarmth(P1MaxWarmth);
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(P1currentWarmth > 0)
        {
            loseWarmth(0.05f);
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

    public void gainWarmth(float warmthLoss)
    {
        P1currentWarmth += warmthLoss;
        P1HealthBar.P1SetWarmth(P1currentWarmth);
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag.Contains("Fire")) 
        {
            contactingFire = true;
            if(other.gameObject.CompareTag("Fire1"))
            {
                fire1 = true;
                lastFire = 1;
            }
            else if(other.gameObject.CompareTag("Fire2"))
            {
                fire2 = true;
                lastFire = 2;
            }
            else if(other.gameObject.CompareTag("Fire3"))
            {
                fire3 = true;
                lastFire = 3;
            }
            if(!isInteracting)
            {
                p1Interact.SetActive(true);
            }
            
            if(Input.GetKey(KeyCode.E))
            {
                isInteracting = true;
                p1Interact.SetActive(false);
            }

            if (Input.GetKey("space"))
            {
                isInteracting = false;
            }

            if(P1currentWarmth <= P1MaxWarmth)
            {
                P1currentWarmth += .75f;
            }
            P1HealthBar.P1SetWarmth(P1currentWarmth);
            SavePlayer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        p1Interact.SetActive(false);
        contactingFire = false;
        isInteracting = false;
        fire1 = false;
        fire2 = false;
        fire3 = false;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        lastFire = data.lastFire;
        if(lastFire == 1)
        {
            if(movementController.isPlayer1) transform.position = new Vector3(-15, 5, -13);
            else transform.position = new Vector3(30, -1, -14);
            Debug.Log("fire one");
            
        }
        else if (lastFire == 2)
        {
            Debug.Log("fire two");
        }
    }

}
