using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class hacks : MonoBehaviour
{
    public GameObject player1, player2, controlList;
    bool controlActive = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            //load level 1
            SceneManager.LoadScene("Level");
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Tutorial");
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {//teleprot to the end of tutorial
            player1.transform.position = new Vector3(0,0,0);
            player2.transform.position = new Vector3(0,0,0);
        }
        if(Input.GetKey(""))
        {//teleport to the end of first rope section lvel 1
            player1.transform.position = new Vector3(0,0,0);
            player2.transform.position = new Vector3(0,0,0);
        }
        if(Input.GetKey(""))
        {//teleport to end of level 1
            player1.transform.position = new Vector3(0,0,0);
            player2.transform.position = new Vector3(0,0,0);
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            if(controlActive) controlList.SetActive(false);
            else controlList.SetActive(true);
        }
    }




}
