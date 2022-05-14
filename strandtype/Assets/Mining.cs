using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{

    //credit to gabriel aguilar and brackeys for the idea!

    bool promptDisplayed = false;
    public bool isMining = false;
    public bool miningPossible = false;
    public Vector3 sizeChange = new Vector3(50f, 50f, 50f);
    public Vector3 minSize = new Vector3(0f, 0f, 0f);
    bool hasMined = false;

    public GameObject p1Mine;
    public GameObject p1Mining;
    public GameObject pickaxe;

    public GameObject fracturedObject;
    public float explosionMinForce = 1;
    public float explosionMaxForce = 10;
    public float explosionForceRadius = 3;
    public float fragScaleFactor = 1;
    private GameObject fractObj;

    public WallBar wallbar;
    public HotBar Hotbar;
    public P2HotBar p2hotbar;

    public ItemFuncitons itemFuncs;

    public Animator animator;

    public bool player1;

    void Update()
    {
        if (isMining) wallbar.loseStamina(0.3f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (player1 && other.gameObject.tag.Contains("Rock") && HotBars.HotBarListP1[HotBars.HotBarPositionP1] != null && HotBars.HotBarListP1[HotBars.HotBarPositionP1].name == "PickaxeItem")
        {
            miningPossible = true;
            //display prompt to mine
            if (!promptDisplayed && !isMining && other.transform.localScale.x > minSize.x)
            {
                DisplayPrompt();
            }
            if ((Input.GetKey("f") && !isMining && other.transform.localScale.x > minSize.x))
            {   
                //reference item funcs, pass thru other and animator
                StartCoroutine(POGMINE(other.gameObject, player1));

            }
        }
        else if (!player1 && other.gameObject.tag.Contains("Rock") && HotBars.HotBarListP2[HotBars.HotBarPositionP2] != null && HotBars.HotBarListP2[HotBars.HotBarPositionP2].name == "PickaxeItem")
        {
            miningPossible = true;
            //display prompt to mine
            if (!promptDisplayed && !isMining && other.transform.localScale.x > minSize.x)
            {
                DisplayPrompt();
            }
            if ((Input.GetKey("[5]") && !isMining && other.transform.localScale.x > minSize.x))
            {   
                //reference item funcs, pass thru other and animator
                StartCoroutine(POGMINE(other.gameObject, player1));

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Contains("Rock"))
        {
            HidePrompt();
            miningPossible = false;
        }
    }
    
    IEnumerator POGMINE(GameObject other, bool player1)
    {
        if(player1) Hotbar.isGrabbing = true;
        else p2hotbar.isGrabbing = true;
        HidePrompt();
        MiningPrompt();
        isMining = true;
        pickaxe.SetActive(true);
        animator.SetTrigger("Mining");
        //play particle, reduce size of rock
        yield return new WaitForSeconds(0.9f);
        if(other.transform.localScale.x >= 300)
        {
            animator.SetTrigger("Mining");
            yield return new WaitForSeconds(0.9f);
        }

        Explode(other);

        pickaxe.SetActive(false);
        HideMiningPrompt();
        isMining = false;
        hasMined = true;
        if(player1) Hotbar.isGrabbing = false;
        else p2hotbar.isGrabbing = false;
    }

    

    void Explode(GameObject originalObject)
    {
        if(originalObject != null)
        {
            originalObject.SetActive(false);

            if(fracturedObject != null)
            {
                fractObj = Instantiate(fracturedObject, originalObject.transform.position, originalObject.transform.rotation) as GameObject;
                fractObj.transform.localScale = new Vector3(originalObject.transform.localScale.x / 100, originalObject.transform.localScale.y / 100, originalObject.transform.localScale.z / 100);

                foreach (Transform t in fractObj.transform)
                {
                    var rb = t.GetComponent<Rigidbody>();

                    if(rb != null)
                        rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), originalObject.transform.position, explosionForceRadius);

                    StartCoroutine(Shrink(t,2));
                }

                Destroy(fractObj, 8f);
            }
        }
    }

    IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 newScale = t.localScale;

        while(newScale.x >= 0)
        {
            newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);

            t.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }
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
