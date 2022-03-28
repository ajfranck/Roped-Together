using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaderScript : MonoBehaviour
{
    public GameObject LadderSegment;
    public GameObject Player1;
    void Start()
    {
    }

    public Vector3 PositionClass()
    {
        Vector3 SpawnPosition = this.transform.position;
        double Cosdegrees = Math.Cos(this.transform.rotation.y);
        double Sindegrees = Math.Sin(this.transform.rotation.y);
        SpawnPosition.y = SpawnPosition.y + 3;
        SpawnPosition.x = Player1.transform.position.x + ((1/2)*(float)Sindegrees/3);
        SpawnPosition.z = Player1.transform.position.y + ((1/2)*(float)Cosdegrees/3);
        
        

        
        return SpawnPosition;

    }



    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Vector3 SpawnPosition = PositionClass();
            GameObject Ladder = Instantiate(LadderSegment, SpawnPosition, this.transform.rotation);
            //Ladder.transform.rotation = Quaternion.Euler(Vector3.forward + degrees);
            
        }




    }


  
  


}
