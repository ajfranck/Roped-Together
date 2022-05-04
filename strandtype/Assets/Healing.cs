using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    public Mining mining;
    public HotBar hotbar;
    public P2HotBar p2hotbar;
    public WarmthBar warmthbar;
    public TutorialManager tutorial;

    public Animator animator;

    float warmthStart;

    public bool player1;

    void Update()
    {
        if (player1 && (Input.GetKey("e") || Input.GetKey("[4]")) && HotBars.HotBarListP1[HotBars.HotBarPositionP1] != null && HotBars.HotBarListP1[HotBars.HotBarPositionP1].name == "BowlItem" && !mining.isMining && !warmthbar.isInteracting)
        {
            StartCoroutine(UseIt(player1));
        }
        else if(!player1 && (Input.GetKey("e") || Input.GetKey("[4]")) && HotBars.HotBarListP2[HotBars.HotBarPositionP2] != null && HotBars.HotBarListP2[HotBars.HotBarPositionP2].name == "BowlItem" && !mining.isMining && !warmthbar.isInteracting)
        {
            StartCoroutine(UseIt(player1));
        }
    }


    IEnumerator UseIt(bool player1)
    {
        tutorial.consumeAnim.SetTrigger("FadeOut");
        hotbar.isGrabbing = true;
        animator.SetTrigger("Grab");
        yield return new WaitForSeconds(1.5f);
        
        if(player1)
        {
            HotBars.HotBarListP1[HotBars.HotBarPositionP1] = null;
            hotbar.HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = hotbar.BackgroundImage.GetComponent<Image>().sprite;
        }
        else
        {
            HotBars.HotBarListP2[HotBars.HotBarPositionP2] = null;
            p2hotbar.HotBarSpritesP1[HotBars.HotBarPositionP1].GetComponent<Image>().sprite = hotbar.BackgroundImage.GetComponent<Image>().sprite;
        }
        
        
        warmthStart = warmthbar.P1currentWarmth;
        if(warmthStart + 50 > warmthbar.P1MaxWarmth) warmthStart = warmthbar.P1MaxWarmth - 1;
        else warmthStart += 50;
        while(warmthbar.P1currentWarmth < warmthStart)
        {
            warmthbar.gainWarmth(0.5f);
        }
        hotbar.isGrabbing = false;
    }


}