using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Backpack : MonoBehaviour
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

        if (other.gameObject.CompareTag("Fire"))
        {
            if (Input.GetKeyDown("right") && BackpackPosition < 24)
            {
                BackpackPosition++;
                Debug.Log("P2 Backpack Position is:  " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition] );
            }
            if (Input.GetKeyDown("left") && BackpackPosition > 0)
            {
                BackpackPosition--;
                Debug.Log("P2 Backpack Position is:  " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if (Input.GetKeyDown("down"))
            {
                BackpackPosition += 5;
                Debug.Log("P2 Backpack Position is:  " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if (Input.GetKeyDown("up") && BackpackPosition > 4)
            {
                BackpackPosition -= 5;
                Debug.Log("P2 Backpack Position is:  " + BackpackPosition + "item is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
            if (Input.GetKeyDown("l"))
            {
                StaticBackPack.BackpackList[BackpackPosition] = HotBars.HotBarListP1[HotBars.HotBarPositionP1];
                HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;

                Debug.Log("P2 Backpack at position " + BackpackPosition + "is: " + StaticBackPack.BackpackList[BackpackPosition]);
            }
        }



    }


    private void ClearInventory()
    {
        for (int i = 0; i < 24; i++)
        {
            StaticBackPack.BackpackList.Add(null);
        }
    }



}
