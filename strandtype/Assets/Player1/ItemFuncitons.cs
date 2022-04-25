using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFuncitons : MonoBehaviour
{
    public bool usingPickaxe = false;

    public void TestItemFunction()
    {
        Debug.Log("Runs the test item function");
    }


    public void CubeItemFunction()
    {
        Debug.Log("Cube has been used.");
    }


    public void RopeItemFunction()
    {
        
    }


    ////////////////////////////////////////////////////////////
    bool promptDisplayed = false;
    public bool isMining = false;
    bool hasMined = false;
    public bool miningPossible = false;
    public Vector3 sizeChange = new Vector3(100f, 100f, 100f);

    public GameObject p1Mine;
    public GameObject p1Mining;

    public void PickaxeItemFunction(Animator animator, Collider other)
    {
        StartCoroutine(POGMINE(other.gameObject, animator));
    }

    IEnumerator POGMINE(GameObject other, Animator animator)
    {
        HidePrompt();
        MiningPrompt();
        isMining = true;
        animator.SetTrigger("Mining");
        //play particle, reduce size of rock
        yield return new WaitForSeconds(1.5f);
        other.transform.localScale = other.gameObject.transform.localScale - sizeChange;

        HideMiningPrompt();
        isMining = false;
        hasMined = true;
    }
    private void DisplayPrompt()
    {
        p1Mine.SetActive(true);
        promptDisplayed = true;
    }
    private void HidePrompt()
    {
        p1Mine.SetActive(false);
        promptDisplayed = false;
    }
    private void MiningPrompt()
    {
        p1Mining.SetActive(true);
    }
    private void HideMiningPrompt()
    {
        p1Mining.SetActive(false);
    }
    /////////////////////////////////////////////////////////////////
}
