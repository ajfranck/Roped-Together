using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneChanger : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}