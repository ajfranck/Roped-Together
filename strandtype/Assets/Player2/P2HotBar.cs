using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2HotBar : MonoBehaviour
{

    public Item TestItem;
    public Item CubeItem;

    

    public bool pickedUp = false;
    public bool promptDisplayed = false;

    public string itemNear = "";

    public static int HotBarPosition = 0;



    void Start()
    {
        ClearInventory();
    }


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {

            HotBars.HotBarPositionP2 = 0;
            Debug.Log("P2 Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP2]);

        }


        if (Input.GetKeyDown("2"))
        {


            HotBars.HotBarPositionP2 = 1;
            Debug.Log("P2 Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP2[HotBars.HotBarPositionP2]);

        }


        if (Input.GetKeyDown("3"))
        {
            HotBars.HotBarPositionP2 = 2;
            Debug.Log("P2 Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP2[HotBars.HotBarPositionP2]);
        }


        if (Input.GetKeyDown("4"))
        {
            HotBars.HotBarPositionP2 = 3;
            Debug.Log("P2 Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP2[HotBars.HotBarPositionP2]);
        }



        if (Input.GetKeyDown("u"))
        {

            if (HotBars.HotBarListP2[HotBars.HotBarPositionP2] == null)
            {
                Debug.Log("P2 Nothing in this hotbar position");
            }
            else
            {
                HotBars.HotBarListP2[HotBars.HotBarPositionP2].CallFunction();
            }
        }


    }










    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Fire"))
        {

            if (!promptDisplayed)
            {
                itemNear = TestItem.name;

                Debug.Log("P2 pick up" + itemNear + "?");

                promptDisplayed = true;
            }

            if (Input.GetKey("p") && !pickedUp)
            {

                HotBars.HotBarListP2[HotBars.HotBarPositionP2] = TestItem;
                pickedUp = true;
                Debug.Log("P2 Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP2[HotBars.HotBarPositionP2]);

            }
        }



        if (other.gameObject.CompareTag("CubeItem"))
        {

            if (!promptDisplayed)
            {
                itemNear = CubeItem.name;

                Debug.Log("P2 pick up" + itemNear + "?");

                promptDisplayed = true;

            }


            if (Input.GetKey("p") && !pickedUp)
            {
                HotBars.HotBarListP2[HotBars.HotBarPositionP2] = CubeItem;
                pickedUp = true;
                Debug.Log("P2 Hotbar Item at Position " + HotBars.HotBarPositionP2 + "is: " + HotBars.HotBarListP2[HotBars.HotBarPositionP2]);
            }
        }



    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Fire"))
        {
            pickedUp = false;
            promptDisplayed = false;
        }

        if (other.gameObject.CompareTag("CubeItem"))
        {
            pickedUp = false;
            promptDisplayed = false;
        }

    }



    private void ClearInventory()
    {
        for (int i = 0; i < 4; i++)
        {
            HotBars.HotBarListP2.Add(null);
        }
    }


    private void PickUpItem()
    {

    }


}
