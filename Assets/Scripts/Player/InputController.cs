using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }

    public event Action Interact;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();

        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Interact?.Invoke();
    }
}
