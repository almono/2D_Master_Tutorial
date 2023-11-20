using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls playerControls;

    public float valueX;
    public bool jumpInput;

    public bool tryAttack;

    // for ladders
    public float valueY;
    public bool tryingToClimb;
    private bool isPaused = false;

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

        playerControls.Player.MeleeAttack.performed += TryToAttack;
        playerControls.Player.MeleeAttack.canceled += StopTryToAttack;

        playerControls.Player.Climb.performed += ClimbStart;
        playerControls.Player.Climb.canceled += StopClimbing;

        playerControls.Player.Pause.performed += PauseGame;

        playerControls.Player.Enable();
    }

    void OnDisable()
    {
        playerControls.Player.Move.performed -= StartMove;
        playerControls.Player.Move.canceled -= StartMove;

        playerControls.Player.Jump.performed -= JumpStart;
        playerControls.Player.Jump.canceled -= JumpStart;

        playerControls.Player.MeleeAttack.performed -= TryToAttack;
        playerControls.Player.MeleeAttack.canceled -= StopTryToAttack;

        playerControls.Player.Climb.performed -= ClimbStart;
        playerControls.Player.Climb.canceled -= StopClimbing;

        playerControls.Player.Pause.performed -= PauseGame;

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

    private void TryToAttack(InputAction.CallbackContext ctx)
    {
        tryAttack = true;
    }

    private void StopTryToAttack(InputAction.CallbackContext ctx)
    {
        tryAttack = false;
    }

    private void ClimbStart(InputAction.CallbackContext ctx)
    {
        valueY = Mathf.RoundToInt(ctx.ReadValue<float>()); // get axis values

        // check if we are pressing climb buttons
        // absolute is used in case we are climbing down
        if(Mathf.Abs(valueY) > 0)
        {
            tryingToClimb = true;
        }
    }

    private void StopClimbing(InputAction.CallbackContext ctx)
    {
        tryingToClimb = false;
        valueY = 0;
    }

    public void DisableControls()
    {
        playerControls.Player.Move.performed -= StartMove;
        playerControls.Player.Move.canceled -= StartMove;

        playerControls.Player.Jump.performed -= JumpStart;
        playerControls.Player.Jump.canceled -= JumpStart;

        playerControls.Player.MeleeAttack.performed -= TryToAttack;
        playerControls.Player.MeleeAttack.canceled -= StopTryToAttack;

        playerControls.Player.Climb.performed -= ClimbStart;
        playerControls.Player.Climb.canceled -= StopClimbing;

        playerControls.Player.Disable();
        valueX = 0;
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
            playerControls.Player.Disable();
        } else
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            playerControls.Player.Enable();
        }
    }
}
