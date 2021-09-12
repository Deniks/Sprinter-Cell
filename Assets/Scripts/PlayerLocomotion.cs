using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    public AnimatorController animatorController;
    Transform cameraObject;
    InputManager inputManager;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Falling")]
    public LayerMask groundLayer;
    public float rayCastHeightOffset = 0.2f;
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isWalking;
    public bool isJumping;
    public bool isGrounded;
    public bool canVault;
    public bool canClimb;
    public bool isParkour;

    [SerializeField]
    private float t_parkour;
    private float chosenParkourMoveTime;

    [Header("Detection")]
    public DetectObs detectVaultObject; //checks for vault object
    public DetectObs detectVaultObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to vault
    public DetectObs detectClimbObject; //checks for climb object
    public DetectObs detectClimbObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to climb


    public DetectObs DetectWallL; //detects for a wall on the left
    public DetectObs DetectWallR; //detects for a wall on the right

    public float vaultTime; //how long the vault takes
    public Transform vaultEndPoint;

    public float climbTime; //how long the vault takes
    public Transform climbEndPoint;

    [Header("Stats")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
    public float rotationSpeed = 10;

    [Header("Jumping")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        animatorController = FindObjectOfType<AnimatorController>();
        playerManager = GetComponent<PlayerManager>();
        cameraObject = Camera.main.transform;
        myTransform = transform;

    }

    public void HandleAllMovement()
    {
        HandleFalling();
        HandleRotation();
        if (playerManager.isInteracting)
        {
            return;
        }
         HandleMovement();
        
    }
    private void HandleMovement()
    {

        moveDirection = cameraObject.forward * inputManager.vertical;
        moveDirection += cameraObject.right * inputManager.horizontal;

        moveDirection.Normalize();
        moveDirection.y = 0;
        
        if (isSprinting)
        {
            moveDirection *= sprintingSpeed;
        }
        else if (isWalking)
        {
            moveDirection *= walkingSpeed;
        }
        else if (inputManager.moveAmount > 0.5f)
        {
            moveDirection *= runningSpeed;
        }

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;
    }

    #region Rotation
    Vector3 normalVector;
    #endregion

    private void HandleRotation()
    {
 

        Vector3 targertDirection = Vector3.zero;

        targertDirection = cameraObject.forward * inputManager.vertical;
        targertDirection += cameraObject.right * inputManager.horizontal;
        targertDirection.Normalize();
        targertDirection.y = 0;

        if (targertDirection == Vector3.zero)
        {
            targertDirection = myTransform.forward;
        }


        Quaternion targetRotation = Quaternion.LookRotation(targertDirection);
        Quaternion playerRotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        myTransform.rotation = playerRotation;

    }

    private void HandleFalling()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorController.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            rigidbody.AddForce(transform.forward * leapingVelocity);
            rigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorController.PlayTargetAnimation("Landing State", true);
            }
            
            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            
            inAirTimer = 0;
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
            if (transform.position.y < -5) // if player falls down into void
            {
                GameManager.isGameOver = true;
            }
            if (GameManager.respawnGranted == true) // if continuation option is chosen by player at the end of the game
            {
                myTransform.position = new Vector3(transform.position.x, 0, transform.position.z);
                GameManager.respawnGranted = false;
            }
 
        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    private Vector3 RecordedMoveToPosition; //the position of the vault end point in world space to move the player to
    private Vector3 RecordedStartPosition; // position of player right before vault

    public void HandleJumping()
    {
        if (isGrounded) {

            //HandleVaulting();
            
            if (!(canVault || canClimb))
            {

            

                animatorController.animator.SetBool("isJumping", true);

                animatorController.PlayTargetAnimation("Jump", false);
                //rigidbody.AddForce(0, 30f, 0, ForceMode.Impulse);
                rigidbody.AddForce(new Vector3(0, 100, 0), ForceMode.VelocityChange);


                float jumpingVelcity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
                Vector3 playerVelocity = moveDirection;
                playerVelocity.y = jumpingVelcity;
                rigidbody.velocity = playerVelocity;
                rigidbody.AddForce(playerVelocity, ForceMode.VelocityChange);
                Debug.Log(rigidbody.velocity);
            }


        }
    }

    private void HandleVaulting()
    {
        //vault
        if (detectVaultObject.Obstruction)
        // if detects a vault object and there is no wall in front then player can pressing space or in air and pressing forward
        {
            Debug.Log("Vaulting");
            canVault = true;
        }

        if (canVault)
        {
            canVault = false; // so this is only called once
            rigidbody.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = vaultEndPoint.position;
            RecordedStartPosition = transform.position;
            isParkour = true;
            chosenParkourMoveTime = vaultTime;
            animatorController.PlayTargetAnimation("Vault", true);
        }

        //climb
        if (detectClimbObject.Obstruction && !detectClimbObstruction.Obstruction && !canClimb)
        {
            Debug.Log("Climbing");

            canClimb = true;
        }

        if (canClimb)
        {

            canClimb = false; // so this is only called once
            rigidbody.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = climbEndPoint.position;
            RecordedStartPosition = transform.position;
            isParkour = true;
            chosenParkourMoveTime = climbTime;
            animatorController.PlayTargetAnimation("Climb", true);
        }

        //Parkour movement
        if (isParkour && t_parkour < 1f)
        {
            t_parkour += Time.deltaTime / chosenParkourMoveTime;
            transform.position = Vector3.Lerp(RecordedStartPosition, RecordedMoveToPosition, t_parkour);

            if (t_parkour >= 1f)
            {
                isParkour = false;
                t_parkour = 0f;

            }
            rigidbody.isKinematic = false;

        }

    }
    private void HandleSliding()
    {
        //
    }

}
