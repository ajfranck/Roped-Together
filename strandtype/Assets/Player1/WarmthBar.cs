using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WarmthBar : MonoBehaviour
{

    public GameObject p1Interact;
    public GameObject p1Inventory;

    public float P1MaxWarmth = 100f;
    public float P1currentWarmth;
    public TimeLeft timeleft;

    public bool isInteracting = false;
    public bool p2isInteracting = false;
    public bool contactingFire = false;
    public bool player1;
    bool veryCold;
    
    public P1HealthBar P1HealthBar;

    public MovementController movementController;


    //all fires:
    public bool fire1 = false;
    public bool fire2 = false;
    public bool fire3 = false;
    public bool fire4 = false;
    public bool fire5 = false;
    public bool fire6 = false;

    public int lastFire;

    string sceneName;
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        //LoadPlayer();
        P1currentWarmth = P1MaxWarmth;
        P1HealthBar.P1SetWarmth(P1MaxWarmth);
    }

    // Update is called once per frame
    void Update()
    {       
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if(P1currentWarmth > 0)
        {
            veryCold = false;
            loseWarmth(0.01f);
        }

        if(veryCold)
        {
            timeleft.loseWarmth(0.03f);
        }

        if(P1currentWarmth <= 0)
        {
            veryCold = true;
            P1currentWarmth = 0f;
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
                lastFire = 2;
            }
            else if(other.gameObject.CompareTag("Fire2"))
            {
                fire2 = true;
                lastFire = 3;
            }
            else if(other.gameObject.CompareTag("Fire3"))
            {
                fire3 = true;
                lastFire = 4;
            }
            else if(other.gameObject.CompareTag("Fire4"))
            {
                fire4 = true;
                lastFire = 5;
            }
            else if(other.gameObject.CompareTag("Fire5"))
            {
                fire5 = true;
                lastFire = 6;
            }
            if(!other.gameObject.CompareTag("TutorialFire"))
            {
                if(!isInteracting && player1)
                {
                    p1Interact.SetActive(true);
                }
                if(!p2isInteracting && !player1)
                {
                    p1Interact.SetActive(true);
                }
                if(Input.GetKey("space") && player1)
                {
                    isInteracting = true;
                    p1Interact.SetActive(false);
                }
                if(Input.GetKey("[0]") && !player1)
                {
                    p2isInteracting = true;
                    p1Interact.SetActive(false);
                }
                if(Input.GetKey("space") && player1)
                {
                    isInteracting = false;
                }
                if (Input.GetKey("[0]") && !player1)
                {
                    p2isInteracting = false;
                }
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
        fire4 = false;
        fire5 = false;
        fire6 = false;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        lastFire = data.lastFire;

        if(sceneName == "Tutorial")
        {
            if(lastFire == 1)
            {
                if(movementController.isPlayer1) transform.position = new Vector3(-37, 4, -17); //-15, 5, -13
                else transform.position = new Vector3(45, -2, -17);
                Debug.Log("fire one");  
            }
            else if (lastFire == 2)
            {
                Debug.Log("fire two");
                if(movementController.isPlayer1) transform.position = new Vector3(18.81f, 72f, 98.5f); //-15, 5, -13
                else transform.position = new Vector3(17.5f, 72f, 98.5f);
            }
            else
            {
                if(movementController.isPlayer1) transform.position = new Vector3(-37, 4, -17);
                else transform.position = new Vector3(45, -2, -17);
            }
        }
        else if(sceneName == "Level")
        {
            if(lastFire == 4)
            {
                ///bottom right fire
                if(movementController.isPlayer1) transform.position = new Vector3(96.18f, 2.52f, 14.71f); //-15, 5, -13
                else transform.position = new Vector3(97.2f, 2.52f, 14.71f);
                Debug.Log("fire three");  
            }
            else if(lastFire == 5)
            {
                //fire at bottom right

            }
            else if(lastFire == 6)
            {

            }
            else
            {//level start
                if(movementController.isPlayer1) transform.position = new Vector3(9.61f, 2.04f, -9.92f); //-15, 5, -13
                else transform.position = new Vector3(8f, 2.04f, -9.92f);
                Debug.Log("level start");
            }
        }
        
    }

}
