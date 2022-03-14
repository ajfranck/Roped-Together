using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaderScript : MonoBehaviour
{
    public GameObject LadderSegment;
    public GameObject Player1;
    void Start()
    {



    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            GameObject Ladder = Instantiate(LadderSegment,Player1.transform.position, Player1.transform.rotation);
            Debug.Log("Ladder rotation: " + Ladder.transform.rotation.x);
        }




    }


    void InstantateLadders()
    {

    }

  


}
