using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBar : MonoBehaviour
{

    public bool ClimbRope;
    // Start is called before the first frame update

    public P1HealthBar P1HealthBar;

    public float P1currentStamina;
    public float P1MaxStamina;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ClimbRope)
        {
            loseStamina(0.005f);
        }
    }




    void loseStamina(float staminaLoss)
    {
        P1currentStamina -= staminaLoss;
        P1StaminaBar.P1SetStamina(P1currentStamina);
        

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            ClimbRope = true;
        }
    }

}
