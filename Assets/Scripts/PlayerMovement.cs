using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput _input;
    private Rigidbody2D _rb;
    private Vector2 _xMovement;
    private Vector2 _yMovement = Vector2.zero;

    public bool isMovementEnabled = true;
    [SerializeField] private float velocityMultiplierX = 5f;
    [SerializeField] private float velocityMultiplierJump = 3f;

    private void Awake()
    {
        _input = new CustomInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player1.XMovement.performed += OnXMovement;
        _input.Player1.XMovement.canceled += OnXCancelled;
        _input.Player1.Fly.performed += OnJump;
        _input.Player1.Fly.canceled += OnJumpCancelled;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player1.XMovement.performed -= OnXMovement;
        _input.Player1.XMovement.canceled -= OnXCancelled;
        _input.Player1.Fly.performed -= OnJump;
        _input.Player1.Fly.canceled -= OnJumpCancelled;
    }

    private void FixedUpdate() =>
        _rb.velocity = isMovementEnabled
            ? new Vector2(_xMovement.x * velocityMultiplierX, _yMovement.y * velocityMultiplierJump)
            : Vector2.zero;

    private void OnXMovement(InputAction.CallbackContext callbackContext) =>
        _xMovement = callbackContext.ReadValue<Vector2>();

    private void OnXCancelled(InputAction.CallbackContext callbackContext) => _xMovement = Vector2.zero;

    private void OnJump(InputAction.CallbackContext callbackContext) => _yMovement.y = velocityMultiplierJump;

    private void OnJumpCancelled(InputAction.CallbackContext callbackContext) => _yMovement = Vector2.zero;
}