using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

   
    // declare reference variables 
    PlayerInput playerInput;
    CharacterController characterController;


    // variables to store player input 
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool movementPressed;

    float rotationFactor = 10f;
    public float gravity = -9.8f;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

         playerInput.CharacterController.Move.started += onMovementInput;
         playerInput.CharacterController.Move.canceled += onMovementInput; 
         playerInput.CharacterController.Move.performed += onMovementInput;
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;

    }


    // Update is called once per frame
    void Update()
    {
        // this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        characterController.Move(currentMovement * Time.deltaTime * 10f);
        Rotation();

        Gravity();

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
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
        }

        else
        {
            currentMovement.y += gravity;
        }

    }




    void Rotation()
    {
        Vector3 positionToLook;

        //telling it to look at the direction we are moving
        positionToLook.x = currentMovement.x;
        positionToLook.y = currentMovement.y = 0f;
        positionToLook.z = currentMovement.z;

        Debug.Log("PositionTolook: " + positionToLook);

        Quaternion currentRotation = transform.rotation; // character's current rotation
        if (movementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLook);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);

        }
        Debug.Log("rotation = " + this.transform.rotation);


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
