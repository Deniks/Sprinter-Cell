using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls inputActions;
    AnimatorController animatorManager;
    PlayerLocomotion playerLocomotion;

    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float cameraInputX;
    public float cameraInputY;

    public bool sprintInput; // Shift
    public bool walkInput;  // Ctrl
    public bool jumpInput; // Spacebar

    public bool isInteracting;

    public Vector2 movementInput;
    public Vector2 cameraInput;
    
    public bool escInput = false;
    
    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<AnimatorController>();
    }
    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            #region Sprint Input
            inputActions.PlayerActions.Sprint.performed += i => sprintInput = true;
            inputActions.PlayerActions.Sprint.canceled += i => sprintInput = false;
            #endregion

            #region Walk Input
            inputActions.PlayerActions.Walk.performed += i => walkInput = true;
            inputActions.PlayerActions.Walk.canceled += i => walkInput = false;
            #endregion

            inputActions.PlayerActions.Jump.performed += i => jumpInput = true;

            inputActions.UI.ESC.performed += i => escInput = true;

        }

        inputActions.Enable();

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleWalkInput();
        HandleJumpInput();
    }

    private void HandleMovementInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting, playerLocomotion.isWalking);
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }
    private void HandleWalkInput()
    {
        if (walkInput && moveAmount > 0)
        {
            playerLocomotion.isWalking = true;
        }
        else
        {
            playerLocomotion.isWalking = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }
}