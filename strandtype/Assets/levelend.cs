using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelend : MonoBehaviour
{
    public bool promptDisplay = false;
    public GameObject prompt;
    
    public Animator transition;

    void Start()
    {
        promptDisplay = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("exitscene"))
        {
            if(!promptDisplay) prompt.SetActive(true);
            if(Input.GetKey("e") || Input.GetKey("[4]"))
            {
                Debug.Log("loading next scene");
                promptDisplay = true;
                StartCoroutine(LoadAsynchronously());   
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("exitscene"))
        {
            prompt.SetActive(false);
            promptDisplay = false;
        }
    }
    


    IEnumerator LoadAsynchronously()
    {
        prompt.SetActive(false);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        //AsyncOperation operation = SceneManager.LoadSceneAsync("next level");
        SceneManager.LoadScene("Main Menu");
    }

}
