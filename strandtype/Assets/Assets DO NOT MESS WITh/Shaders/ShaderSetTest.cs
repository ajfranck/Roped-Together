using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSetTest : MonoBehaviour
{

    public Material testMaterial;
    public float transparencyRate = -0.01f;
    public float transparency = 1f;
    public bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        testMaterial.SetFloat("Vector1_1bf5ef6f67664d09acaa75b493636256", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            Debug.Log("is pressed");
            if (!waiting)
            {
                testMaterial.SetFloat("Vector1_1bf5ef6f67664d09acaa75b493636256", transparency);
                StartCoroutine("Wait");
            }


        }

        



        if (Input.GetKey("k"))
        {
            testMaterial.SetFloat("Vector1_1bf5ef6f67664d09acaa75b493636256", 1f);
        }
    }





    IEnumerator Wait()
    {
        Debug.Log("bot frozen true " + Time.time);
        waiting = true;
        transparency += transparencyRate;
        yield return new WaitForSeconds(.05f);

        waiting = false;
        //After we have waited 5 seconds print the time again.
        Debug.Log("bot frozen false  " + Time.time);
    }





}
