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
    public Mining mining;

    public float P1currentStamina = 1f;
    public float P1MaxStamina;
    public bool isFalling = false;

    public MovementController movementController;

    public GameObject Gripper;
    public Image gripBar;
    float grip, maxGrip = 100f;
    float lerpSpeed;
    public bool fallCoil = false;
    public LineRenderer ropeLines;
    public GameObject theAnchor;

    void Start()
    {
        grip = maxGrip;
        Debug.Log("Start current stamina " + P1currentStamina);
        theAnchor = this.gameObject;
    }

    // Update is called once per frame

    void Update()
    {

        Debug.Log("Grip " + grip);
        Debug.Log("Grip movement" + movementController.currentMovement.magnitude);
        Debug.Log("ClimbRope " + ClimbRope);
        if (ClimbRope)
        {
            //GRIP LOSS LEVEL
            if (movementController.currentMovement.magnitude < 1f)
            {
                Debug.Log("Grip minusing");
                grip -= 0.5f;
            }
            else
            {
                loseStamina(1f);
                grip += 0.7f;
            }

            if (P1currentStamina <= 0f || grip <= 0f)
            {
                isFalling = true;
                ClimbRope = false;
            }
        }
        else // (movementController.characterController.isGrounded)
        {
            Debug.Log("adding stamina");
            if (P1currentStamina < P1MaxStamina && movementController.characterController.isGrounded && !mining.isMining) addStamina(0.175f);
            grip = 100f;
        }

        //else grip = 100f;
        if (movementController.characterController.isGrounded) isFalling = false;

        if (grip > maxGrip) grip = maxGrip;

        if (grip == maxGrip) Gripper.SetActive(false);
        if (grip < maxGrip) Gripper.SetActive(true);

        lerpSpeed = 3f * Time.deltaTime;
        GripBarFiller();
        ColorChanger();


    }

    void GripBarFiller()
    {
        gripBar.fillAmount = Mathf.Lerp(gripBar.fillAmount, grip / maxGrip, lerpSpeed);
    }
    void ColorChanger()
    {
        Color gripColor = Color.Lerp(Color.red, Color.green, (grip / maxGrip));
        gripBar.color = gripColor;
    }


    public void loseStamina(float staminaLoss)
    {
        P1currentStamina -= staminaLoss * Time.deltaTime;
        P1HealthBar.P1SetStamina(P1currentStamina);
    }
    public void addStamina(float staminaGain)
    {
        P1currentStamina += staminaGain * Time.deltaTime;
        P1HealthBar.P1SetStamina(P1currentStamina);
    }



    void OnTriggerEnter(Collider other)
    {
        /* if (other.gameObject.CompareTag("wall"))
         {
             //display a prompt to attach to the wall
             ClimbRope = true;
         }
        */
        if (other.gameObject.CompareTag("AnchorIn"))
        {
            GameObject OldAnchor = theAnchor;
            ropeLines = theAnchor.GetComponent<LineRenderer>();
            theAnchor = other.gameObject;
            ropeLines.SetPosition(0, OldAnchor.transform.position);
            ropeLines.SetPosition(1, theAnchor.transform.position);
            Debug.Log("The anchor is: " + theAnchor);
            toAnchor = true;
        }
    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("wall") && Input.GetKey("v"))
        {
            ClimbRope = true;
        }
    }
}
