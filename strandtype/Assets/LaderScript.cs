using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LaderScript : MonoBehaviour
{
//GameObjects
    public GameObject LadderSegment;
    public GameObject LadderParent;
    GameObject LadderParent2;
    Rigidbody LadderBody2;
    public GameObject ladderClimbPrompt;
    public MovementController movementcontroller;
    public Mining mining;
    public WarmthBar warmthbar;

    public P2HotBar p2hotbar;

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
    bool duplicated = false;
    bool duplicated2 = false;
//Vector3
    private Vector3 SpawnPosition = new Vector3(35f, -2.3f, 19f);
    private Vector3 SpawnPositionBridge = new Vector3(8f, 4.7f, 35.8f);
    Vector3 NewPosition;

    bool holdingLadder = true;

    void Start()
    {
        LadderParent = GameObject.FindGameObjectWithTag("ParentLadder");
        LadderBody = LadderParent.GetComponent<Rigidbody>();
        //LadderBody2 = LadderParent.GetComponent<Rigidbody>();
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
        LadderBody2.isKinematic = true;
        i = 0;
        x = 0;
        count = 0;
        //holdingLadder = false;
        duplicated = false;
        HotBars.HotBarListP2[HotBars.HotBarPositionP2] = null;
        p2hotbar.HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = p2hotbar.BackgroundImage.GetComponent<Image>().sprite;
    }
    IEnumerator Waiter3()
    {
        yield return new WaitForSeconds(1.5f);
        LadderBody2.isKinematic = true;
        i = 0;
        x = 0;
        count = 0;
        //holdingLadder = false;
        duplicated = false;
        HotBars.HotBarListP2[HotBars.HotBarPositionP2] = null;
        p2hotbar.HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = p2hotbar.BackgroundImage.GetComponent<Image>().sprite;
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
    
    void OnTriggerStay(Collider other) //mountain and we //roped together
    {
        if(other.gameObject.tag.Contains("ladderClimbArea")) movementcontroller.characterController.slopeLimit = 90f;

        ////THESE NEED TO BE P1 IN OTHER SCRIPT
        if(HotBars.HotBarListP2[HotBars.HotBarPositionP2].name != null && HotBars.HotBarListP2[HotBars.HotBarPositionP2].name == "LadderItem" && !mining.isMining && !warmthbar.isInteracting)
        {
            if(other.gameObject.tag.Contains("ladderSpotRock"))
            {
                if(holdingLadder && !duplicated)
                {
                    LadderParent2 = Instantiate(LadderParent);
                    duplicated = true;
                    LadderBody2 = LadderParent2.GetComponent<Rigidbody>(); 
                }


                ladderClimbPrompt.SetActive(true);
                //display a prompt
                if (i < MaxLength && holdingLadder)
                {
                    if ((Input.GetKey("space") || Input.GetKey("[0]")) && isWaiting == false)
                    {
                        isWaiting = true;
                        if (i == 0)
                        {
                            SpawnPosition = SpawnPosition;//PositionClass();
                            LadderParent2.transform.position = this.transform.position + this.transform.forward * SpawnDistance;
                            GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, Quaternion.Euler(0, 0, 0), LadderParent2.transform);
                            i++;
                        }
                        else
                        {
                            NewPosition = StackFunction(SpawnPosition, i);
                            Instantiate(LadderSegment, NewPosition, Quaternion.Euler(0, 0, 0), LadderParent2.transform);
                            i++;
                        }
                        StartCoroutine(Waiter());
                    }
                }
                else
                {
                    if ((Input.GetKeyDown("f") || Input.GetKeyDown("[6]")) && x == 0 && FPressed == false)
                    {
                        LadderBody2.useGravity = true;
                        LadderBody2.isKinematic = false;
                        FPressed = true;
                        LadderBody2.AddTorque(LadderParent.transform.right * (MaxLength * 75));
                        x++;
                        StartCoroutine(Waiter3());
                    }
                    else
                    {
                        
                        //StartCoroutine(BeginDestruction());
                        FPressed = false;
                    }
                }
            }


                if(other.gameObject.tag.Contains("ladderSpotBridge"))
                {
                    if (holdingLadder && !duplicated)
                    {
                        ladderClimbPrompt.SetActive(true);
                        LadderParent2 = Instantiate(LadderParent);
                        duplicated = true;
                        LadderBody2 = LadderParent2.GetComponent<Rigidbody>();

                        
                    }

                    //display a prompt
                    if (i < MaxLength + 4 && holdingLadder)
                    {
                        if ((Input.GetKey("space") || Input.GetKey("[0]")) && isWaiting == false)
                        {
                            isWaiting = true;
                            if (i == 0)
                            {
                                SpawnPosition = SpawnPositionBridge;//PositionClass();
                                LadderParent2.transform.position = this.transform.position + this.transform.forward * SpawnDistance;
                                GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, Quaternion.Euler(0, 90, 0), LadderParent2.transform);
                                i++;
                            }
                            else
                            {
                                NewPosition = StackFunction(SpawnPosition, i);
                                Instantiate(LadderSegment, NewPosition, Quaternion.Euler(0, 90, 0), LadderParent2.transform);
                                i++;
                            }
                            StartCoroutine(Waiter());
                        }
                    }
                    else
                    {
                        if ((Input.GetKeyDown("f") || Input.GetKeyDown("[6]")) && x == 0 && FPressed == false)
                        {
                            LadderBody2.useGravity = true;
                            LadderBody2.isKinematic = false;
                            FPressed = true;
                            LadderBody2.AddTorque(this.transform.right * (MaxLength * 75));
                            x++;
                        }
                        else
                        {
                            StartCoroutine(Waiter2());
                            //StartCoroutine(BeginDestruction2());
                            FPressed = false;
                        }
                    }

            }
        }
        
       
    }
     
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Contains("ladderClimbArea"))
        {
            movementcontroller.characterController.slopeLimit = 45f;
            ladderClimbPrompt.SetActive(false);
           // duplicated = false;
        }
    }
    
}
