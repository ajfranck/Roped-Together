using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToLamp : MonoBehaviour
{

    private bool waiting = false;
    public Rigidbody theRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            StartCoroutine("Swing");
        }

        HandleSwing();

    }


    void HandleSwing()
    {

    
    }

    IEnumerator Swing()
    {
        waiting = true;
        theRB.AddForce(1f, 0f, 0f);
        Debug.Log("adds force");
        yield return new WaitForSeconds(.01f);
        waiting = false;

    }

}
