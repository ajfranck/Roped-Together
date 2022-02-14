using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{


    public List<Item> BackpackList = new List<Item>();    

//Items
    public Item TestItem;
    public Item CubeItem;

//Variables
    public int BackpackPosition = 0;

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown("d")){
                BackpackPosition++;
            }
            if(Input.GetKeyDown("a")){
                BackpackPosition--;
            }
            if(Input.GetKeyDown("s")){
                BackpackPosition+=5;
            }
            if(Input.GetKeyDown("w") && BackpackPosition > 4 ){
                BackpackPosition-=5;
            }
            if(Input.GetKeyDown("f")){
                BackpackList[BackpackPosition] = HotBars.HotBarListP1[HotBars.HotBarPositionP1];
                HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;
            }




        }
    }

     


}
