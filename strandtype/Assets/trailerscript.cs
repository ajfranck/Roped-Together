using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailerscript : MonoBehaviour
{
    Vector3 matric;
    public GameObject cam;
    Vector3 moveToPosition;
    Vector3 turnToPosition;
    float speed = 1.7f; 
    bool move = true;

    float degreesPerSecond = 15f;

    Quaternion startRotation;
    Quaternion endRotation;
    float rotationProgress = -1;

    public Animator fadeIn;


/*
    void Start(
        cam.transform.position =  new Vector3(29.5f, 26.2f, 65.4f);
    )
    */
    void Update(){

        /*
         if(move){
              //Assigning new position to moveTOPosition
              moveToPosition = new Vector3(-8.74f, 10.6f, -21.13f);
              cam.transform.position = 
              Vector3.SmoothDamp(cam.transform.position,
                            moveToPosition,
                            ref matric, speed);
        }
        */
        StartCoroutine(Waiter2());
        StartCoroutine(Waiter());
        
        if (rotationProgress < 1 && rotationProgress >= 0){
            rotationProgress += Time.deltaTime;

            // Here we assign the interpolated rotation to transform.rotation
            // It will range from startRotation (rotationProgress == 0) to endRotation (rotationProgress >= 1)
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }
         
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(6.5f);
        fadeIn.SetTrigger("Start");
        //transform.Rotate(new Vector3(-Time.deltaTime * degreesPerSecond, 0f, 0f), Space.World);
    }
    IEnumerator Waiter2()
    {
        yield return new WaitForSeconds(1);
        StartRotating(35);
        //transform.Rotate(new Vector3(-Time.deltaTime * degreesPerSecond, 0f, 0f), Space.World);
    }

    

    // Call this to start the rotation
    void StartRotating(float zPosition){

        // Here we cache the starting and target rotations
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(zPosition, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // This starts the rotation, but you can use a boolean flag if it's clearer for you
        rotationProgress = 0;
    }

}