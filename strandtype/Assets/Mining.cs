using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{

    bool promptDisplayed = false;
    public bool isMining = false;
    bool hasMined = false;
    public bool miningPossible = false;
    public Vector3 sizeChange = new Vector3(100f, 100f, 100f);

    public GameObject p1Mine;
    public GameObject p1Mining;

    public WallBar wallbar;
    public ItemFuncitons itemFuncs;

    public Animator animator;

    void Update()
    {
        if (isMining) wallbar.loseStamina(0.3f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Rock"))
        {
            //display prompt to mine
            Debug.Log("Mine rock?");
            miningPossible = true;
            if (!promptDisplayed && !isMining)
            {
                DisplayPrompt();
            }
            if (Input.GetKey("e") && !isMining)
            {
                itemFuncs.PickaxeItemFunction(animator, other);
                //reference item funcs, pass thru other and animator
               
                //StartCoroutine(POGMINE(other.gameObject));
            }
        }
    }

    IEnumerator POGMINE(GameObject other)
    {
        HidePrompt();
        MiningPrompt();
        isMining = true;
        animator.SetTrigger("Mining");
        //play particle, reduce size of rock
        yield return new WaitForSeconds(1.5f);
        other.transform.localScale -= sizeChange;

        HideMiningPrompt();
        isMining = false;
        hasMined = true;
        itemFuncs.usingPickaxe = false;
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
}
