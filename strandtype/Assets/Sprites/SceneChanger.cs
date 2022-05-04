using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class SceneChanger : MonoBehaviour
{

    public Animator transition;
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    void Start()
    {
        MainMenu.SetActive(true);
    }

    public void NextScene()
    {
        StartCoroutine(LoadAsynchronously());    
        //SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    IEnumerator LoadAsynchronously()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        //AsyncOperation operation = SceneManager.LoadSceneAsync("Tutorial");
        SceneManager.LoadScene("Tutorial");
        //LoadPlayer();
    }
}