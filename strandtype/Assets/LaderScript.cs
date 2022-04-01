using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaderScript : MonoBehaviour
{
    public GameObject LadderSegment;
    public GameObject Player1;
    float SpawnDistance = 2;
    int laddermax = 5;
    int x = 0;
    int i = 0;
    public Vector3 SpawnPosition;
    Vector3 NewPosition;
    public Quaternion OriginalRotation;
    void Start()
    {
    }

    public Vector3 PositionClass()
    {
        Vector3 playerDirection = this.transform.forward;
        Vector3 SpawnPosition = this.transform.position + playerDirection * SpawnDistance;
        SpawnPosition.y = SpawnPosition.y + 3;
        return SpawnPosition;
    }
    public Vector3 StackFunction(Vector3 Spawn)
    {
        //Spawn = SpawnPosition;
        Vector3 NewPosition = Spawn;
        NewPosition.y = NewPosition.y + 1;
        return NewPosition;
    }
    //public 



    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
                if(i == 0)
                {
                    SpawnPosition = PositionClass();
                    GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, this.transform.rotation);
                    i++;
                }
                else
                {
                    NewPosition = StackFunction(SpawnPosition);
                    Instantiate(LadderSegment, NewPosition, this.transform.rotation);
                }    
        }




    }


  
  


}
