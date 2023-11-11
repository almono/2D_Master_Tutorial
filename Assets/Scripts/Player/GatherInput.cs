using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls playerControls;

    public float valueX;
    public bool jumpInput;

    void Awake()
    {
        playerControls = new Controls();
    }

    void OnEnable()
    {
        playerControls.Player.Move.performed += StartMove;
        playerControls.Player.Move.canceled += StopMove;

        playerControls.Player.Jump.performed += JumpStart;
        playerControls.Player.Jump.canceled += JumpStart;

        playerControls.Player.Enable();
    }

    void OnDisable()
    {
        playerControls.Player.Move.performed -= StartMove;
        playerControls.Player.Move.canceled -= StartMove;

        playerControls.Player.Jump.performed -= JumpStart;
        playerControls.Player.Jump.canceled -= JumpStart;

        playerControls.Player.Disable();
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }

    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }
}
