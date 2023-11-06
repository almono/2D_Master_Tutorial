using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls playerControls;

    public float valueX;

    void Awake()
    {
        playerControls = new Controls();
    }

    void OnEnable()
    {
        playerControls.Player.Move.performed += StartMove;
        playerControls.Player.Move.canceled += StopMove;

        playerControls.Player.Enable();
    }

    void OnDisable()
    {
        playerControls.Player.Move.performed -= StartMove;
        playerControls.Player.Move.canceled -= StartMove;
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
}
