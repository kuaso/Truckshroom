using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private float _horizontalMovement;
    private float _verticalMovement;

    [SerializeField] private float horizontalMultiplier = 15f;
    [SerializeField] private float verticalMultiplier = 20f;
    [SerializeField] private float gravity = 9.81f;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.HorizontalMovement.performed += Move;
        _playerInput.Player.HorizontalMovement.canceled += StoppedMoving;
        _playerInput.Player.Crouch.performed += Crouch;
        _playerInput.Player.Crouch.canceled += StoppedCrouching;
        _playerInput.Player.Fly.performed += Fly;
        _playerInput.Player.Fly.canceled += StoppedFlying;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.HorizontalMovement.performed -= Move;
        _playerInput.Player.HorizontalMovement.canceled -= StoppedMoving;
        _playerInput.Player.Crouch.performed -= Crouch;
        _playerInput.Player.Crouch.canceled -= StoppedCrouching;
        _playerInput.Player.Fly.performed -= Fly;
        _playerInput.Player.Fly.canceled -= StoppedFlying;
    }

    private void Update()
    {
        _rb.velocity = new Vector2(_horizontalMovement, _verticalMovement - gravity);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _horizontalMovement = ctx.ReadValue<Vector2>().x * horizontalMultiplier;
        // TODO Flip sprite based on direction moved
    }

    private void StoppedMoving(InputAction.CallbackContext ctx) => _horizontalMovement = 0f;

    private void Crouch(InputAction.CallbackContext ctx)
    {
        // TODO manipulate sprite and hit-box 
    }

    private void StoppedCrouching(InputAction.CallbackContext ctx)
    {
        // TODO manipulate sprite and hit-box
    }

    // This is a button press, not a vector value. When this is called, we know that the button is being pressed/held.
    private void Fly(InputAction.CallbackContext ctx) => _verticalMovement = 1f * verticalMultiplier;

    private void StoppedFlying(InputAction.CallbackContext ctx) => _verticalMovement = 0f;
}