using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{

    bool promptDisplayed = false;
    public bool isMining = false;
    public bool miningPossible = false;
    public Vector3 sizeChange = new Vector3(50f, 50f, 50f);
    public Vector3 minSize = new Vector3(0f, 0f, 0f);
    bool hasMined = false;

    public GameObject p1Mine;
    public GameObject p1Mining;

    public GameObject fracturedObject;
    public float explosionMinForce = 1;
    public float explosionMaxForce = 10;
    public float explosionForceRadius = 3;
    public float fragScaleFactor = 1;
    private GameObject fractObj;

    public WallBar wallbar;
    public ItemFuncitons itemFuncs;

    public Animator animator;

    void Update()
    {
        Debug.Log("using pickaxe is " + itemFuncs.usingPickaxe);
        if (isMining) wallbar.loseStamina(0.3f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Rock"))
        {
            //display prompt to mine
            if (!promptDisplayed && !isMining && other.transform.localScale.x > minSize.x)
            {
                DisplayPrompt();
            }
            if (Input.GetKey("e") && !isMining && other.transform.localScale.x > minSize.x)
            {   
                //itemFuncs.PickaxeItemFunction(animator, other, isMining);
                //reference item funcs, pass thru other and animator
                StartCoroutine(POGMINE(other.gameObject));

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Contains("Rock"))
        {
            HidePrompt();
        }
    }
    
    IEnumerator POGMINE(GameObject other)
    {
        HidePrompt();
        MiningPrompt();
        isMining = true;
        animator.SetTrigger("Mining");
        //play particle, reduce size of rock
        yield return new WaitForSeconds(0.9f);
        //animator.SetTrigger("Mining");
        //yield return new WaitForSeconds(1.2f);

        Explode(other);
        //other.transform.localScale -= sizeChange;
        //Instantiate(destroyedVersion, other.transform.position, other.transform.rotation);
        //other.SetActive(false);

        HideMiningPrompt();
        isMining = false;
        hasMined = true;
        itemFuncs.usingPickaxe = false;
    }

    void Explode(GameObject originalObject)
    {
        if(originalObject != null)
        {
            originalObject.SetActive(false);

            if(fracturedObject != null)
            {
                fractObj = Instantiate(fracturedObject, originalObject.transform.position, originalObject.transform.rotation) as GameObject;

                foreach (Transform t in fractObj.transform)
                {
                    var rb = t.GetComponent<Rigidbody>();

                    if(rb != null)
                        rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), originalObject.transform.position, explosionForceRadius);

                    StartCoroutine(Shrink(t,2));
                }

                Destroy(fractObj, 7.3f);
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
