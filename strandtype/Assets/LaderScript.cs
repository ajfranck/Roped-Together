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
    public void Stack(Vector3 OldSpawn, GameObject LadderStack)
    {
        for(x = 0; x < laddermax; x++)
        {
            Vector3 StackPosition = SpawnPosition;
            //Vector3 LadderDirection = Ladder.transform.forward;
            StackPosition.y = StackPosition.y + 3;
            Instantiate(LadderSegment, StackPosition, Ladder.transform.rotation);
        }
    }



    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
        for(i = 0; i < 5; i++)
        {
            if(i == 0){
            Vector3 SpawnPosition = PositionClass();
            GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, this.transform.rotation);
            }
            else
            {
                Stack(SpawnPosition, Ladder);
            }
        }
            
        }




    }


  
  


}
