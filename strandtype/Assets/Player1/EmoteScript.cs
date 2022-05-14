using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteScript : MonoBehaviour
{
    public MovementController movementcontroller;
    public WarmthBar warmthbar;
    public WallBar wallbar;
    public HotBar hotbar;
    public P2HotBar p2hotbar;
    public Animator animator;


    void Update()
    {
        if(!wallbar.isFalling && !wallbar.ClimbRope && !warmthbar.isInteracting)
        {
            if(Input.GetKeyDown("tab") && movementcontroller.isPlayer1)
            {
                StartCoroutine(Animation());
            }
            else if(Input.GetKeyDown("[9]") && !movementcontroller.isPlayer1)
            {
                StartCoroutine(Animation());
            }
        }   
    }

    IEnumerator Animation()
    {
        if(movementcontroller.isPlayer1) hotbar.isGrabbing = true;
        else p2hotbar.isGrabbing = true;

        animator.SetBool("Play", true);
        yield return new WaitForSeconds(2.9f);
        animator.SetBool("Play", false);
        yield return new WaitForSeconds(1f);
        if(movementcontroller.isPlayer1) hotbar.isGrabbing = false;
        else p2hotbar.isGrabbing = false;
    }
}
