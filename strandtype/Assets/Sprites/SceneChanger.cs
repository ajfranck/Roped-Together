using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneChanger : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}