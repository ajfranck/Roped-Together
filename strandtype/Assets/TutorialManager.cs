using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject warmthTutorial;
    public GameObject pickupTutorial;
    public GameObject timerTutorial;

    
    void OnTriggerEnter(collider other)
    {
        if(other.gameObject.tag.Contains("warmthTutorial"))
        {
            
        }
        else if (other.gameObject.tag.Contains("pickupTutorial"))
        {

        }
        else if (other.gameObject.tag.Contains("timerTutorial"))
        {

        }
    }

}
