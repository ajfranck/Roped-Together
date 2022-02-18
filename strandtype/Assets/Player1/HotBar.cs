using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HotBar : MonoBehaviour
{

    public Item TestItem;
    public Item CubeItem;

  

    public bool pickedUp = false;
    public bool promptDisplayed = false;

    public string itemNear = "";

    public static int HotBarPosition = 0;

    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;

    public  List<GameObject> HotBarSpritesP1 = new List<GameObject>();
    



    void Start()
    {
        ClearInventory();
        
    }


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {

            HotBars.HotBarPositionP1 = 0;
            Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

        }


        if (Input.GetKeyDown("2"))
        {


            HotBars.HotBarPositionP1 = 1;
            Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);

        }


        if (Input.GetKeyDown("3"))
        {
            HotBars.HotBarPositionP1 = 2;
            Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
        }


        if (Input.GetKeyDown("4"))
        {
            HotBars.HotBarPositionP1 = 3;
            Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
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

                HotBars.HotBarListP1[HotBars.HotBarPositionP1] = TestItem;
                pickedUp = true;
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
               
                HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = HotBars.HotBarListP1[HotBars.HotBarPositionP1].Artwork;


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
                HotBars.HotBarListP1[HotBars.HotBarPositionP1] = CubeItem;
                pickedUp = true;
                Debug.Log("Hotbar Item at Position " + HotBars.HotBarPositionP1 + "is: " + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
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
            HotBars.HotBarListP1.Add(null);
            
           
        }
    }


    private void PickUpItem()
    {

    }


}
