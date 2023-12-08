using System.Collections.Generic;
using UnityEngine;

public class Player2InputScript : BasePlayerInputScript
{
    private const int PlayerNumber = 1;
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        IsGrounded.Add(PlayerNumber, false);

        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player2.HorizontalMovement.performed += Move;
        _playerInput.Player2.HorizontalMovement.canceled += StoppedMoving;
        _playerInput.Player2.Crouch.performed += Crouch;
        _playerInput.Player2.Crouch.canceled += StoppedCrouching;
        _playerInput.Player2.Fly.performed += Fly;
        _playerInput.Player2.Fly.canceled += StoppedFlying;
        // Player 2 does not have a pause function. If both players pause, 2 pause menus will be instantiated.
        _playerInput.Player2.Carry.performed += ToggleCarry;
        _playerInput.Player2.Carry.canceled += ToggleCarry;
    }

    private void OnDisable()
    {
        _playerInput.Player2.HorizontalMovement.performed -= Move;
        _playerInput.Player2.HorizontalMovement.canceled -= StoppedMoving;
        _playerInput.Player2.Crouch.performed -= Crouch;
        _playerInput.Player2.Crouch.canceled -= StoppedCrouching;
        _playerInput.Player2.Fly.performed -= Fly;
        _playerInput.Player2.Fly.canceled -= StoppedFlying;
        _playerInput.Player2.Carry.performed -= ToggleCarry;
        _playerInput.Player2.Carry.canceled -= ToggleCarry;
        _playerInput.Disable();
    }

    private void FixedUpdate() => UpdateLoop(_rb, PlayerNumber, _animator);
}