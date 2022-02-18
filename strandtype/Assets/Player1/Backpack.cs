using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{


       

//Items
    //public Item TestItem;
    //public Item CubeItem;

//Variables
    public int BackpackPosition = 0;


    void Start()
    {
        ClearInventory();
    }



    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Fire1"))
        {
            if(Input.GetKeyDown("d") && BackpackPosition<11){
                BackpackPosition++;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if(Input.GetKeyDown("a") && BackpackPosition>0){
                BackpackPosition--;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if(Input.GetKeyDown("s")){
                BackpackPosition+=3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if(Input.GetKeyDown("w") && BackpackPosition > 2 ){
                BackpackPosition-=3;
                Debug.Log("Backpack Position is: " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if(Input.GetKeyDown("f") && HotBars.HotBarListP1[HotBars.HotBarPositionP1] != null){
                StaticBackPack.BackpackList[BackpackPosition] = HotBars.HotBarListP1[HotBars.HotBarPositionP1];
                HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;

                Debug.Log("Backpack at position " + BackpackPosition + "is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if(Input.GetKeyDown("g") && StaticBackPack.BackpackList[BackpackPosition] != null){
                HotBars.HotBarListP1[HotBars.HotBarPositionP1] = StaticBackPack.BackpackList[BackpackPosition];
                //StaticBackPack.BackpackList[BackpackPosition] = null;
                Debug.Log("HotBar is:" + HotBars.HotBarListP1[HotBars.HotBarPositionP1]);
            }
        }



    }


    private void ClearInventory()
    {
        for (int i = 0; i < 11; i++)
        {
            StaticBackPack.BackpackList.Add(null);
        }
    }



}
