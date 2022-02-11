using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSetTest : MonoBehaviour
{

    public Material testMaterial;
    public float transparencyRate = -0.01f;
    public float transparency = 1f;
    public float minTransparency = 0.1f;
    public bool waiting;




    // Start is called before the first frame update
    void Start()
    {
        testMaterial.SetFloat("Vector1_1bf5ef6f67664d09acaa75b493636256", 1f);
    }

    // Update is called once per frame
    void Update()
    {


        testMaterial.SetFloat("Vector1_1bf5ef6f67664d09acaa75b493636256", transparency);

    




        if (Input.GetKey("k"))
        {
            transparency = 1f;
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



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CameraBox"))
        {
            Debug.Log("HitBox Camera 1");

            if (transparency >= minTransparency)
            {
                StartCoroutine("Wait");
            }
            
        }

    }

}
