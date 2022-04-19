using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSetTest : MonoBehaviour
{



   

    public Material testMaterial;
    private float transparencyRate = -0.07f;
    public float transparency = 1f;
    public float minTransparency = 0.1f;
    public float maxTransparency = 1f;


    public int PlayersIn = 0; //the number of players in the building
    public bool FadeOutNow;
    public bool FadeInNow; // if the number of players in the building is zero, then this is set to true
    public bool waiting;


    //README

    /* This script, along with a fader shader material, is to be dragged on to anything that you'd like to be transparent when the player is inside it's collider. 
       It works on a per-material basis, and can only be called once per material. (i.e. if you have a roof and a wall of the 
        same material, only one can have this script and the collider associated with it.)
    */

    void Start()
    {

        testMaterial.SetFloat("Vector1_3890158690a54921b2fff7bec7240495", 1f);
    }


    void Update()
    {
       

        testMaterial.SetFloat("Vector1_3890158690a54921b2fff7bec7240495", transparency);

        if (PlayersIn>=1 && !waiting)
        {
            StartCoroutine("FadeOut");
        }

        if (transparency <= minTransparency)
        {
            transparency = minTransparency;
            FadeOutNow = false;
        }

        if (PlayersIn <= 0)
        {
            if (FadeInNow && !waiting)
            {
                StartCoroutine("FadeIn");
            }



            if (transparency >= maxTransparency)
            {
                FadeInNow = false;
                transparency = maxTransparency;
            }
        }
        
    }


    IEnumerator FadeIn()
    {
        waiting = true;
        transparency -= transparencyRate;
        yield return new WaitForSeconds(.003f);
        waiting = false;

    }

    IEnumerator FadeOut()
    {
        waiting = true;
        transparency += transparencyRate;
        yield return new WaitForSeconds(.003f);

        waiting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ClearBuildingCollider"))
        {
            PlayersIn++;
        }
        
            
    }

    

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ClearBuildingCollider"))
        {
            FadeInNow = true;
            PlayersIn -= 1;
        }
        
    }

}
