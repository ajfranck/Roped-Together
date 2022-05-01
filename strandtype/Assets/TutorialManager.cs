using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject warmthTutorial;
    public GameObject pickupTutorial;
    public GameObject timerTutorial;

    public Mining mining;
    public HotBar hotbar;

    public Animator warmthAnim;
    public Animator itemAnim;
    public Animator miningAnim;
    public Animator consumeAnim;

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
        if (other.gameObject.tag.Contains("miningTutorial"))
        {
            miningAnim.SetTrigger("FadeIn");
        }
        if (other.gameObject.tag.Contains("consumableTutorial"))
        {
            consumeAnim.SetTrigger("FadeIn");
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
        if (other.gameObject.tag.Contains("miningTutorial"))
        {
            miningAnim.SetTrigger("FadeOut");
        }
        
    }

}
