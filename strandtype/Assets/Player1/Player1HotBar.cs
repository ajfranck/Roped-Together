using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1HotBar : MonoBehaviour
{

    public Item TestItem;
    public Item CubeItem;

    public List<Item> HotBar = new List<Item>();


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (HotBar[0] == null)
            {
                Debug.Log("null");
            }

            else
            {
                HotBar[0].CallFunction();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fire") && Input.GetKeyDown("p"))
        {
            HotBar.Add(TestItem);
        }


        if (other.gameObject.CompareTag("CubeItem") && Input.GetKeyDown("p"))
        {
            HotBar.Add(CubeItem);
        }


    }
}
