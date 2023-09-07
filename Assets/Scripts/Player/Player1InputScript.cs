using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1InputScript : BasePlayerInputScript
{
    
    // TODO ATTEMPT TO FIX ONLY ONE PLAYER MOVING BY USING PLAYERINPUT() AND JUST USING SEPERATE MAPS FOR EACH PLAYER
    // TODO WHAT SHOULD PROBABLY BE DONE IS THAT EACH SCRIPT IS A SUBSCRIPT OF AN (ABSTRACT?) PLAYER INPUT SCRIPT
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player1.HorizontalMovement.performed += Move;
        _playerInput.Player1.HorizontalMovement.canceled += StoppedMoving;
        _playerInput.Player1.Crouch.performed += Crouch;
        _playerInput.Player1.Crouch.canceled += StoppedCrouching;
        _playerInput.Player1.Fly.performed += Fly;
        _playerInput.Player1.Fly.canceled += StoppedFlying;
        _playerInput.Player1.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        _playerInput.Player1.HorizontalMovement.performed -= Move;
        _playerInput.Player1.HorizontalMovement.canceled -= StoppedMoving;
        _playerInput.Player1.Crouch.performed -= Crouch;
        _playerInput.Player1.Crouch.canceled -= StoppedCrouching;
        _playerInput.Player1.Fly.performed -= Fly;
        _playerInput.Player1.Fly.canceled -= StoppedFlying;
        _playerInput.Player1.Pause.performed -= Pause;
        _playerInput.Disable();
    }

    private void FixedUpdate() => UpdateLoop(_rb);
}