using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{

    PlayerControls inputActions;

    private Vector2 mouseInput;
    private Vector2 movementInput;

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => mouseInput = i.ReadValue<Vector2>();

        }

        inputActions.Enable();

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public Vector2 GetMousePosition()
    {
        return mouseInput;
    }

    public Vector2 GetMovementPosition()
    {
        return movementInput;
    }
}