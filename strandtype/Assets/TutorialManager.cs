using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject warmthTutorial;
    public GameObject pickupTutorial;
    public GameObject timebar;

    public TimeLeft timeleft;

    public Mining mining;
    public HotBar hotbar;

    public Animator warmthAnim;
    public Animator itemAnim;
    public Animator miningAnim;
    public Animator consumeAnim;
    public Animator consume2Anim;
    public Animator finalfire;
    public Animator timeBar;

    void Start()
    {
        warmthAnim.SetTrigger("FadeIn");
    }
    void Update()
    {
        if(mining.isMining)
        {
            miningAnim.SetTrigger("FadeOut");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("pickupTutorial"))
        {
            itemAnim.SetTrigger("FadeIn");
        }
        if (other.gameObject.tag.Contains("timerTutorial"))
        {

        }
        if (other.gameObject.tag.Contains("miningTutorial") || other.gameObject.tag.Contains("ladderTutorial"))
        {
            miningAnim.SetTrigger("FadeIn");
        }
        if (other.gameObject.CompareTag("consumableTutorial"))
        {
            consumeAnim.SetTrigger("FadeIn");
        }
        if (other.gameObject.CompareTag("consumableTutorial2"))
        {
            consume2Anim.SetTrigger("FadeIn2");
        }
        if (other.gameObject.CompareTag("finalfire"))
        {
            finalfire.SetTrigger("FadeIn");
        }
        if(other.gameObject.CompareTag("timeBar"))
        {
            timebar.SetActive(true);
            timeleft.startLoss = true;
            timeBar.SetTrigger("FadeIn");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Contains("warmthTutorial"))
        {
            warmthAnim.SetTrigger("FadeOut");
        }
        if (other.gameObject.tag.Contains("pickupTutorial"))
        {
            itemAnim.SetTrigger("FadeOut");
        }
        if (other.gameObject.tag.Contains("miningTutorial") || other.gameObject.tag.Contains("ladderTutorial"))
        {
            miningAnim.SetTrigger("FadeOut");
        }
        if (other.gameObject.CompareTag("consumableTutorial"))
        {
            consumeAnim.SetTrigger("FadeOut");
        }
        if (other.gameObject.CompareTag("consumableTutorial2"))
        {
            consume2Anim.SetTrigger("FadeOut2");
        }
        if (other.gameObject.CompareTag("finalfire"))
        {
            finalfire.SetTrigger("FadeOut");
        }
        if(other.gameObject.CompareTag("timeBar"))
        {
            timeBar.SetTrigger("FadeOut");
        }
    }

}
