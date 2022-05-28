using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, Controls.IPlayerControlsActions
{
    private Controls controls;
    public Vector2 Movement {get; private set;}

    public event Action Confirm;
    public event Action Menu;

    public event Action KillSelf;

    void Start()
    {
        controls = new ();
        controls.PlayerControls.SetCallbacks(this);

        controls.PlayerControls.Enable();
    }

    void Update()
    {
    }

    private void OnDestroy()
    {
        controls.PlayerControls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }

        Confirm?.Invoke();
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }

        Menu?.Invoke();
    }

    public void OnDeath(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        
        KillSelf?.Invoke();
    }
}
