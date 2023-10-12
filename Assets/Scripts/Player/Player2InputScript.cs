using System.Collections.Generic;
using UnityEngine;

public class Player2InputScript : BasePlayerInputScript
{
    private const int PlayerNumber = 1;
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        ColliderStates[PlayerNumber] = new Dictionary<Collider2D, bool>();
        foreach (var key in GetComponents<Collider2D>())
        {
            ColliderStates[PlayerNumber][key] = false;
        }
        
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player2.HorizontalMovement.performed += Move;
        _playerInput.Player2.HorizontalMovement.canceled += StoppedMoving;
        _playerInput.Player2.Crouch.performed += Crouch;
        _playerInput.Player2.Crouch.canceled += StoppedCrouching;
        _playerInput.Player2.Fly.performed += Fly;
        _playerInput.Player2.Fly.canceled += StoppedFlying;
        // Player 2 does not have a pause function. If both players pause, 2 pause menus will be instantiated.
    }

    private void OnDisable()
    {
        _playerInput.Player2.HorizontalMovement.performed -= Move;
        _playerInput.Player2.HorizontalMovement.canceled -= StoppedMoving;
        _playerInput.Player2.Crouch.performed -= Crouch;
        _playerInput.Player2.Crouch.canceled -= StoppedCrouching;
        _playerInput.Player2.Fly.performed -= Fly;
        _playerInput.Player2.Fly.canceled -= StoppedFlying;
        _playerInput.Disable();
    }

    private void FixedUpdate() => UpdateLoop(_rb);
    private void OnCollisionEnter2D(Collision2D other) => CollisionEntered2D(other, PlayerNumber);
    private void OnCollisionExit2D(Collision2D other) => CollisionExited2D(other, PlayerNumber);
}