using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MovementController : MonoBehaviour
{



    // declare reference variables 

    PlayerInput playerInput;
    public CharacterController characterController;
    Animator animator;


    // variables to store player input 
    Vector2 currentMovementInput;

    public Vector3 currentMovement;

    public GameObject climbPrompt;

    Vector3 ropeMovement;

    bool movementPressed;

    float rotationFactor = 10f;

    float groundedGravity = -.05f;
    public float gravity = -9.8f;

    public bool AtFire = false;

    public WarmthBar warmthbar;
    public WallBar wallbar;
    public HotBar hotbar;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ladder"))
        {
            characterController.slopeLimit = 90f;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ladder"))
        {
            characterController.slopeLimit = 45f;
        }
        climbPrompt.SetActive(false);
    }



    void Awake()
    {

        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterController.Move.started += onMovementInput;
        playerInput.CharacterController.Move.canceled += onMovementInput;
        playerInput.CharacterController.Move.performed += onMovementInput;
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;



    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("current gravity is " );
        if (wallbar.ClimbRope) 
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }    
        else
        {
            if (!wallbar.isFalling)
            {
                Gravity(-9.8f);
                characterController.Move(currentMovement * Time.deltaTime * 10f);
                Rotation();
            }
        }
        if (warmthbar.isInteracting || hotbar.isGrabbing)

        {
            OnDisable();
        }
        else
        {
            OnEnable();
        }
    }

    void FixedUpdate()
    {           

        handleAnimation();     
    }

    void onMovementInput(InputAction.CallbackContext Context)
    {
        if (wallbar.ClimbRope)
        {
            RopeMove(Context);
        }
        else 
        {
            DefaultMove(Context);
        }
    }

    void DefaultMove(InputAction.CallbackContext context)

    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y; 
        movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;

    }

    void RopeMove(InputAction.CallbackContext context)

    {
        Debug.Log("RopeMove runs");

        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = currentMovementInput.y;
        movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;

        if(Input.GetKey("s") && characterController.isGrounded)
        {
            wallbar.ClimbRope = false;
        }
    }

    void Gravity(float gravity)
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
        }
        else
        {

            currentMovement.y += gravity;

        }
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");

        if (movementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!movementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }


    void Rotation()
    {
        Vector3 positionToLook;

        //telling it to look at the direction we are moving
        positionToLook.x = currentMovement.x;

        positionToLook.y = currentMovement.y = 0f;
        positionToLook.z = currentMovement.z;


        Quaternion currentRotation = transform.rotation; // character's current rotation
        if (movementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLook);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);

        }
    }

    void OnEnable()
    {
        playerInput.CharacterController.Enable();
    }


    void OnDisable()
    {
        playerInput.CharacterController.Disable();
    }

    void RopeRotation()
    {

        Vector3 positionToLook;

        //telling it to look at the direction we are moving
        positionToLook.x = currentMovement.x;

        // positionToLook.y = currentMovement.y = 0f;



        positionToLook.y = currentMovement.y;

        if(positionToLook.y == -1)

        {

            positionToLook.y = 1;

        }

        positionToLook.z = currentMovement.z;
        Debug.Log("PositionLook" + positionToLook.y);

        Quaternion currentRotation = transform.rotation; // character's current rotation
        if (movementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLook);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);
        }

    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("wallLeader"))// && Input.GetKey("v"))
        {
            climbPrompt.SetActive(true);
            if(Input.GetKey("e") && !wallbar.ClimbRope)
            {
                if (wallbar.isFalling) 
                {
                    wallbar.fallCoil = true;
                }               
                wallbar.ClimbRope = true;
                climbPrompt.SetActive(false);
                currentMovement = new Vector3(0f, 0f, 0f);
                wallbar.isFalling = false;
            }
            if(wallbar.ClimbRope) climbPrompt.SetActive(false);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("wallFollower"))
        {
            climbPrompt.SetActive(true);
            if (Input.GetKey("e") && !wallbar.ClimbRope)
            {
                wallbar.FollowRope = true;
            }
        }
        if (wallbar.ClimbRope) climbPrompt.SetActive(false);
    }
}
