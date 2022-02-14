using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{

    public Item TestItem;
    public Item CubeItem;

    public List<Item> HotBarList = new List<Item>();

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

            HotBars.HotBarPositionP1 = 0;
            
        }


        if (Input.GetKeyDown("2"))
        {


            HotBars.HotBarPositionP1 = 1;

        }


        if (Input.GetKeyDown("3"))
        {
            HotBars.HotBarPositionP1 = 2;
        }


        if (Input.GetKeyDown("4"))
        {
            HotBars.HotBarPositionP1 = 3;
        }



        if (Input.GetKeyDown("u"))
        {

            if (HotBars.HotBarListP1[HotBars.HotBarPositionP1] == null)
            {
                Debug.Log("Nothing in this hotbar position");
            }
            else
            {
                HotBars.HotBarListP1[HotBars.HotBarPositionP1].CallFunction();
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

                Debug.Log("pick up " + itemNear + "?");

                promptDisplayed = true;
            }

            if (Input.GetKey("p") && !pickedUp)
            {

                HotBarList[HotBarPosition] = TestItem;
                pickedUp = true;

            }
        }



        if (other.gameObject.CompareTag("CubeItem"))
        {

            if (!promptDisplayed)
            {
                itemNear = CubeItem.name;

                Debug.Log("pick up " + itemNear + "?");

                promptDisplayed = true;
            }


            if (Input.GetKey("p") && !pickedUp)
            {
                HotBarList[HotBarPosition] = CubeItem;
                pickedUp = true;
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
        for(int i = 0; i<4; i++)
        {
            HotBarList.Add(null);
        }
    }


    private void PickUpItem()
    {

    }


}
