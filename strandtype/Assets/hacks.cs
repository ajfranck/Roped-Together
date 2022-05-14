using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class hacks : MonoBehaviour
{
    public GameObject player1, player2;
    public CharacterController characterController;
    public CharacterController p2characterController;
    public TimeLeft timeleft;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            //load level 1
            SceneManager.LoadScene("Level");
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Tutorial");
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {//teleprot to the end of tutorial
            player1.transform.position = new Vector3(30f,71f,87.16f);
            player2.transform.position = new Vector3(28f,71f,87.16f);
        }
        if(Input.GetKey(KeyCode.F4))
        {//teleport to the end of first rope section lvel 1
            characterController.enabled = false;
            p2characterController.enabled = false;
            player1.transform.position = new Vector3(-6f, 52f, 106.11f);
            player2.transform.position = new Vector3(-4f, 52f, 106.11f);
            characterController.enabled = true;
            p2characterController.enabled = true;
        }
        if(Input.GetKey(KeyCode.F5))
        {//teleport to end of level 1
            characterController.enabled = false;
            p2characterController.enabled = false;
            player1.transform.position = new Vector3(-14f,110f,240f);
            player2.transform.position = new Vector3(-12f,110f,240f);
            characterController.enabled = true;
            p2characterController.enabled = true;
        }
        if(Input.GetKey(KeyCode.F5))
        {//teleport to end of level 1
            characterController.enabled = false;
            p2characterController.enabled = false;
            player1.transform.position = new Vector3(11f,2f,-8f);
            player2.transform.position = new Vector3(9f,2f,-8f);
            characterController.enabled = true;
            p2characterController.enabled = true;
        }
        //if(Input.GetKeyDown(KeyCode.F6))
        //{
        //    if(controlActive) controlList.SetActive(false);
        //    else controlList.SetActive(true);
        //}
        if(Input.GetKeyDown(KeyCode.F7))
        {
            timeleft.currentTimeLeft = 100f;
        }
    }




}
