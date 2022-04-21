using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBar : MonoBehaviour
{

    public bool ClimbRope;
    public bool toAnchor;
    // Start is called before the first frame update

    public P1HealthBar P1HealthBar;

    public float P1currentStamina = 1f;
    public float P1MaxStamina;
    public bool isFalling = false;

    public GameObject theAnchor;
    public MovementController movementController;

    public GameObject Gripper;
    public Image gripBar;
    float grip, maxGrip = 100;
    float lerpSpeed;

    void Start()
    {
        grip = maxGrip;
        Debug.Log("Start current stamina " + P1currentStamina);
    }

    // Update is called once per frame

    void Update()
    {
        Debug.Log("ClimbRope " + ClimbRope);
        if (ClimbRope)
        {
            //GRIP LOSS LEVEL
            if(movementController.currentMovement.magnitude == 0)
            {
                grip -= 0.5f;
            }
            else
            {
                loseStamina(0.1f);
                grip += 0.7f;
            }
            
            Debug.Log("hoe");
            Debug.Log("Current Stamina is " + P1currentStamina);

            if (P1currentStamina <= 0f || grip <= 0f)
            {
                isFalling = true;
                ClimbRope = false;
            }
        } 
        else // (movementController.characterController.isGrounded)
        {
            Debug.Log("adding stamina");
            if (P1currentStamina < P1MaxStamina) addStamina(0.175f);
            grip = 100f;
        }
        
        //else grip = 100f;
        if(movementController.characterController.isGrounded) isFalling = false;
        
        if(grip > maxGrip) grip = maxGrip;
        
        if (grip == maxGrip) Gripper.SetActive(false);
        if (grip < maxGrip) Gripper.SetActive(true);

        lerpSpeed = 3f * Time.deltaTime;
        GripBarFiller();
        ColorChanger();

        
    }

    void GripBarFiller()
    {
        gripBar.fillAmount = Mathf.Lerp(gripBar.fillAmount, grip/maxGrip, lerpSpeed); 
    }
    void ColorChanger()
    {
        Color gripColor = Color.Lerp(Color.red, Color.green, (grip/maxGrip));
        gripBar.color = gripColor;
    }


    void loseStamina(float staminaLoss)
    {
        P1currentStamina -= staminaLoss * Time.deltaTime;
        P1HealthBar.P1SetStamina(P1currentStamina);
    }
    void addStamina(float staminaGain)
    {
        P1currentStamina += staminaGain * Time.deltaTime;
        P1HealthBar.P1SetStamina(P1currentStamina);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            //display a prompt to attach to the wall
            ClimbRope = true;
        }

        if (other.gameObject.CompareTag("AnchorIn"))
        {
            theAnchor = other.gameObject;
            Debug.Log("The anchor is: " + theAnchor);
            toAnchor = true;
        }
    }

}
