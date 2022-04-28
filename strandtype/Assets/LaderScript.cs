using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaderScript : MonoBehaviour
{
//GameObjects
    public GameObject LadderSegment;
    public GameObject LadderParent;
    Rigidbody LadderBody;
//NumericalValues
    float SpawnDistance = 2;
    int laddermax = 5;
    int x = 0;
    int i = 0;
    int count = 0;
    int MaxLength = 8;
//Bool
    bool FPressed = false;
    bool isWaiting = false;
//Vector3
    public Vector3 SpawnPosition;
    Vector3 NewPosition;

    void Start()
    {
        LadderParent = GameObject.FindGameObjectWithTag("ParentLadder");
        LadderBody = LadderParent.GetComponent<Rigidbody>();
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
        if(i < MaxLength)
        {
            if(Input.GetKey("space") && isWaiting == false)
            {   
                    isWaiting = true;
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
                    StartCoroutine(Waiter());
            }
        }
        else
        {
            if(Input.GetKeyDown("f") && x == 0 && FPressed == false)
            {
                LadderBody.useGravity = true;
                LadderBody.isKinematic = false;
                FPressed = true;
                LadderBody.AddTorque(this.transform.right * (MaxLength * 20));
                x++;
            }
            if(x==1 && Input.GetKeyDown("f") && FPressed == false)
            {
                LadderBody.isKinematic = true;
            }
            else
            {
                FPressed = false;
            }
        }

        //if(LadderBody.useGravity) //StartCoroutine(BeginDestruction());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.2f);
        isWaiting = false;
    }
}
