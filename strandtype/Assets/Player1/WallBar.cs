using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBar : MonoBehaviour
{

    public bool ClimbRope;
    // Start is called before the first frame update

    public P1HealthBar P1HealthBar;

    public float P1currentStamina = 10f;
    public float P1MaxStamina;
    public bool isFalling = false;

    void Start()
    {
        Debug.Log("Start current stamina " + P1currentStamina);
    }

    // Update is called once per frame

    void Update()
    {
        if (ClimbRope)
        {
            //loseStamina(0.1f);
            Debug.Log("hoe");
            Debug.Log("Current Stamina is " + P1currentStamina);

            if (P1currentStamina <= 0f)
            {
                isFalling = true;
            }
        }       
    }




    void loseStamina(float staminaLoss)
    {
        P1currentStamina -= staminaLoss*Time.deltaTime;
        P1HealthBar.P1SetStamina(P1currentStamina);

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            ClimbRope = true;
        }
    }

}