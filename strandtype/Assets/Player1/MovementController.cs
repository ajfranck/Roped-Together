using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MovementController : MonoBehaviour
{
    // declare reference variables 

    PlayerInput playerInput;
    Player2Input player2Input;

    public CharacterController characterController;
    Animator animator;


    // variables to store player input 
    Vector2 currentMovementInput;

    public Vector3 currentMovement;

    public Vector3 zero = new Vector3(0f, 0f, 0f);

    public GameObject climbPrompt;
    public GameObject endClimbPrompt;


    Vector3 ropeMovement;

    bool movementPressed;

    public GameObject thePlayer;

    float rotationFactor = 10f;

    float groundedGravity = -.05f;
    public float gravity = -9.8f;

    public bool AtFire = false;

    public WarmthBar warmthbar;
    public WallBar wallbar;
    public HotBar hotbar;
    public P2HotBar p2hotbar;

    public bool isPlayer1;
    public bool isEndingClimb = false;




    void Awake()
    {

        if(isPlayer1) playerInput = new PlayerInput();
        else player2Input = new Player2Input();
        
        //characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if(isPlayer1)
        {
            playerInput.CharacterController.Move.started += onMovementInput;
            playerInput.CharacterController.Move.canceled += onMovementInput;
            playerInput.CharacterController.Move.performed += onMovementInput;
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
        }
        else
        {
            player2Input.CharacterController.Move.started += onMovementInput;
            player2Input.CharacterController.Move.canceled += onMovementInput;
            player2Input.CharacterController.Move.performed += onMovementInput;
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
        }
        



    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("current gravity is " );
        Debug.Log("put at " + this.transform.position);
        if (wallbar.FollowRope)
        {
            Debug.Log("Update followrope " + wallbar.FollowRope);
            characterController.Move(currentMovement * Time.deltaTime * 2f);
        }

        else if (wallbar.ClimbRope) 
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }    
        else
        {
            if (!wallbar.isFalling)
            {
                Debug.Log("Not falling should have gravity");
                Gravity(-3f);
                characterController.Move(currentMovement * Time.deltaTime * 10f);
                Rotation();
            }
        }
        if (warmthbar.isInteracting == true || warmthbar.p2isInteracting)// || hotbar.isGrabbing == true || p2hotbar.isGrabbing == true)// || mining.isMining)
        {
            //Debug.Log("disabling the movement " );
            //OnDisable();
            if(isPlayer1) playerInput.CharacterController.Disable();
            else player2Input.CharacterController.Disable();
        }
        else if(!isEndingClimb)
        {
            OnEnable();
        }

        if(hotbar.isGrabbing)
        {
            playerInput.CharacterController.Disable();
        }
        if(p2hotbar.isGrabbing)
        {
            player2Input.CharacterController.Disable();
        }
        if (characterController.isGrounded)
        {
            wallbar.fallIfNoAnchors = true;
        }
    }

    void FixedUpdate()
    {

        handleAnimation();
        if (isPlayer1)
        {
            Debug.Log("player one wallbar climb is " + wallbar.FollowRope);
        }

        else
        {
            Debug.Log("player two wallbar climb is " + wallbar.FollowRope);
        }
    }

    void onMovementInput(InputAction.CallbackContext Context)
    {
        Debug.Log("Movement thinks " + wallbar.FollowRope);
        if (wallbar.FollowRope)
        {
            FollowRopeMove(Context);
        }

        else if (wallbar.ClimbRope)
        {
            RopeMove(Context);
        }
        else if(!isEndingClimb)
        {
            DefaultMove(Context);
        }
    }

    void DefaultMove(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * 0.7f;
        currentMovement.z = currentMovementInput.y * 0.7f; 
        movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;
    }

    void RopeMove(InputAction.CallbackContext context)
    {
        Debug.Log("RopeMove runs");

        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = currentMovementInput.y;
        currentMovement.z = 0f;
        movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;

        if((Input.GetKey("e") || Input.GetKey("[4]")) && characterController.isGrounded)
        {
            wallbar.ClimbRope = false;
        }
    }

    void FollowRopeMove(InputAction.CallbackContext context)
    {
        Debug.Log("FollowRope runs");
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = currentMovementInput.y;
        currentMovement.z = 0f;
        movementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;

        if ((Input.GetKey("e") || Input.GetKey("[4]")) && characterController.isGrounded)
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

    public void OnEnable()
    {
        if(isPlayer1) playerInput.CharacterController.Enable();
        else player2Input.CharacterController.Enable();
        Debug.Log("ONENABLE");
    }


    public void OnDisable()
    {
        if(isPlayer1) playerInput.CharacterController.Disable();
        else player2Input.CharacterController.Disable();
        Debug.Log("ONDISABLE");
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
        if (other.gameObject.CompareTag("wallLeader") && wallbar.isLeader)// && Input.GetKey("v"))
        {
            climbPrompt.SetActive(true);
            if (isPlayer1)
            {
                if (Input.GetKey("e") && !wallbar.ClimbRope && wallbar.P1currentStamina>0f)
                {

                    Debug.Log("starts leading");
                    Debug.Log("wallLeader");
                    if (wallbar.isFalling)
                    {
                        wallbar.fallCoil = true;
                    }
                    wallbar.ClimbRope = true;
                    climbPrompt.SetActive(false);
                    currentMovement = new Vector3(0f, 0f, 0f);
                    wallbar.isFalling = false;
                    wallbar.onLedge = false;
                    // other.gameObject.SetActive(false);
                }
                if (wallbar.ClimbRope) climbPrompt.SetActive(false);
            }

            else
            {
                if (Input.GetKey("4") && !wallbar.ClimbRope && wallbar.P1currentStamina > 0f)
                {

                    Debug.Log("starts leading");
                    Debug.Log("wallLeader");
                    if (wallbar.isFalling)
                    {
                        wallbar.fallCoil = true;
                    }
                    wallbar.ClimbRope = true;
                    climbPrompt.SetActive(false);
                    currentMovement = new Vector3(0f, 0f, 0f);
                    wallbar.isFalling = false;
                    wallbar.onLedge = false;
                    // other.gameObject.SetActive(false);
                }
                if (wallbar.ClimbRope) climbPrompt.SetActive(false);
            }


        }

        if (other.gameObject.CompareTag("wallFollower") && wallbar.isFollower)
        {
            Debug.Log("Followerd" );
            if (isPlayer1)
            {
                climbPrompt.SetActive(true);
                if ((Input.GetKey("e")) && !wallbar.FollowRope)
                {
                    Debug.Log("Starts following");
                    wallbar.FollowRope = true;
                    climbPrompt.SetActive(false);
                    currentMovement = new Vector3(0f, 0f, 0f);
                    wallbar.isFalling = false;
                    wallbar.onLedge = false;

                }
                if (wallbar.FollowRope) climbPrompt.SetActive(false);
            }

            else
            {
                climbPrompt.SetActive(true);
                if ((Input.GetKey("4")) && !wallbar.FollowRope)
                {
                    Debug.Log("Starts following");
                    wallbar.FollowRope = true;
                    climbPrompt.SetActive(false);
                    currentMovement = new Vector3(0f, 0f, 0f);
                    wallbar.isFalling = false;
                    wallbar.onLedge = false;

                }
                if (wallbar.FollowRope) climbPrompt.SetActive(false);
            }


        }

        if (other.gameObject.CompareTag("climbEnd"))
        {
            if (isPlayer1)
            {
                endClimbPrompt.SetActive(true);
                if ((Input.GetKey("f")) && (wallbar.ClimbRope || wallbar.FollowRope))
                {
                    characterController.enabled = false;
                    wallbar.ClimbRope = false;
                    wallbar.isFalling = false;
                    wallbar.FollowRope = false;
                    wallbar.onLedge = true;
                    GameObject endClimber = other.gameObject;
                    this.transform.position = endClimber.transform.position;
                    Debug.Log("Should be transported to " + endClimber.transform.position);
                    endClimbPrompt.SetActive(false);
                    characterController.enabled = true;
                }
                if (wallbar.onLedge)
                {
                    endClimbPrompt.SetActive(false);
                }
            }
            else
            {
                endClimbPrompt.SetActive(true);
                if ((Input.GetKey("5")) && (wallbar.ClimbRope || wallbar.FollowRope))
                {
                    characterController.enabled = false;
                    wallbar.ClimbRope = false;
                    wallbar.isFalling = false;
                    wallbar.FollowRope = false;
                    wallbar.onLedge = true;
                    GameObject endClimber = other.gameObject;
                    this.transform.position = endClimber.transform.position;
                    Debug.Log("Should be transported to " + endClimber.transform.position);
                    endClimbPrompt.SetActive(false);
                    characterController.enabled = true;
                }
                if (wallbar.onLedge)
                {
                    endClimbPrompt.SetActive(false);
                }
            }
            

        }

        



    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            characterController.slopeLimit = 45f;
        }
        climbPrompt.SetActive(false);

        if (other.gameObject.CompareTag("ledge"))
        {
            wallbar.onLedge = false;
        }

        if (other.gameObject.tag.Contains("ParentLadder"))
        {
            characterController.slopeLimit = 45f;
        }

        if (other.gameObject.CompareTag("climbEnd"))
        {
            endClimbPrompt.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("ParentLadder"))
        {
            climbPrompt.SetActive(false);
            characterController.slopeLimit = 90f;
        }


        if (other.gameObject.CompareTag("ledge"))
        {
            if (wallbar.ClimbRope || wallbar.FollowRope)
            {
                wallbar.onLedge = true;
            }
        }

    }
}
