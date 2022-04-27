using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour
{
    public GameObject FireworksAll;


    void OnCollisionEnter(Collision coll) 
    {
        if (coll.collider.CompareTag ("CubeItem")) 
        {
           Explode();
           StartCoroutine(waiter());
           FireworksAll.SetActive(false);
        }
    }
    void Explode () 
    {
        Instantiate(FireworksAll);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
    }

}
