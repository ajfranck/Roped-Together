using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2OldInput : MonoBehaviour
{
    public Rigidbody Player;
    public Vector3 playerVelocity = new Vector3(0f, 0f, 0f);
    public Vector3 Friction = new Vector3(0f, 0f, 0f);
    public float FaceDirectionX = 0f;
    public float FaceDirectionZ = 0f;
    public bool grounded = true;
    void Update()
    {

       // Move();
        Rotate();



        playerVelocity = Player.velocity; //velocity variable is set to the rigidbody's velocity.
        Player.AddForce(CalculateFriction(playerVelocity)); // friction is calculated and added by feeding the velocity vector into CalculateFriciton.
        Player.AddForce(0f, -10f, 0f);
        
            
    }


    Vector3 CalculateFriction(Vector3 playerVelocity)
    {

        Vector3 friction = new Vector3(0f, 0f, 0f);
        friction.x = playerVelocity.x * -1f;
        friction.z = playerVelocity.z * -1f;
        return friction;

    }


    void Rotate()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(FaceDirectionX, 0f, 0f));
        Vector3 direction = new Vector3(0f, 0f, 0f);
        if (Input.GetKey("w"))
        {
            FaceDirectionX = 0f;
            Move();

        }

        if (Input.GetKey("s"))
        {
            FaceDirectionX = 180f;
            Move();
        }

        if (Input.GetKey("a"))
        {
            FaceDirectionX = 90f;
            Move();
        }

        if (Input.GetKey("d"))
        {
            FaceDirectionX = 180f;
            Move();
        }

        if (grounded && Input.GetKeyDown("space"))
        {

        }
    }




    void Move()
    {

        Player.AddForce(transform.forward * 10f);

    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            grounded = false;
            Debug.Log("grounded false");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (this.gameObject.CompareTag("ground"))
        {
            Debug.Log("we are grounded");
        }
    }






}
