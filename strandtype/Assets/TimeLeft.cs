using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLeft : MonoBehaviour
{
    //public WarmthBar p1warmthbar;
    //public P2WarmthBar p2warmthbar;

    public Slider slider;

    public float currentTimeLeft;

    public bool startLoss = false;
    public bool level1StartLoss;
    public Animator sceneMover;
    public GameObject deathText;

    // Start is called before the first frame update
    void Start()
    {
        deathText.SetActive(false);
        currentTimeLeft = 100;
    }
    void Update()
    {
        slider.value = currentTimeLeft;
        /*
        if(p1warmthbar.P1currentWarmth <= 0)
        {
            loseWarmth(0.001f);
        }
        if(p2warmthbar.P2currentWarmth <= 0)
        {
            loseWarmth(0.001f);
        }*/
        if(startLoss) 
        {
            loseWarmth(0.003f);
        }
        else if(level1StartLoss)
        {
            loseWarmth(0.0005f);
        }
        if(currentTimeLeft <= 0f)
        {
            StartCoroutine(LoadAsynchronously());
            //loss condition
        }
    }

    public void loseWarmth(float warmthLoss)
    {
        currentTimeLeft -= warmthLoss;
        slider.value = currentTimeLeft;

    }

    IEnumerator LoadAsynchronously()
    {
        
        sceneMover.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        deathText.SetActive(true);
        yield return new WaitForSeconds(3f);
        //AsyncOperation operation = SceneManager.LoadSceneAsync("Tutorial");
        SceneManager.LoadScene("Main Menu");
        //LoadPlayer();
    }
    /*
    public void MaxTimeLeft(float timeisLeft)
    {
        slider.maxValue = timeisLeft;
        slider.minValue = timeisLeft;
    }
    public void TimeisLeft(float timeleft)
    {
        slider.value = timeleft;
    }
    */


}
