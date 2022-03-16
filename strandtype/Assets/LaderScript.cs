using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaderScript : MonoBehaviour
{
    public GameObject LadderSegment;
    public GameObject Player1;
    public int degrees;
    void Start()
    {
    }

    public Vector3 PositionClass()
    {
        Vector3 SpawnPosition = this.transform.position;
        SpawnPosition.y = SpawnPosition.y + 3;
        if(this.transform.rotation.y > 0)
        {
        SpawnPosition.x = SpawnPosition.x + 2;
        }
        if(this.transform.rotation.y < 0)
        {
            SpawnPosition.x = SpawnPosition.x - 2;
        }
        
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
