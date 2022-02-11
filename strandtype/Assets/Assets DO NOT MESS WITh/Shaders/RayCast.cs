
using UnityEngine;

public class RayCast : MonoBehaviour
{


    //public Camera cam;
    public Transform Player;
    float range = 100000f;
    
   
  

    // Update is called once per frame
    void Update()
    {
       //Debug.DrawRay(this.transform.position, Player.transform.position-this.transform.position, Color.red);
        
        ShootRay();
    }


    void ShootRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Player.transform.position - this.transform.position, out hit))
        {
            Debug.DrawRay(this.transform.position, Player.transform.position - this.transform.position);
            Debug.Log("Hit wit ray : " + hit.transform.name);
        }


    }
}

