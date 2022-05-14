using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public HotBar hotbar;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    string sceneName;
    Scene currentScene;
    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Resume();
    }
    public void RestartLevel()
    {
        //hotbar.ClearInventory();
        
        for (int i = 0; i < 2; i++)
        {
            HotBars.HotBarListP1[i] = null;
            HotBars.HotBarListP2[i] = null;
        }
        
        if(sceneName == "Tutorial") SceneManager.LoadScene("Tutorial");
        if(sceneName == "Level") SceneManager.LoadScene("Level");
        Resume();
    }
}
