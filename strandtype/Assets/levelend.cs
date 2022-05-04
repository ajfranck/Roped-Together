using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelend : MonoBehaviour
{
    public bool promptDisplayed = false;
    public GameObject prompt;
    
    public Animator transition;

    void Start()
    {
        promptDisplayed = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("exitscene"))
        {
            if(!promptDisplayed) prompt.SetActive(true);
            if(Input.GetKey("e") || Input.GetKey("f"))
            {
                Debug.Log("loading next scene");
                promptDisplayed = true;
                StartCoroutine(LoadAsynchronously());   
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("exitscene"))
        {
            prompt.SetActive(false);
            promptDisplayed = false;
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
