using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelend : MonoBehaviour
{
    public bool promptDisplay = false;
    public bool promptDisplay2 = false;
    public GameObject prompt;
    public GameObject prompt2;
    public GameObject finalText;
    public GameObject HUD;

    public Animator transition;
    public Animator textfade;

    public bool isPlayer1;
    public HotBar hotbar;


    public bool startRotation = false;
    bool ending = false;
    

    void Start()
    {
        ending = false;
        startRotation = false;
        promptDisplay = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("exitscene"))
        {
            if(!promptDisplay) prompt.SetActive(true);
            if(Input.GetKeyDown("e") || Input.GetKeyDown("[4]"))
            {
                Debug.Log("loading next scene");
                promptDisplay = true;
                StartCoroutine(LoadAsynchronously());   
            }
        }
        if(other.gameObject.CompareTag("endLevel1"))
        {
            if(!promptDisplay2) prompt2.SetActive(true);
            if((Input.GetKeyDown("e") || Input.GetKeyDown("[4]")) && !ending)
            {
                Debug.Log("loading next scene");
                promptDisplay2 = true;
                StartCoroutine(EndGame());   
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
        if(other.gameObject.CompareTag("endLevel1"))
        {
            prompt2.SetActive(false);
            promptDisplay2 = false;
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

    IEnumerator EndGame()
    {
        if(isPlayer1) hotbar.isGrabbing = true;
        ending = true;
        transition.SetTrigger("Start");
        prompt2.SetActive(false);
        HUD.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        if(!isPlayer1) this.gameObject.SetActive(false);

        startRotation = true;
        yield return new WaitForSeconds(0.2f);
        transition.SetTrigger("End");
        yield return new WaitForSeconds(7f);
        //textfade.SetTrigger("Start");
        finalText.SetActive(true);
        yield return new WaitForSeconds(7f);
        textfade.SetTrigger("End");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Menu");
    }

}
