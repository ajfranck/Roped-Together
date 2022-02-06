using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Rigidbody PlayerRB;

    PlayerInput playerInput;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool movementPressed;


    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterController.Move.started += context => {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;


            movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;
           
        };

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerRB.AddForce(currentMovement.x, 0f, currentMovement.z);
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
