using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaderScript : MonoBehaviour
{
//GameObjects
    public GameObject LadderSegment;
    public GameObject LadderParent;
    public GameObject ladderClimbPrompt;
    public MovementController movementcontroller;
    Rigidbody LadderBody;
//NumericalValues
    float SpawnDistance = 2;
    int laddermax = 5;
    int x = 0;
    int i = 0;
    int count = 0;
    int MaxLength = 9;
//Bool
    bool FPressed = false;
    bool isWaiting = false;
//Vector3
    public Vector3 SpawnPosition = new Vector3(35f, -2.5f, 19f);
    private Vector3 SpawnPositionBridge = new Vector3(8f, 4.7f, 35.8f);
    Vector3 NewPosition;

    void Start()
    {
        LadderParent = GameObject.FindGameObjectWithTag("ParentLadder");
        LadderBody = LadderParent.GetComponent<Rigidbody>();
    }
    
    /*
    public Vector3 PositionClass()
    {
        Vector3 playerDirection = this.transform.forward;
        Vector3 SpawnPosition = this.transform.position + playerDirection * SpawnDistance;
        SpawnPosition.y = SpawnPosition.y + 3;
        return SpawnPosition;
    }
    */
    
    public Vector3 StackFunction(Vector3 Spawn, int count)
    {
        Vector3 NewPosition = Spawn;
        NewPosition.y = NewPosition.y + count;
        return NewPosition;
    }
    
    /*
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
                LadderBody.AddTorque(this.transform.right * (MaxLength * 100));
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
    */
    

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.05f);
        isWaiting = false;
    }
    IEnumerator Waiter2()
    {
        yield return new WaitForSeconds(3f);
        LadderBody.isKinematic = true;
        i = 0;
        x = 0;
        count = 0;
    }
    IEnumerator BeginDestruction()
    {
        LadderSegment.SetActive(true);
        yield return new WaitForSeconds(7f);
        for (var i = LadderParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(LadderParent.transform.GetChild(i).gameObject);
        }
    }
    IEnumerator BeginDestruction2()
    {
        LadderSegment.SetActive(true);
        yield return new WaitForSeconds(20f);
        for (var i = LadderParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(LadderParent.transform.GetChild(i).gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Contains("ladderClimbArea")) movementcontroller.characterController.slopeLimit = 90f;

        if(other.gameObject.tag.Contains("ladderSpotRock"))
        {
            ladderClimbPrompt.SetActive(true);
            //display a prompt
            if(i < MaxLength)
            {
                if(Input.GetKey("space") && isWaiting == false)
                {   
                        isWaiting = true;
                        if(i == 0)
                        {
                            SpawnPosition = SpawnPosition;//PositionClass();
                            LadderParent.transform.position = this.transform.position + this.transform.forward * SpawnDistance;
                            GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, Quaternion.Euler(0,0,0), LadderParent.transform);
                            i++;
                        }
                        else
                        {
                            NewPosition = StackFunction(SpawnPosition, i);
                            Instantiate(LadderSegment, NewPosition, Quaternion.Euler(0,0,0), LadderParent.transform);
                            i++;
                        }    
                        StartCoroutine(Waiter());
                }
            }
            else
            {
                if(Input.GetKey("f") && x == 0 && FPressed == false)
                {
                    LadderBody.useGravity = true;
                    LadderBody.isKinematic = false;
                    FPressed = true;
                    LadderBody.AddTorque(this.transform.right * (MaxLength * 75));
                    x++;
                }
                else
                {
                    StartCoroutine(Waiter2());
                    StartCoroutine(BeginDestruction());
                    FPressed = false;
                }
            }
        }


            if(other.gameObject.tag.Contains("ladderSpotBridge"))
            {
                ladderClimbPrompt.SetActive(true);
                //display a prompt
                if(i < MaxLength + 4)
                {
                    if(Input.GetKey("space") && isWaiting == false)
                    {   
                            isWaiting = true;
                            if(i == 0)
                            {
                                SpawnPosition = SpawnPositionBridge;//PositionClass();
                                LadderParent.transform.position = this.transform.position + this.transform.forward * SpawnDistance;
                                GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, Quaternion.Euler(0,90,0), LadderParent.transform);
                                i++;
                            }
                            else
                            {
                                NewPosition = StackFunction(SpawnPosition, i);
                                Instantiate(LadderSegment, NewPosition, Quaternion.Euler(0,90,0), LadderParent.transform);
                                i++;
                            }    
                            StartCoroutine(Waiter());
                    }
                }
                else
                {
                    if(Input.GetKey("f") && x == 0 && FPressed == false)
                    {
                        LadderBody.useGravity = true;
                        LadderBody.isKinematic = false;
                        FPressed = true;
                        LadderBody.AddTorque(this.transform.right * (MaxLength * 75));
                        x++;
                    }
                    else
                    {
                        StartCoroutine(Waiter2());
                        StartCoroutine(BeginDestruction2());
                        FPressed = false;
                    }
                }

                
            }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Contains("ladderClimbArea"))
        {
            movementcontroller.characterController.slopeLimit = 45f;
        }
    }
    
}
