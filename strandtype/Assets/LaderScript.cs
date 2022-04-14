using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaderScript : MonoBehaviour
{
//GameObjects
    public GameObject LadderSegment;
    public GameObject LadderParent;
//NumericalValues
    float SpawnDistance = 2;
    int laddermax = 5;
    int x = 0;
    int i = 0;
    int count = 0;
//Vector3
    public Vector3 SpawnPosition;
    Vector3 NewPosition;
    void Start()
    {
        LadderParent = GameObject.FindGameObjectWithTag("ParentLadder");
    }

    public Vector3 PositionClass()
    {
        Vector3 playerDirection = this.transform.forward;
        Vector3 SpawnPosition = this.transform.position + playerDirection * SpawnDistance;
        SpawnPosition.y = SpawnPosition.y + 3;
        return SpawnPosition;
    }
    public Vector3 StackFunction(Vector3 Spawn, int count)
    {
        Vector3 NewPosition = Spawn;
        NewPosition.y = NewPosition.y + count;
        return NewPosition;
    }
    void Update()
    {
        if(i < 5)
        {
            if(Input.GetKeyDown("space"))
            {
                    if(i == 0)
                    {
                        SpawnPosition = PositionClass();
                        LadderParent.transform.position = this.transform.position + this.transform.forward * SpawnDistance;
                        GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, this.transform.rotation, LadderParent.transform);
                        i++;
                    }
                    else
                    {
                        NewPosition = StackFunction(SpawnPosition, i);
                        Instantiate(LadderSegment, NewPosition, this.transform.rotation, LadderParent.transform);
                        i++;
                    }    
            }
        }
        else
        {

        }
    }
}
