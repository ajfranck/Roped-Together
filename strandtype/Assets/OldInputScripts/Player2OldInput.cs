using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2OldInput : MonoBehaviour
{
    public Rigidbody Player;
    public Vector3 playerVelocity = new Vector3(0f, 0f, 0f);
    public Vector3 Friction = new Vector3(0f, 0f, 0f);


    public bool grounded = true;
    void Update()
    {

        Move();
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
        this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }




    void Move()
    {
        Vector3 direction = new Vector3(0f, 0f, 0f);
        if (Input.GetKey("w"))
        {
            Debug.Log(this.transform.forward);
            Player.AddForce(this.transform.forward * 10f);
        }

        if (Input.GetKey("s"))
        {

            direction = this.transform.forward.normalized;
            Player.AddForce(direction * -10f);
        }

        if (Input.GetKey("a"))
        {
            direction = new Vector3(-1f, 0f, 0f);
            direction = direction.normalized;
            Player.AddForce(direction*10f);
        }

        if (Input.GetKey("d"))
        {
            Player.AddForce(10f, 0f, 0f);
        }

        if (grounded && Input.GetKeyDown("space"))
        {
            Player.AddForce(0f, 2000f, 0f);
        }
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
