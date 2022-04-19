using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{


    // declare reference variables 
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;


    // variables to store player input 
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool movementPressed;
    float rotationFactor = 10f;

    public float gravity = -9.8f;
    float groundedGravity = -.05f;

    public bool AtFire = false;

    public WarmthBar warmthbar;




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
        // if(!AtFire){
        characterController.Move(currentMovement * Time.deltaTime * 7f);
        
        Rotation();

        Gravity();
        //}

        if (warmthbar.isInteracting)
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
        //if(!AtFire){        
        handleAnimation();
        //}
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;
    }


    void Gravity()
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


   
}
